﻿using System;
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
    private TextGameObject saveText;
    private bool drawSaveText, firstTime;
    private double time;
    
    public SpecialLevel(int roomNumber, string name, bool saved = false)
    {
        drawSaveText = saved;
        firstTime = true;

        //Adding the player and grid
        player = new Player(Vector3.Zero);
        player.Parent = this;
        tileGrid = LoadLevel(name);
        tileGrid.Parent = this;
        gameObjects.Add(tileGrid);
        player.LoadContent();
        gameObjects.Add(player);

        //Setting the roomcounter
        roomCounter = new TextGameObject("text");
        roomCounter.text = roomNumber.ToString();
        //Adding the hanging man
        if (roomCounter.text == "100")
        {
            BillBoard man = new BillBoard(new Vector3(700, 210, 200));
            man.Parent = this;
            gameObjects.Add(man);
        }

        if (drawSaveText)
        {
            saveText = new TextGameObject("text");
            saveText.text = "Progress Saved";
            saveText.Position = new Vector2((GameEnvironment.Screen.X - saveText.Size.X) / 2, 0);
        }
        if (name == "Content\\Special Levels\\Final.txt")
            exitText.text = "Press E to kill yourself";
        else
            exitText.text = "Press E to proceed";

        //Adding the roomcounter
        gameObjects.Add(roomCounter);

        //Adding stamina
        stamina = new Stamina();
        stamina.Parent = this;
        gameObjects.Add(stamina);

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
        else if (chr == 'K')
        {
            Decoration notepad = new Decoration("Misc Level Objects\\NotePad\\NotePad Model", "Notepad");
            notepad.Position = new Vector3(x * 200, 102, y * 200);
            notepad.Parent = this;
            gameObjects.Add(notepad);
            return new PathTile("01");
        }
        else if (chr == 'D')
        {
            Decoration door = new Decoration("Misc Level Objects\\Door\\Door Model", "Door");
            door.Position = new Vector3(x * 200, 155, y * 200 + 100);
            door.modelRotation = (float)Math.PI / 180 * 90;
            door.Parent = this;
            gameObjects.Add(door);
            player.Position = new Vector3(x * 200, 200f, y * 200);
            return new PathTile("01");
        }
        else if (chr == 'C')
        {
            Decoration closet = new Decoration("Misc Level Objects\\Closet\\Closet Model", "Closet");
            closet.Position = new Vector3(x * 200, 190, y * 200 - 80);
            closet.Parent = this;
            closet.modelRotation = (float)Math.PI / 180 * 270;
            gameObjects.Add(closet);
            return new PathTile("01");
        }
        else if (chr == 'V')
        {
            Decoration cupboard = new Decoration("Misc Level Objects\\Cupboard\\Cupboard Model", "Cupboard");
            cupboard.Position = new Vector3(x * 200 + 100, 165, y * 200);
            cupboard.Parent = this;
            cupboard.modelRotation = (float)Math.PI / 180 * 180;
            gameObjects.Add(cupboard);
            return new PathTile("01");
        }
        else if (chr == '1')
            return new WallTile("No Exit Left");
        else if (chr == '2')
            return new WallTile("No Exit Middle");
        else if (chr == '3')
            return new WallTile("No Exit Right");
        else
            return null;
    }

    public override void Update(GameTime gameTime)
    {
        if (firstTime)
        {
            time = gameTime.TotalGameTime.TotalSeconds;
            firstTime = false;
        }
        if (gameTime.TotalGameTime.TotalSeconds >= time + 5)
        {
            drawSaveText = false;
        }
        if (roomCounter.text == "1" && player.Position.Z > 850)
        {
            foreach (Sound sound in MusicPlayer.Music)
                sound.PlaySound();
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {        
        base.Draw(gameTime, spriteBatch);
        if (drawSaveText)
        {
            saveText.Position = new Vector2((GameEnvironment.Screen.X - saveText.Size.X) / 2, 0);
            saveText.Draw(gameTime, spriteBatch);
        }

    }
}
