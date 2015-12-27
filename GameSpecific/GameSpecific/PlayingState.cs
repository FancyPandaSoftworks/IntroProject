using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


class PlayingState : Root
{
    Level level;
    protected int roomCounter;
    
    public PlayingState(int roomCounter = 1)
    {
        this.roomCounter = roomCounter;
        level = new Level(this.roomCounter);
        roomCounter++;
    }

    public void HandleInput(InputHelper inputhelper)
    {
        level.Find("player").HandleInput(inputhelper);
        if (inputhelper.KeyPressed(Keys.R))
<<<<<<< HEAD
            level = new RandomLevel(20);
=======
        {
            level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            roomCounter++;
        }
>>>>>>> origin/master

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