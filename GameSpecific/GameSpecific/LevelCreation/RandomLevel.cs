using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

//A randomly generated level
class RandomLevel : Level
{
    private Dictionary<Point, Tile> tileList; //List of tiles, accessible by a position
    private List<Point> keyList; //List of all occupied positions
    private Tile newTile; //Next tile to be build
    private new Point position; 
    private Random random;
    private int tiles; //Amount of tiles to indicate the size of the level
   
    //Gives the tileList
    public Dictionary<Point, Tile> TileList
    {
        get { return tileList; }
    }

    //Gives the keyList
    public List<Point> KeyList
    {
        get
        {

            return keyList;
        }
    }

    //making the list of tiles into a grid
    public TileGrid Grid //auteur: Wouter
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

            grid = new TileGrid(highestPoint.X - lowestPoint.X + 3, highestPoint.Y - lowestPoint.Y + 3, "Grid");
            Console.WriteLine(highestPoint.X - lowestPoint.X);
            Console.WriteLine(highestPoint.Y - lowestPoint.Y);
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
                            grid.Objects[i - 1, j] = new WallTile();
                            grid.Objects[i - 1, j].Position = new Vector3((i - 1) * grid.CellWidth, grid.CellHeight, j * grid.CellHeight);
                        }
                        if (!(grid.Objects[i + 1, j] is Tile))
                        {
                            grid.Objects[i + 1, j] = new WallTile();
                            grid.Objects[i + 1, j].Position = new Vector3((i + 1) * grid.CellWidth, grid.CellHeight, j * grid.CellHeight);
                        }
                        if (!(grid.Objects[i, j - 1] is Tile))
                        {
                            grid.Objects[i, j - 1] = new WallTile();
                            grid.Objects[i, j - 1].Position = new Vector3(i * grid.CellWidth, grid.CellHeight, (j - 1) * grid.CellHeight);
                        }
                        if (!(grid.Objects[i, j + 1] is Tile))
                        {
                            grid.Objects[i, j + 1] = new WallTile();
                            grid.Objects[i, j + 1].Position = new Vector3(i * grid.CellWidth, grid.CellHeight, (j + 1) * grid.CellHeight);
                        }
                    }
                }
            }
            //Step 4: returning the grid
            return grid;
        }
    }

    //Creating/generating the level itself
    public RandomLevel(int roomNumber, int tiles = 10, bool chased = false) : base(roomNumber)
    {
        //Assining the variables
        tileList = new Dictionary<Point, Tile>();
        keyList = new List<Point>();
        newTile = new EntryTile();
        random = new Random();
        this.tiles = tiles;

        //Create the startpoint
        //newTile.this.position = Point.Zero;
        position = Point.Zero;  //newTile.this.position;
        tileList.Add(position, newTile);
        keyList.Add(position);

        //Creating the paths
        CreateMainPath();

        for (int i = random.Next(1, tiles / 4); i > 0; i--)
<<<<<<< HEAD
            CreateSidePath(random.Next(3, tiles / 2));
=======
            CreateSidePath(random.Next(3, tiles / 2), chased);
>>>>>>> origin/master

        TileGrid tileGrid = Grid;
        gameObjects.Add(tileGrid);

        player = new Player(new Vector3(tileGrid.Columns * 200 /2, 200 , tileGrid.Rows * 200 /2 ));
        gameObjects.Add(player);
    }

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
                    newTile = new PathTile();
                    tileList.Add(this.position, newTile);
                }
                catch
                {
                    //Mainpath enclosed itself, ending it here
                    Console.WriteLine("MainPathError");
                    possiblePositions = GetPossiblePositions(this.position);
                    this.position = possiblePositions[i];
                    newTile = new ExitTile();
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
                    newTile = new ExitTile();
                    tileList.Add(this.position, newTile);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    //Couldn't place exit, so place it at the last placed tile
                    newTile = new ExitTile();
                    this.position = keyList[keyList.Count - 1];
                    tileList[this.position] = newTile;
                     Console.WriteLine(e.StackTrace);
                    tiles--;
                    continue;
                }
                
            }

            keyList.Add(this.position);
/*            possiblePositions.RemoveAt(i);

            foreach (Point element in possiblePositions)
            {
                tileList.Add(element, new WallTile(element));
                keyList.Add(element);
            }
*/
           // Console.WriteLine("X:" + this.position.X + "\nY:" + this.position.Y + "");

            tiles--;
        }

    }


    //Creating SidePath From here
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
                        nextTile = new PathTile();
                        tileList.Add(this.position, nextTile);
                        keyList.Add(this.position);
                        // Console.WriteLine("Side: \nX: " + this.position.X + "\nY: " + this.position.Y);
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
                    if (tileList.ContainsKey(new Point(this.position.X + x, this.position.Y + y)))
                    {
                        //Choose where to place the Tile
                        this.position.X += x;
                        this.position.Y += y;
                        nextTile = new PathTile();
                        tileList.Add(this.position, nextTile);
                        keyList.Add(this.position);

                        // Console.WriteLine("Side: \nX: " + this.position.X + "\nY: " + this.position.Y);
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

    //Checks whether the place is suitable for a SidePath
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
                return (new SidePathEntryTile());
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
    
    //Can a SidePathEntryTile be placed here
    private bool CanPlaceSideEntry(Point currentPosition)
    {
        return (!tileList.ContainsKey(currentPosition) && GetPossiblePositions(currentPosition).Count > 0);
    }

    //Checking all the empty tiles around the position
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

    //Checks whether a MainPathTile can be placed here
    private bool CanPlaceMainPathTile(Point currentPosition)
    {
        return (!tileList.ContainsKey(currentPosition) && GetPossiblePositions(currentPosition).Count == 3);
    }

}