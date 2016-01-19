﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

/// <summary>
/// The state the game is in when you have died
/// </summary>
class GameOverState : GameState
{
    protected Button continueButton, exitButton;
    protected Button continueButtonMouseOver, exitButtonMouseOver;
    protected Object2D background, gameOver;

    public GameOverState()
    {
        //Add the background
        background = new Object2D("Note blank", 0);
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2, (GameEnvironment.Screen.Y - background.Height) / 2);
        gameObjects.Add(background);

        //Add text: game over
        gameOver = new Object2D("Menu Buttons\\GameOver");
        gameOver.Position = new Vector2((GameEnvironment.Screen.X - gameOver.Width) / 2, background.Position.Y);
        gameObjects.Add(gameOver);

        //add a continue button
        continueButton = new Button("Menu Buttons\\Menu button Continue", 0);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.Y - continueButton.Height) / 2);
        gameObjects.Add(continueButton);

        //Add a mouse-over continue button
        continueButtonMouseOver = new Button("Menu Buttons\\Menu button Continue MouseOver", 0);
        continueButtonMouseOver.Position = continueButton.Position;
        gameObjects.Add(continueButtonMouseOver);
        continueButtonMouseOver.Visible = false;

        //add an exit button
        exitButton = new Button("Menu Buttons\\Menu button Exit", 0);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - exitButton.Width) / 2, (GameEnvironment.Screen.Y - exitButton.Height) / 2 + 100);
        gameObjects.Add(exitButton);

        //Add a mouse-over exit button
        exitButtonMouseOver = new Button("Menu Buttons\\Menu button Exit MouseOver", 0);
        exitButtonMouseOver.Position = exitButton.Position;
        gameObjects.Add(exitButtonMouseOver);
        exitButtonMouseOver.Visible = false;
    }

    //method for resetting the positions of the buttons and background
    //neccesary for when one switches from fullscreen to windowed or the other way around
    public void ResetPositions()
    {
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2, (GameEnvironment.Screen.Y - background.Height) / 2);
        gameOver.Position = new Vector2((GameEnvironment.Screen.X - gameOver.Width) / 2, background.Position.Y);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.Y - continueButton.Height) / 2);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - exitButton.Width) / 2, (GameEnvironment.Screen.Y - exitButton.Height) / 2 + 100);
        continueButtonMouseOver.Position = continueButton.Position - new Vector2(20, 0);
        exitButtonMouseOver.Position = exitButton.Position - new Vector2(10, 0);
    }

    /// <summary>
    /// React to the input
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        inputHelper.Update();

        //Check if the continueButton is being pressed to go back to the game, starting from the last checkpoint
        if (continueButton.ButtonIsPressed)
        {
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            if (File.Exists("Content\\SaveFile.txt"))
            {
                using (StreamReader stream = new StreamReader("SaveFile.txt"))
                {
                    string line = stream.ReadLine();
                    if (line != null)
                    {
                        PlayingState playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
                        playingState.RoomCounter = int.Parse(line);
                    }
                }
            }
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        //Check if the exitbutton is being pressed, to switch back to the main menu
        if (exitButton.ButtonIsPressed)
        {
            foreach (Sound sound in MusicPlayer.Music)
            {
                sound.PlaySound();
            }
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");
        }

        //Change visibility of mouseoverbutton
        if (continueButton.IsMouseOver)
            continueButtonMouseOver.Visible = true;
        else
            continueButtonMouseOver.Visible = false;

        if (exitButtonMouseOver.IsMouseOver)
            exitButtonMouseOver.Visible = true;
        else
            exitButtonMouseOver.Visible = false;

    }

    /// <summary>
    /// Draw the game-over screen with the game in the background
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        GameEnvironment.GameStateManager.GetGameState("playingState").Draw(gameTime, spriteBatch);

        //Draw the Pausescreen
        spriteBatch.Begin();
        base.Draw(gameTime, spriteBatch);
        spriteBatch.End();

        //Go back to 3D mode
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        //Resetting button and background
        ResetPositions();

        MusicPlayer.dangerLevel = 0;
    }
}
