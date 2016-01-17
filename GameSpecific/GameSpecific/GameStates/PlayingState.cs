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
    public int roomCounter;

    public int RoomCounter
    {
        get { return roomCounter; }
        set { roomCounter = value - 1; level.Completed = true; }
    }

    /// <summary>
    /// Creates the random level and makes the Level the parent of the GameObjects in the level 
    /// </summary>
    /// <param name="roomCounter">The Roomcounter from where to start</param>
    public PlayingState(int roomCounter = 230)
    {
        this.roomCounter = roomCounter;
        level = new RandomLevel(this.roomCounter);
        Save(roomCounter, "Content\\SaveFile.txt");
        //level = new MultipleExitLevel();
        foreach (GameObject obj in level.Objects)
        {
            obj.Parent = level;
        }
    }

    /// <summary>
    /// HandleInput for the level
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.Escape))
        {
            foreach (Sound sound in MusicPlayer.SoundEffect)
                if (sound.Name == "paperrustle")
                    sound.PlaySound();
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
        MusicPlayer.dangerLevel = 0;

        if (level.Completed)
        {
            roomCounter++;
            Console.WriteLine(roomCounter);
            //First room
            if (roomCounter == 1)
                level = new SpecialLevel(roomCounter, "Content\\Special Levels\\First.txt");

            //Save rooms 
            else if (roomCounter % 50 == 0 && roomCounter != 250)
            {
                level = new SpecialLevel(roomCounter, "Content\\Special Levels\\CheckPoint.txt");
                Save(roomCounter, "Content\\SaveFile.txt");
            }

            //Final level
            else if (roomCounter == 250)
            {
                level = new SpecialLevel(roomCounter, "Content\\Special Levels\\Final.txt");
            }

            //Rooms 61 - 80: 1/5 chance of a monster chasing you
            else if (roomCounter > 60 && roomCounter < 81)
            {
                if (GameEnvironment.Random.Next(5) == 0)
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)), true);
                else
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            }
            //Rooms 101 - 125: 1/4 chance of monster chasing you
            else if (roomCounter > 100 && roomCounter < 126)
            {
                if (GameEnvironment.Random.Next(4) == 0)
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)), true);
                else
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            }
            //Rooms 151 - 190: 1/3 chance of monster chasing you
            else if (roomCounter > 150 && roomCounter < 191)
            {
                if (GameEnvironment.Random.Next(3) == 0)
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)), true);
                else
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            }
            //Rooms 211 - 240: 1/2 chance of monster chasing you
            else if (roomCounter > 210 && roomCounter < 241)
            {
                if (GameEnvironment.Random.Next(2) == 0)
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)), true);
                else
                    level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));
            }
            //Levels with no monster
            else
                level = new RandomLevel(roomCounter, 20 + (((roomCounter - 1) / 4) - ((roomCounter - 1) % 4)));

            //set a boolean for the final level
            if (roomCounter == 250)
                ExitTile.finalLevel = true;
            else
                ExitTile.finalLevel = false;

            foreach (GameObject obj in level.Objects)
                obj.Parent = level;
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