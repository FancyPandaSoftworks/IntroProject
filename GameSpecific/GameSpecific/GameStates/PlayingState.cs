using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System;


class PlayingState : Root
{
    Level level;
    protected int roomCounter;
    
    //Creates the random level and makes the Level the parent of the GameObjects in the level 
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

    //HandleInput for level
    public void HandleInput(InputHelper inputHelper)
    {
        inputHelper.Update();
        if (inputHelper.IsKeyDown(Keys.Escape))
        {
            GameEnvironment.GameStateManager.SwitchTo("pauseScreenState");
        }
        level.HandleInput(inputHelper);
    }

    //updating the level
    public void Update(GameTime gameTime)
    {

        if (level.Completed)
        {
            roomCounter++;
            
            //Random level 
            if (roomCounter % 50 != 0)
                level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            
            //Final level
            else if (roomCounter == 250)
            {
                level = new SpecialLevel(roomCounter, "Final.txt");
            }
           
            //Every 50 levels
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

    //Draws the level
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
    }

    //Saves the level
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