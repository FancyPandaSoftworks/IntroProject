using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

/// <summary>
/// A randomly generated level
/// </summary>
class RandomLevel : Level
{
    private Dictionary<Point, Tile> tileList; //List of tiles, accessible by a position
    private List<Point> keyList; //List of all occupied positions
    private Tile newTile; //Next tile to be build
    private new Point position; 
    private Random random;
    private int tiles; //Amount of tiles to indicate the size of the level
    private string pathID, wallID;
    private float timer;
    private bool chased, monsterMade;

   
    /// <summary>
    /// Property for getting the grid by making a grid out of the list of tiles
    /// </summary>
    public TileGrid Grid
    {
        get
        {
            TileGrid grid;

            //Step 1: figuring out the size of the grid
            Point lowestPoint = keyList[0];
            Point highestPoint = keyList[0];
            foreach (Point point in keyList)
            {
                if (point.X < lowestPoint.X)
                {
                    lowestPoint.X = point.X;
                }
                if (point.Y < lowestPoint.Y)
                {
                    lowestPoint.Y = point.Y;
                }
                if (point.X > highestPoint.X)
                {
                    highestPoint.X = point.X;
                }
                if (point.Y > highestPoint.Y)
                {
                    highestPoint.Y = point.Y;
                }
            }

            grid = new TileGrid(highestPoint.X - lowestPoint.X + 3, highestPoint.Y - lowestPoint.Y + 3, "TileGrid");
            
            //Step 2: filling the grid
            foreach (Point position in keyList)
            {
                grid.Add(tileList[position], position.X - lowestPoint.X + 1, position.Y - lowestPoint.Y + 1);
            }

            //Step 3: adding the walls
            for (int i = 0; i < grid.Objects.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Objects.GetLength(1); j++)
                {
                    if (grid.Objects[i, j] != null && !(grid.Objects[i, j] is WallTile))
                    {
                        Tile tile = (Tile)grid.Objects[i, j];
                        if (!(grid.Objects[i - 1, j] is Tile))
                        {
                            grid.Objects[i - 1, j] = new WallTile(wallID);
                            grid.Objects[i - 1, j].Position = new Vector3((i - 1) * GameObjectGrid.CellWidth, GameObjectGrid.CellHeight, j * GameObjectGrid.CellHeight);
                        }
                        if (!(grid.Objects[i + 1, j] is Tile))
                        {
                            grid.Objects[i + 1, j] = new WallTile(wallID);
                            grid.Objects[i + 1, j].Position = new Vector3((i + 1) * GameObjectGrid.CellWidth, GameObjectGrid.CellHeight, j * GameObjectGrid.CellHeight);
                        }
                        if (!(grid.Objects[i, j - 1] is Tile))
                        {
                            grid.Objects[i, j - 1] = new WallTile(wallID);
                            grid.Objects[i, j - 1].Position = new Vector3(i * GameObjectGrid.CellWidth, GameObjectGrid.CellHeight, (j - 1) * GameObjectGrid.CellHeight);
                        }
                        if (!(grid.Objects[i, j + 1] is Tile))
                        {
                            grid.Objects[i, j + 1] = new WallTile(wallID);
                            grid.Objects[i, j + 1].Position = new Vector3(i * GameObjectGrid.CellWidth, GameObjectGrid.CellHeight, (j + 1) * GameObjectGrid.CellHeight);
                        }
                    }
                }
            }
            grid.Parent = this;

            //Step 4: returning the grid
            return grid;
        }
    }

    /// <summary>
    /// Creating/generating the level itself
    /// </summary>
    /// <param name="roomNumber">The number of the room</param>
    /// <param name="tiles">The size the Mainpath should be, counted in tiles</param>
    /// <param name="chased">Whether or not the monster is chasing the player</param>
    public RandomLevel(int roomNumber, int tiles = 10, bool chased = false)
    {
        //Setting two booleans for the monster (they are used in the Update method)
        this.chased = chased;
        monsterMade = false;

        //Assining the variables
        tileList = new Dictionary<Point, Tile>();
        keyList = new List<Point>();
        
        random = GameEnvironment.Random;
        
        //Select the tiles to use
        int pathType = random.Next(3);
        if (pathType < 9)
            pathID = "0" + (pathType + 1);
        else
            pathID = "" + (pathType + 1);

        int wallType = random.Next(3);
        if (wallType < 9)
            wallID = "0" + (wallType + 1);
        else
            pathID = "" + (wallType + 1);
        newTile = new EntryTile(pathID);
        this.tiles = tiles;

        //Create the startpoint
        position = Point.Zero;
        tileList.Add(position, newTile);
        keyList.Add(position);

        //Creating the paths
        CreateMainPath();

        for (int i = random.Next(1, tiles / 4); i > 0; i--)
            CreateSidePath(random.Next(3, tiles / 2), chased);

        //making the tile grid
        TileGrid tileGrid = Grid;
        gameObjects.Add(tileGrid);

        //making the player
        player = new Player(Vector3.Zero);
        gameObjects.Add(player);
        player.Parent = this;
        player.LoadContent();

        foreach(GameObject obj in tileGrid.Objects)
        {
            if (obj != null)
                if(obj.ID == "EntryTile")
                    player.Position = new Vector3(obj.Position.X, obj.Position.Y + GameObjectGrid.CellHeight, obj.Position.Z);
        }

        //making the stamina bar
        stamina = new Stamina();
        gameObjects.Add(stamina);
        stamina.Parent = this;
        text.text = "Press E to proceed";
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer > 3)
        {
            //making the monster
            if (chased && !monsterMade)
            {
                //Playing a sound-effect before the monster has entered the room
                foreach (Sound sound in MusicPlayer.SoundEffect)
                    if (sound.Name == "MonsterScreech")
                        sound.PlaySound();
                //Making the monster
                Monster monster = new Monster(Grid.Objects);
                monster.Parent = this;
                monster.LoadContent();
                gameObjects.Add(monster);
                monsterMade = true;
            }
        }

        if (GameEnvironment.Random.Next(10000) == 0 && !chased)
            foreach (Sound sound in MusicPlayer.SoundEffect)
                if (sound.Name == "WindAmbience")
                {
                    sound.PlaySound();
                }
    }

    /// <summary>
    /// Create the Mainpath
    /// </summary>
    private void CreateMainPath()
    {
        //Creating all MainPathTiles
        while (tiles >= 0)
        {
            List<Point> possiblePositions = new List<Point>();

            //Look where a Tile can be placed
            Point position = new Point(this.position.X - 1, this.position.Y);

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            position.X = this.position.X;
            position.Y = this.position.Y + 1;

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            position.X = this.position.X + 1;
            position.Y = this.position.Y;

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            position.X = this.position.X;
            position.Y = this.position.Y - 1;

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            //Pick where to place it
            int i = random.Next(0, possiblePositions.Count);

            if (tiles > 0)
            {
                //Choose where to place the Tile
                try
                {
                    this.position = possiblePositions[i];
                    newTile = new PathTile(pathID);
                    tileList.Add(this.position, newTile);
                }
                catch
                {
                    //Mainpath enclosed itself, ending it here
                    Console.WriteLine("MainPathError");
                    possiblePositions = GetPossiblePositions(this.position);
                    this.position = possiblePositions[i];
                    newTile = new ExitTile(pathID);
                    tileList.Add(this.position, newTile);
                    tiles = -2;
                }
            }
            else
            {
                //Choose where to place the Tile
                try
                {
                    this.position = possiblePositions[i];
                    newTile = new ExitTile(pathID);
                    newTile.Parent = this;
                    tileList.Add(this.position, newTile);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    //Couldn't place exit, so place it at the last placed tile
                    newTile = new ExitTile(pathID);
                    newTile.Parent = this;
                    this.position = keyList[keyList.Count - 1];
                    tileList[this.position] = newTile;
                     Console.WriteLine(e.StackTrace);
                    tiles--;
                    continue;
                }
                
            }

            keyList.Add(this.position);
            tiles--;
        }

    }

    /// <summary>
    /// Creating SidePath From here
    /// </summary>
    /// <param name="tiles">The size, counted in tiles</param>
    /// <param name="chased">Whether or not the monster is chasing th player</param>
    private void CreateSidePath(int tiles, bool chased)
    {
        //Pick a place to place a SidePath
        Point previousPosition;
        Point anchorPosition;
        do
        {
            anchorPosition = keyList[random.Next(0, keyList.Count - 1)];
        } while (tileList[anchorPosition] is ExitTile);
        
        previousPosition = anchorPosition;

        Tile nextTile = CanCreateSidePath(anchorPosition);

        //If it can be placed
        if (nextTile != null)
        {

            tileList.Add(this.position, nextTile);
            keyList.Add(this.position);

            if (!chased)
            {
                while (tiles > 0)
                {
                    List<Point> possiblePositions = GetPossiblePositions(this.position);

                    if (possiblePositions.Count > 0)
                    {
                        //Choose where to place the Tile
                        this.position = possiblePositions[random.Next(0, possiblePositions.Count - 1)];
                        nextTile = new PathTile(pathID);
                        tileList.Add(this.position, nextTile);
                        keyList.Add(this.position);
                    }
                    else
                    {
                        break;
                    }
                    tiles--;
                }
            }
            else
            {
                int x = this.position.X - previousPosition.X;
                int y = this.position.Y - previousPosition.Y;

                tiles = tiles / 4;
                
                while (tiles > 0)
                {
                    if (!(tileList.ContainsKey(new Point(this.position.X + x, this.position.Y + y))))
                    {
                        //Choose where to place the Tile
                        this.position.X += x;
                        this.position.Y += y;
                        nextTile = new PathTile(pathID);
                        tileList.Add(this.position, nextTile);
                        keyList.Add(this.position);
                    }
                    else
                    {
                        break;
                    }
                    tiles--;
                }
            }

        }
        //Try again
        else
        {
            CreateSidePath(tiles, chased);
        }
    }

    /// <summary>
    /// Checks whether the place is suitable for a path tile
    /// </summary>
    /// <param name="anchorPosition">The point from which to create the path</param>
    /// <returns>The Tile if it can be placed, null if it cannot</returns>
    private Tile CanCreateSidePath(Point anchorPosition)
    {
        List<Point> possibleEntrys = new List<Point>();
        Point test = anchorPosition;

        test.X = anchorPosition.X - 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        test.X = anchorPosition.X + 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        test.X = anchorPosition.X;
        test.Y = anchorPosition.Y - 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        test.Y = anchorPosition.Y + 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }

        if (possibleEntrys.Count >= 1)
        {
            try
            {
                //If it is suitable
                this.position = (possibleEntrys[random.Next(0, possibleEntrys.Count)]);
                return (new PathTile(pathID));
            }
            catch (Exception e)
            {
                //If it is, for some reason, unsuitable
                Console.WriteLine("SidePathEntryError: " + e.StackTrace);
                return null;
            }
        }
        else return null;
    }
    
    /// <summary>
    /// Can a SidePathEntryTile be placed here
    /// </summary>
    /// <param name="currentPosition">The position to be checked</param>
    /// <returns>True if it is a suitable position, false if this is not the case</returns>
    private bool CanPlaceSideEntry(Point currentPosition)
    {
        return (!tileList.ContainsKey(currentPosition) && GetPossiblePositions(currentPosition).Count > 0);
    }

    /// <summary>
    /// Checking all the empty tiles around the position
    /// </summary>
    /// <param name="currentPosition">The point from which to count</param>
    /// <returns>The list of the positions where the tiles can be placed around the given position</returns>
    private List<Point> GetPossiblePositions(Point currentPosition)
    {
        List<Point> possiblePositions = new List<Point>();

        Point position = new Point(currentPosition.X - 1, currentPosition.Y);

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        position.X = currentPosition.X;
        position.Y = currentPosition.Y + 1;

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        position.X = currentPosition.X + 1;
        position.Y = currentPosition.Y;

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        position.X = currentPosition.X;
        position.Y = currentPosition.Y - 1;

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        return possiblePositions;

    }

    /// <summary>
    /// Checks whether a MainPathTile can be placed here
    /// </summary>
    /// <param name="currentPosition">The point to check</param>
    /// <returns>Returns true if there can be a tile placed here, false if this is not the case</returns>
    private bool CanPlaceMainPathTile(Point currentPosition)
    {
        return (!tileList.ContainsKey(currentPosition) && GetPossiblePositions(currentPosition).Count == 3);
    }

}