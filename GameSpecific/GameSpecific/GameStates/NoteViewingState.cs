using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The state the game is in while reading a note
/// </summary>
class NoteViewingState : Root
{
    private string noteID;
    private bool noteChanged;
    private Texture2D noteSprite;

    public NoteViewingState()
    {
        NoteObject.idList = new System.Collections.Generic.List<string>();
        StreamReader streamReader = new StreamReader("noteIDList.txt");
        string line = streamReader.ReadLine();
        while (line != null)
        {
            NoteObject.idList.Add(line);
            line = streamReader.ReadLine();
        }
    }

    /// <summary>
    /// The ID of the note, which note it is that needs to be shown
    /// </summary>
    public string NoteID
    {
        set { noteID = value; noteChanged = true; }
    }

    /// <summary>
    /// Reacting to input from the player
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.AnyKeyPressed)
        {
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
    }

    public void Update(GameTime gameTime)
    {
        if (noteChanged)
        {
            try
            {
                noteSprite = GameEnvironment.AssetManager.GetSprite(noteID);
            }
            catch
            {

            }

            noteChanged = false;
        }
    } 

    /// <summary>
    /// Draw the note on the screen with the Game in the Background
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        GameEnvironment.GameStateManager.GetGameState("playingState").Draw(gameTime, spriteBatch);
        
        //Draw the Pausescreen
        spriteBatch.Begin();
        //spriteBatch.Draw(noteSprite, new Vector2((GameEnvironment.Screen.X - noteSprite.Width) / 2, (GameEnvironment.Screen.Y - noteSprite.Height) / 2), Color.White);
        if (noteSprite == null)
        {
            noteSprite = GameEnvironment.AssetManager.GetSprite("White Sprite");
        }
        spriteBatch.Draw(noteSprite, new Vector2((GameEnvironment.Screen.X - noteSprite.Width) / 2, (GameEnvironment.Screen.Y - noteSprite.Height) / 2), Color.White);
        spriteBatch.End();

        //Go back to 3D mode
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }

    /// <summary>
    /// Reset the Object to its state when it was build
    /// </summary>
    public void Reset()
    {
        noteID = null;
    }
}
