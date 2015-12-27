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
            GameEnvironment.GameStateManage.SwitchTo("playingState");

        }

    }

    public void Update(GameTime gameTime)
    {
        Console.WriteLine("GameOverState");
        
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
       
        
    }

    public void Reset()
    {

    }
}