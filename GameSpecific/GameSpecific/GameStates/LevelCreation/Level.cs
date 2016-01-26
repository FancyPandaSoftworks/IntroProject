using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The standard for a level
/// </summary>
 class Level : GameObjectList
{
    public Player player;
    protected Stamina stamina;
    protected bool isOnExit, completed;
    protected TextGameObject exitText, roomCounter, noteText;


    public Level(Player player = null)
    {
        completed = false;
        if (player != null)
        {
            this.player = player;
        }

        exitText = new TextGameObject("text");
        noteText = new TextGameObject("text");
        noteText.text = "Press F to pick up the note";
    }
    
    /// <summary>
    /// Handleinput for the objects in the level
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.F) && NoteInVicinity)
        {
            foreach (GameObject obj in gameObjects)
            {
                if (obj is NoteObject)
                {
                    foreach (Sound sound in MusicPlayer.SoundEffect)
                        if (sound.Name == "paperrustle")
                            sound.PlaySound();
                    NoteObject note = obj as NoteObject;
                    note.PickUp();
                }
            }
        }

        Find("Player").HandleInput(inputHelper);        
    }

    /// <summary>
    /// Update each GameObject in the GameObjectGrid
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj is GameObjectGrid)
            {
                GameObjectGrid gameObjectGrid = obj as GameObjectGrid;
                foreach (GameObject gameObject in gameObjectGrid.Objects)
                {
                    if (gameObject != null)
                    {
                        gameObject.Update(gameTime);
                        if(gameObject is ExitTile)
                        {
                            ExitTile exitTile = gameObject as ExitTile;
                            if (exitTile.isOnTile)
                                isOnExit = true;
                        }
                    }
                }
            }
            else
                obj.Update(gameTime);
        }

        exitText.Position = new Vector2((GameEnvironment.Screen.X - 300) / 2, 0);
        noteText.Position = new Vector2((GameEnvironment.Screen.X - 300) / 2, 30);
        roomCounter.Position = new Vector2(GameEnvironment.Screen.X - 60, 10);
    }

    /// <summary>
    /// Draws the GameObjects of the list
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject is Object3D)
            {
                Object3D gameObject3D = gameObject as Object3D;
                if (gameObject3D.Model != null)
                {
                    gameObject3D.DrawCamera(player);
                    gameObject3D.Draw(gameTime, spriteBatch);
                }
            }

            else if (gameObject is GameObjectGrid)
            {
                GameObjectGrid gameObjectGrid = gameObject as GameObjectGrid;
                foreach (GameObject obj in gameObjectGrid.Objects)
                    if (obj is Object3D)
                    {
                        Object3D gameObject3D = obj as Object3D;
                        if (gameObject3D.Model != null)
                        {
                            gameObject3D.DrawCamera(player);
                            gameObject3D.Draw(gameTime, spriteBatch);
                        }
                    }
            }
            else
                gameObject.Draw(gameTime, spriteBatch);
        }

        if(isOnExit && !NoteInVicinity)
        {
            exitText.Draw(gameTime, spriteBatch);
            isOnExit = false;
        }
        else if (NoteInVicinity)
        {
            noteText.Draw(gameTime, spriteBatch);
        }
    }

    protected void CreateNote(NoteObject note, TileGrid tileGrid)
    {
        for (int x = 0; x < tileGrid.Columns; x++)
        {
            for (int y =0; y < tileGrid.Rows; y++)
            {
                if (tileGrid.Get(x, y) != null && tileGrid.Get(x, y) is WallTile)
                {
                    if(tileGrid.Get(x + 1, y) is PathTile) 
                    {
                        note.Position = tileGrid.Get(x, y).Position + new Vector3(102/*100+modelDepth*/, 0, 0);
                    }
                    else if(tileGrid.Get(x, y + 1) is PathTile)
                    {
                        note.Position = tileGrid.Get(x, y).Position + new Vector3(0, 0, 102);
                    }

                    else if(tileGrid.Get(x - 1, y) is PathTile)
                    {
                        note.Position = tileGrid.Get(x, y).Position - new Vector3(102, 0, 0);
                    }
                    else if(tileGrid.Get(x, y - 1)is PathTile)
                    {
                        note.Position = tileGrid.Get(x, y).Position - new Vector3(0, 0, 102);
                    }
                }       
            }
        }
    }

    /// <summary>
    /// Property to get and set whether the level is completed or not
    /// </summary>
    public bool Completed
    {
        get { return completed; }
        set { completed = value; }
    }

    public bool NoteInVicinity
    {
        get 
        {
            NoteObject note = Find("note") as NoteObject;
            if (note == null)
                return false;
            return ((player.Position.X >= note.Position.X - 100 && player.Position.X <= note.Position.X + 100)
                    && (player.Position.Z >= note.Position.Z - 100 && player.Position.Z <= note.Position.Z + 100));
        }
    }
}
