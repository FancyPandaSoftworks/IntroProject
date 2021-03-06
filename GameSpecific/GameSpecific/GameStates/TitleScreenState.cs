﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

/// <summary>
/// The state where the game starts of in
/// </summary>
class TitleScreenState : GameState
{
    protected Button continueButton, newGameButton, exitButton;
    protected Button continueButtonMouseOver, newGameButtonMouseOver, exitButtonMouseOver;
    protected Object2D title;
    private int part;
    private Level level;
    Player player;

    public TitleScreenState()
    {
        
        level = new Level();
        level.player = new Player(new Vector3(200, 200f, 200));

        foreach (Sound sound in MusicPlayer.Music)
        {
            sound.PlaySound();
        }

        //Add title
        title = new Object2D("Menu Buttons\\Chased");
        gameObjects.Add(title);

        //Add a continue button
        continueButton = new Button("Menu Buttons\\Menu button Continue", 0);
        gameObjects.Add(continueButton);
        
        //Add a mouse-over continue button
        continueButtonMouseOver = new Button("Menu Buttons\\Menu button Continue MouseOver", 0);
        gameObjects.Add(continueButtonMouseOver);
        continueButtonMouseOver.Visible = false;

        //Add a new game button
        newGameButton = new Button("Menu Buttons\\Menu button NewGame", 0);
        gameObjects.Add(newGameButton);

        //Add a mouse-over new game button
        newGameButtonMouseOver = new Button("Menu Buttons\\Menu button NewGame MouseOver", 0);
        gameObjects.Add(newGameButtonMouseOver);
        newGameButtonMouseOver.Visible = false;

        //Add an exit button
        exitButton = new Button("Menu Buttons\\Menu button Exit", 0);
        gameObjects.Add(exitButton);

        //Add a mouse-over exit button
        exitButtonMouseOver = new Button("Menu Buttons\\Menu button Exit MouseOver", 0);
        gameObjects.Add(exitButtonMouseOver);
        exitButtonMouseOver.Visible = false;

        part = 0;
    }

    public void ResetPositions()
    {
        title.Position = new Vector2((GameEnvironment.Screen.X - title.Width) / 2, 10);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2 - 100);
        newGameButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width)  / 2 + 100);
        continueButtonMouseOver.Position = continueButton.Position - new Vector2(20,0);
        newGameButtonMouseOver.Position = newGameButton.Position - new Vector2(10,0); 
        exitButtonMouseOver.Position = exitButton.Position - new Vector2(10,0);
    }

    /// <summary>
    /// Handle the input
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {        
        base.HandleInput(inputHelper);
        inputHelper.Update();

        //Check if the playbutton is being pressed to go to the game
        if (continueButton.ButtonIsPressed)
        {
            foreach (Sound sound in MusicPlayer.Music)
                sound.StopSound();

            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            if (File.Exists("Content\\SaveFile.txt"))
            {
                using (StreamReader stream = new StreamReader("Content\\SaveFile.txt"))
                {
                    string line = stream.ReadLine();
                    if (line != null)
                    {
                        PlayingState playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
                        playingState.RoomCounter = int.Parse(line);
                    }
                }
                GameEnvironment.GameStateManager.SwitchTo("playingState");
            }       
        }

        if (newGameButton.ButtonIsPressed)
        {
            foreach (Sound sound in MusicPlayer.Music)
                sound.StopSound();
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            File.WriteAllText("Content\\SaveFile.txt", String.Empty);
            PlayingState playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
            playingState.RoomCounter = 1;
            GameEnvironment.GameStateManager.SwitchTo("controlsGameState");
        }

        if (exitButton.ButtonIsPressed)
        {
            game.Exit();
        }

        //Change visibility of mouseoverbutton
        if (continueButton.IsMouseOver)
            continueButtonMouseOver.Visible = true;
        else
            continueButtonMouseOver.Visible = false;

        if (newGameButtonMouseOver.IsMouseOver)
            newGameButtonMouseOver.Visible = true;
        else
            newGameButtonMouseOver.Visible = false;

        if (exitButtonMouseOver.IsMouseOver)
            exitButtonMouseOver.Visible = true;
        else
            exitButtonMouseOver.Visible = false;
    }

    /// <summary>
    /// Draw the menu
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    { 
        //Draw the endless room
        part +=2;
        part = part % 60;
        DrawEndless(part,gameTime,spriteBatch);

        //Draw the Menu
        spriteBatch.Begin();
        level.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
        spriteBatch.End();

        //Go back to 3D mode
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        //Resetting the positions of the buttons
        ResetPositions();

        
        //Stop ambience sound in the main menu
        MusicPlayer.beatCount = 0;
        MusicPlayer.barCount = 0;
        MusicPlayer.dangerLevel = -1;
    }

    private void DrawEndless(int part,GameTime gameTime, SpriteBatch spriteBatch)
    {
        level = new Level();
        level.player = new Player(new Vector3(200, 200f, 200));

        GameObjectGrid grid = new GameObjectGrid(20,20,"grid");
        
        for (int i = 0; i < grid.Rows; i++)
        {
            grid.Add(new WallTile("01"), 0, i);
            grid.Add(new WallTile("01"), 2, i);
            grid.Add(new PathTile("01"), 1, i);
        }

        for (int x = 0; x < grid.Objects.GetLength(0); x++)
        {
            for (int y = 0; y < grid.Objects.GetLength(1); y++)
            {
                if (grid.Objects[x, y] != null)
                {
                    grid.Objects[x, y].Position -= new Vector3(0, 0, GameObjectGrid.CellWidth * part / 60);
                    if (grid.Objects[x, y] is WallTile)
                    {
                        grid.Objects[x, y].Position += new Vector3(0, 200, 0);
                    }
                }
            }
        }

        level.Add(grid);

        level.Parent = this;

        foreach (GameObject obj in level.Objects)
        {
            obj.Parent = level;
        }
    }
}
