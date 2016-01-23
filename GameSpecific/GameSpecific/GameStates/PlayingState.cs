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
    public static int chaseCounter; //integer for the amount of rooms the monster will be chasing you

    public int RoomCounter
    {
        get { return roomCounter; }
        set { roomCounter = value - 1; level.Completed = true; }
    }

    /// <summary>
    /// Creates the random level and makes the Level the parent of the GameObjects in the level 
    /// </summary>
    /// <param name="roomCounter">The Roomcounter from where to start</param>
    public PlayingState(int roomCounter = 100)
    {
        this.roomCounter = roomCounter;
        level = new RandomLevel(this.roomCounter);
        Save(roomCounter, "Content\\SaveFile.txt");
        //level = new MultipleExitLevel();
        foreach (GameObject obj in level.Objects)
        {
            obj.Parent = level;
        }
        chaseCounter = 0;
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
            int tiles = 12 + (((roomCounter + 1) / 10) - ((roomCounter + 1) % 10));

            //First room
            if (roomCounter == 1)
                level = new SpecialLevel(roomCounter, "Content\\Special Levels\\First.txt");

            //Save rooms 
            else if (roomCounter % 50 == 0 && roomCounter != 100)
            {                
                Save(roomCounter, "Content\\SaveFile.txt");
                level = new SpecialLevel(roomCounter, "Content\\Special Levels\\CheckPoint.txt", true);
            }

            //Final level
            else if (roomCounter == 100)
                level = new SpecialLevel(roomCounter, "Content\\Special Levels\\Final.txt");

            //Rooms 31 - 39: 1/4 chance of a monster chasing you
            else if (roomCounter > 30 && roomCounter < 40 && chaseCounter == 0)
            {
                if (GameEnvironment.Random.Next(4) == 0)
                    level = new RandomLevel(roomCounter, tiles, true);
                else
                    level = new RandomLevel(roomCounter, tiles);
            }
            //Rooms 41 - 50: 1/3 chance of monster chasing you
            else if (roomCounter > 40 && roomCounter < 51 && chaseCounter == 0)
            {
                if (GameEnvironment.Random.Next(3) == 0)
                    level = new RandomLevel(roomCounter, tiles, true);
                else
                    level = new RandomLevel(roomCounter, tiles);
            }
            //Rooms 61 - 75: 1/2 chance of monster chasing you
            else if (roomCounter > 60 && roomCounter < 76 && chaseCounter == 0)
            {
                if (GameEnvironment.Random.Next(2) == 0)
                    level = new RandomLevel(roomCounter, tiles, true);
                else
                    level = new RandomLevel(roomCounter, tiles);
            }
            //Rooms 81 - 90: monster is chasing you
            else if (roomCounter > 80 && roomCounter < 91)
            {
                level = new RandomLevel(roomCounter, tiles, true);
            }
            //Making the monster chase you for 6/8/10 rooms (at most)
            else if (roomCounter > 30 && roomCounter < 40 && chaseCounter > 0 && chaseCounter < 7)
            {
                level = new RandomLevel(roomCounter, tiles, true);
                chaseCounter++;
            }
            else if (roomCounter > 40 && roomCounter < 51 && chaseCounter > 0 && chaseCounter < 9)
            {
                level = new RandomLevel(roomCounter, tiles, true);
                chaseCounter++;
            }
            else if (roomCounter > 60 && roomCounter < 76 && chaseCounter > 0 && chaseCounter < 11)
            {
                level = new RandomLevel(roomCounter, tiles, true);
                chaseCounter++;
            }
            //Levels with no monster
            else
            {
                level = new RandomLevel(roomCounter, tiles);
                chaseCounter = 0;
            }

            //Levels with notes (last parameter is the note ID)
            if (roomCounter == 2)
                level = new RandomLevel(roomCounter, tiles, false, 1);
            if (roomCounter == 3)
                level = new RandomLevel(roomCounter, tiles, false, 2);
            if (roomCounter == 5)
                level = new RandomLevel(roomCounter, tiles, false, 3);
            if (roomCounter == 20)
                level = new RandomLevel(roomCounter, tiles, false, 4);
            if (roomCounter == 30)
                level = new RandomLevel(roomCounter, tiles, false, 5);
            if (roomCounter == 51)
                level = new RandomLevel(roomCounter, tiles, false, 6);
            if (roomCounter == 55)
                level = new RandomLevel(roomCounter, tiles, false, 7);
            if (roomCounter == 76)
                level = new RandomLevel(roomCounter, tiles, false, 8);
            if (roomCounter == 77)
                level = new RandomLevel(roomCounter, tiles, false, 9);
            if (roomCounter == 78)
                level = new RandomLevel(roomCounter, tiles, false, 10);
            if (roomCounter == 79)
                level = new RandomLevel(roomCounter, tiles, false, 11);
            if (roomCounter == 90)
                level = new RandomLevel(roomCounter, tiles, false, 12);
            if (roomCounter == 97)
                level = new RandomLevel(roomCounter, tiles, false, 13);
            if (roomCounter == 98)
                level = new RandomLevel(roomCounter, tiles, false, 14);
            if (roomCounter == 99)
                level = new RandomLevel(roomCounter, tiles, false, 15);

            //set a boolean for the final level
            if (roomCounter == 100)
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