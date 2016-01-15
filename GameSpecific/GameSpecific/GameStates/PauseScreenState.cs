﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The state the game is in while paused
/// </summary>
class PauseScreenState : GameState
{
    protected Button continueButton, exitButton;
    protected Button continueButtonMouseOver, exitButtonMouseOver;
    protected Object2D background;

    public PauseScreenState()
    {
        background = new Object2D("Note blank", 0);
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2, (GameEnvironment.Screen.Y - background.Height) / 2);
        gameObjects.Add(background);

        //add a resume button
        continueButton = new Button("Menu Buttons\\Menu button Continue", 0);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.Y - continueButton.Height) / 2 - 100);
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

    //method for resetting the positions of the buttons and background while in the pause state
    //neccesary for when one switches from fullscreen to windowed or the other way around
    public void ResetPositions()
    {
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2, (GameEnvironment.Screen.Y - background.Height) / 2);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.Y - continueButton.Height) / 2 - 100);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - exitButton.Width) / 2, (GameEnvironment.Screen.Y - exitButton.Height) / 2 + 100);
        continueButtonMouseOver.Position = continueButton.Position - new Vector2(20,0);
        exitButtonMouseOver.Position = exitButton.Position - new Vector2(10,0);
    }

    /// <summary>
    /// React to the input
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        inputHelper.Update();

        //Check if the exitButton is being pressed to go back to the title screen
        if (exitButton.ButtonIsPressed)
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");

        //Check if the resumeButton is being pressed to go back to the game
        if (continueButton.ButtonIsPressed || inputHelper.KeyPressed(Keys.Escape))
        {
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.GameStateManager.SwitchTo("playingState");
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
    /// Draw the menu with the game in the background
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