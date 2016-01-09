using System;
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
    protected Button resumeButton, exitButton;

    public PauseScreenState()
    {
        Object2D background = new Object2D("White Sprite", 0);
        background.Position = new Vector2((GameEnvironment.Screen.X - background.Width) / 2,
                                          (GameEnvironment.Screen.Y - background.Height) / 2);
        gameObjects.Add(background);

        //Add a resume button
        resumeButton = new Button("White Sprite", 0);
        resumeButton.Position = new Vector2((GameEnvironment.Screen.X - resumeButton.Width) / 2, (GameEnvironment.Screen.Y - resumeButton.Height) / 2 - 100);
        gameObjects.Add(resumeButton);

        exitButton = new Button("White Sprite", 0);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - exitButton.Width) / 2, (GameEnvironment.Screen.Y - exitButton.Height) / 2 + 100);
        gameObjects.Add(exitButton);
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
        if (resumeButton.ButtonIsPressed)
        {
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

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
    }
}