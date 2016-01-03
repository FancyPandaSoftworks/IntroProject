using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class PauseScreenState : GameObjectList
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

    public override void HandleInput(InputHelper inputHelper)
    {
        inputHelper.Update();
        base.HandleInput(inputHelper);

        //Check if the resumeButton is being pressed to go back to the game
        if (resumeButton.ButtonIsPressed)
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        if (exitButton.ButtonIsPressed)
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");
    }

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