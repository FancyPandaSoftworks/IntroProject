using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


class PlayingState : Root
{
    Level level;
    
    public PlayingState()
    {
        level = new Level();
    }

    public void HandleInput(InputHelper inputhelper)
    {
        level.Find("player").HandleInput(inputhelper);
        if (inputhelper.KeyPressed(Keys.R))
            level = new RandomLevel(20);

    }

    public void Update(GameTime gameTime)
    {
        level.Update(gameTime);
        
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
    }
}