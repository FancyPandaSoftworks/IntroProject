using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class TitleScreenState : GameObjectList
{
    protected Button playButton;

    public TitleScreenState()
    {
        Object2D background = new Object2D("White Sprite", 0);
        gameObjects.Add(background);

        playButton = new Button("White Sprite", 0);
        playButton.Position = new Vector2((GameEnvironment.Screen.X - playButton.Width) / 2, 200);
        gameObjects.Add(playButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        inputHelper.Update();
        base.HandleInput(inputHelper);

        if (playButton.ButtonIsPressed)
            GameEnvironment.GameStateManager.SwitchTo("playingState");
    }
  
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        base.Draw(gameTime, spriteBatch);
        spriteBatch.End();
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }
}
