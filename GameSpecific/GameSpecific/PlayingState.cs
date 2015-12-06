using Microsoft.Xna.Framework;
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
    }

    public void Update(GameTime gameTime)
    {
        level.Update(gameTime);
        if (false)
            level = new Level();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
    }
}