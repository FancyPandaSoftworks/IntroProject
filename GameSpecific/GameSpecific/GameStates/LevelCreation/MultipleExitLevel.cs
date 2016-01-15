using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MultipleExitLevel : Level
{
    protected short previousPart;
    protected short part;
    protected TileGrid[] grids;
    protected Vector3[] startingPositions;
    private bool firstTime;

    public MultipleExitLevel()
    {
        part = 0;
        CreateGrids();
        Add(grids[part]);
        Add(player);
        completed = false;
        firstTime = true;
        player.Grid = grids[part];
        stamina = new Stamina();
        stamina.Parent = this;
        gameObjects.Add(stamina);

        text.text = "Press E to proceed";
    }

    public override void Update(GameTime gameTime)
    {
        if (firstTime) { player.Grid = grids[part]; firstTime = false; }
        base.Update(gameTime);
        if (part != previousPart)
        {
            player.Position = startingPositions[part];
            previousPart = part;
            this.gameObjects = new List<GameObject>();
            this.gameObjects.Add(grids[part]);
            this.gameObjects.Add(player);
            player.Grid = grids[part];
        }
    }

    private void CreateGrids()
    {
        grids = new TileGrid[3];
        startingPositions = new Vector3[3];
        grids[0] = LoadPart("MultipleExitLevelPart1", 0);
        grids[1] = LoadPart("MultipleExitLevelPart2", 1);
        grids[2] = LoadPart("MultipleExitLevelPart3", 2);
        player = new Player(startingPositions[0]);
        player.Parent = this;
        for (int i = 0; i < grids.Length; i++)
            grids[i].Parent = this;
    }

    private TileGrid LoadPart(string name, short part)
    {
        List<string> text = new List<string>();
        TileGrid tileGrid;
        using (StreamReader streamReader = new StreamReader("Content/" + name + ".txt"))
        {
            string line = streamReader.ReadLine();
            int width = line.Length;

            //read the file
            while (line != null)
            {
                text.Add(line);
                line = streamReader.ReadLine();
            }

            //make a grid for the tiles
            tileGrid = new TileGrid(width + 1, text.Count + 1, "TileGrid");

            //Load the tiles into the grid
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < text.Count; ++y)
                {
                    Tile tile = LoadTile(text[y][x], x, y, part);
                    if (tile != null)
                    {
                        tileGrid.Add(tile, x, y);
                        if (tile is WallTile)
                        {
                            tile.Position += new Vector3(0, 200, 0);
                        }
                    }
                }
            }
        }
        return tileGrid;
    }

    /// <summary>
    /// Load a single Tile from a certain position in the file
    /// </summary>
    /// <param name="chr">The character in the file, defines what tile it will be</param>
    /// <param name="x">The x-coördinate</param>
    /// <param name="y">The y-coördinate</param>
    /// <returns>The Tile to Load</returns>
    private Tile LoadTile(char chr, int x, int y, short part)
    {
        if (chr == 'W')
            return new WallTile("01");
        else if (chr == 'P')
            return new PathTile("01");
        else if (chr == 'N')
        {
            //place the player in the entry tile
            startingPositions[part] = new Vector3(x * 200, 200f, y * 200);
            return new EntryTile("01");
        }
        else if (chr == 'F')
            return new FakeExit(this);
        else if (chr == 'R')
            return new RealExit(this);
        else if (chr == 'X')
            return new ExitTile("01");
        else
            return null;
    }

    protected class FakeExit : ExitTile
    {
        protected MultipleExitLevel level;

        public FakeExit(MultipleExitLevel level)
            : base("01")
        {
            this.level = level;
        }

        public override void Update(GameTime gameTime)
        {
            //Check if the player is in the ExitTile and if so, if they are pressing E to procceed
            foreach (GameObject obj in level.Objects)
            {
                if (obj != null)
                {
                    if (obj.ID == "player")
                    {
                        if (obj.Position.X > Position.X - 100 && obj.Position.X < Position.X + 100 && obj.Position.Z > Position.Z - 100 && obj.Position.Z < Position.Z + 100)
                        {
                            Player player = obj as Player;
                            if (player.EDown == true)
                            {
                                this.level.part = 0;
                                this.level.player.Position = this.level.startingPositions[this.level.part];
                            }                              
                        }
                    }
                }
            }
        }
    }

    protected class RealExit : ExitTile
    {
        protected MultipleExitLevel level;

        public RealExit(MultipleExitLevel level)
            : base("01")
        {
            this.level = level;
        }

        public override void Update(GameTime gameTime)
        {
            //Check if the player is in the ExitTile and if so, if they are pressing E to procceed
            foreach (GameObject obj in level.Objects)
            {
                if (obj != null)
                {
                    if (obj.ID == "player")
                    {
                        if (obj.Position.X > Position.X - 100 && obj.Position.X < Position.X + 100 && obj.Position.Z > Position.Z - 100 && obj.Position.Z < Position.Z + 100)
                        {
                            Player player = obj as Player;
                            if (player.EDown == true)
                            {
                                this.level.part++;
                            }
                        }
                    }
                }
            }
        }
    }
}
