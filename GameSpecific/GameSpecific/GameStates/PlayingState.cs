using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>
/// The state the game is in while playing
/// </summary>
class PlayingState : Root
{
    Level level;
    protected int roomCounter;
    
    /// <summary>
    /// Creates the random level and makes the Level the parent of the GameObjects in the level 
    /// </summary>
    /// <param name="roomCounter">The Roomcounter from where to start</param>
    public PlayingState(int roomCounter = 1)
    {
        this.roomCounter = roomCounter;
//        level = new RandomLevel(this.roomCounter);
        level = new MultipleExitLevel();
        foreach (GameObject obj in level.Objects)
        {
            obj.Parent = level;
        }
        roomCounter++;
    }

    /// <summary>
    /// HandleInput for the level
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.Escape))
        {
            GameEnvironment.GameStateManager.SwitchTo("pauseScreenState");
        }

        level.HandleInput(inputHelper);
    }

    /// <summary>
    /// Updating the level
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
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

    /// <summary>
    /// Draws the level
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Saves the level
    /// </summary>
    /// <param name="room">What room you are in, representing the roomnumber</param>
    /// <param name="path">The path where to save the file</param>
    public void Save(int room, string path)
    {
        using (StreamWriter fileWriter = new StreamWriter(path, false))
        {
            string line = room.ToString();
            fileWriter.WriteLine(line);
            fileWriter.Close();
        }
    }

    public void Reset() { }
}