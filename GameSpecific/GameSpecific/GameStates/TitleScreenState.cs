using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

class TitleScreenState : GameState
{
    protected Button playButton;

    public TitleScreenState()
    {
        //Add a background sprite
        Object2D background = new Object2D("White Sprite", 0);
        gameObjects.Add(background);

        //Add a start button
        playButton = new Button("White Sprite", 0);
        playButton.Position = new Vector2((GameEnvironment.Screen.X - playButton.Width) / 2, (GameEnvironment.Screen.X - playButton.Width) / 2);
        gameObjects.Add(playButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        
        base.HandleInput(inputHelper);
        inputHelper.Update();

        //Check if the playbutton is being pressed to go to the game
        if (playButton.ButtonIsPressed)
        {
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.GameStateManager.SwitchTo("playingState");

        }
        
    }
  
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Draw the Menu
        spriteBatch.Begin();
        base.Draw(gameTime, spriteBatch);
        spriteBatch.End();

        //Go back to 3D mode
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }
}
