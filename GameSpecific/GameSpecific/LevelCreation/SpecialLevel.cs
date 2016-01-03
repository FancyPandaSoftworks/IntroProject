using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;

class SpecialLevel : Level 
{
    TileGrid tileGrid;
    
    public SpecialLevel(int roomNumber, string name)
    {
        //add items to the level
        tileGrid = LoadLevel(name);
        gameObjects.Add(tileGrid);
        Player player = new Player(Vector3.Zero);
        gameObjects.Add(player);
    }

    //Loads the level's grid of tiles
    public TileGrid LoadLevel(string name)
    {
        List<string> text = new List<string>();
        StreamReader streamReader = new StreamReader(name);
        string line = streamReader.ReadLine();
        int width = line.Length;

        //read the file
        while (line != null)
        {
            text.Add(line);
            line = streamReader.ReadLine();
        }

        //make a grid for the tiles
        TileGrid tileGrid = new TileGrid(width + 1, text.Count + 1, "grid");

        //Load the tiles into the grid
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < text.Count - 1; ++y)
            {
                Tile tile = LoadTile(text[y][x], x, y);
                if (tile != null)
                {
                    tileGrid.Add(tile, x, y);
                    if( tile is WallTile)
                    {
                        tile.Position += new Vector3(0, 200, 0);
                    }
                }
            }
        }
        return tileGrid;
    }

    //Load a single Tile from a certain position in the file
    public Tile LoadTile(char chr, int x, int y)
    {
        if (chr == 'W')
            return new WallTile();
        else if (chr == 'P')
            return new PathTile();
        else if (chr == 'N')
        {
            //place the player in the entry tile
            player.Position = new Vector3(x * 200, 200f, y * 200);
            return new EntryTile();
        }
        else if (chr == 'X')
            return new ExitTile();
        else
            return null;
    }
}
