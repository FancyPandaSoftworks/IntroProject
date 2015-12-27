using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

class GameOverState : Root
{

    public GameOverState()
    {

    }

    public void HandleInput(InputHelper inputhelper)
    {
        
        if (inputhelper.KeyPressed(Keys.Space))
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

    }

    public void Update(GameTime gameTime)
    {

        
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
       
    }
    
}