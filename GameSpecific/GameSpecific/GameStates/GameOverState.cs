using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The state the game is in when you have died
/// </summary>
class GameOverState : GameState
{
    protected Button continueButton, exitButton;
    protected Object2D background;

    public GameOverState()
    {
        background = new Object2D("White Sprite", 0);
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2, (GameEnvironment.Screen.Y - background.Height) / 2);
        gameObjects.Add(background);

        //add a resume button
        continueButton = new Button("White Sprite", 0);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.Y - continueButton.Height) / 2 - 100);
        gameObjects.Add(continueButton);

        //add an exit button
        exitButton = new Button("White Sprite", 0);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - exitButton.Width) / 2, (GameEnvironment.Screen.Y - exitButton.Height) / 2 + 100);
        gameObjects.Add(exitButton);
    }

    //method for resetting the positions of the buttons and background
    //neccesary for when one switches from fullscreen to windowed or the other way around
    public void ResetPositions()
    {
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2, (GameEnvironment.Screen.Y - background.Height) / 2);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.Y - continueButton.Height) / 2 - 100);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - exitButton.Width) / 2, (GameEnvironment.Screen.Y - exitButton.Height) / 2 + 100);
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
            //TODO: START FROM LAST CHECKPOINT
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

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
