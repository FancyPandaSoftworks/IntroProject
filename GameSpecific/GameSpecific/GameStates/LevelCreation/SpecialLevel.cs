using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

/// <summary>
/// A regular pre-created level
/// </summary>
class SpecialLevel : Level 
{
    private TileGrid tileGrid;
    
    public SpecialLevel(int roomNumber, string name)
    {
        //add items to the level
        player = new Player(Vector3.Zero);
        player.Parent = this;
        tileGrid = LoadLevel(name);
        tileGrid.Parent = this;
        gameObjects.Add(tileGrid);
        player.LoadContent();
        gameObjects.Add(player);
        stamina = new Stamina();
        stamina.Parent = this;
        gameObjects.Add(stamina);

        if (name == "Content\\Special Levels\\Final.txt")
            text.text = "Press E to kill yourself";
        else
            text.text = "Press E to proceed";
    }

    /// <summary>
    /// Loads the level's grid of tiles
    /// </summary>
    /// <param name="name">The name of the file from which to open the level</param>
    /// <returns>Returns the TileGrid filled with the complete level</returns>
    private TileGrid LoadLevel(string name)
    {
        List<string> text = new List<string>();
        TileGrid tileGrid;
        using (StreamReader streamReader = new StreamReader(name))
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
                    Tile tile = LoadTile(text[y][x], x, y, name);
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
    private Tile LoadTile(char chr, int x, int y, string name)
    {
        if (chr == 'W')
            return new WallTile("01");
        else if (chr == 'P')
            return new PathTile("01");
        else if (chr == 'N')
        {
            //place the player in the entry tile
            player.Position = new Vector3(x * 200, 200f, y * 200);
            return new EntryTile("01");
        }
        else if (chr == 'X')
        {
            ExitTile exitTile = new ExitTile("01");
            if (name == "Content\\Special Levels\\Final.txt")
                exitTile.exitObject = new Object3D("Misc Level Objects\\Pistol\\Pistol Model");
            return exitTile;
        }
        else
            return null;
    }
}
