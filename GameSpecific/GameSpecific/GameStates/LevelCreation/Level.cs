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
    protected TextGameObject exitText, roomCounter;

    public Level(Player player = null)
    {
        completed = false;
        if (player != null)
        {
            this.player = player;
        }

        if (/* (GameEnvironment.Random.Next(0, 3) == 1) && */ NoteObject.idList.Count != 0) 
        { 
            NoteObject note = new NoteObject(NoteObject.idList[0]);
            note.Parent = this;
            //NoteObject.idList.Remove(NoteObject.idList[0]);
            gameObjects.Add(note);
        }

        exitText = new TextGameObject("text");
        exitText.Position = Vector2.Zero;
    }
    
    /// <summary>
    /// Handleinput for the objects in the level
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.N))
        {
            Console.WriteLine("n");
            foreach (GameObject obj in gameObjects)
            {
                if (obj is NoteObject)
                {
                    NoteObject note = obj as NoteObject;
                    note.PickUp();
                }
            }
        }

        if (inputHelper.KeyPressed(Keys.R))
        {
            Completed = true;
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

        if(isOnExit)
        {
            exitText.Draw(gameTime, spriteBatch);
            isOnExit = false;
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
}
