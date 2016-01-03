using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;


class PlayingState : Root
{
    Level level;
    protected int roomCounter;
    
    public PlayingState(int roomCounter = 1)
    {
        this.roomCounter = roomCounter;
        level = new RandomLevel(this.roomCounter);
        foreach (GameObject obj in level.Objects)
        {
            obj.Parent = level;
        }
        roomCounter++;
    }

    public void HandleInput(InputHelper inputHelper)
    {
        level.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        if (level.Completed)
        {
            roomCounter++;
            if (roomCounter % 50 != 0)
                level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            else if (roomCounter == 250)
            {
                level = new SpecialLevel(roomCounter, "Final.txt");
            }
            else
            {
                level = new SpecialLevel(roomCounter, "CheckPoint.txt");
                Save(roomCounter, "SaveFile.txt");
            }
            foreach (GameObject obj in level.Objects)
            {
                obj.Parent = level;
            }
        }
        level.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
    }

    public void Save(int room, string path)
    {
        StreamWriter fileWriter = new StreamWriter(path, false);
        string line = room.ToString();
        fileWriter.WriteLine(line);
        fileWriter.Close();
    }

    public void Reset()
    {

    }
}