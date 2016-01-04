using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

class NoteViewingState : Root
{
    private string noteID;
    private bool noteChanged;
    private Texture2D noteSprite;

    public NoteViewingState()
    {
        NoteObject.idList = new System.Collections.Generic.List<string>();
        StreamReader streamReader = new StreamReader("Content/noteIDList.txt");
        string line = streamReader.ReadLine();
        while (line != null)
        {
            NoteObject.idList.Add(line);
            line = streamReader.ReadLine();
        }
    }

    public string NoteID
    {
        set { noteID = value; noteChanged = true; }
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Space))
        {
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
    }

    public void Update(GameTime gameTime)
    {
        if (noteChanged)
        {
            try{
            noteSprite = GameEnvironment.AssetManager.GetSprite(noteID);
            }
            catch
            {

            }

            noteChanged = false;
        }
    } 

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

    public void Reset()
    {
        noteID = null;
    }
}
