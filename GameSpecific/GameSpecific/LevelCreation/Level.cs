using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;

class Level : GameObjectList
{
    protected Player player;
    protected bool completed;
    public Monster monster;

    public Level()
    {
        completed = false;
        if (!(this is RandomLevel))
        {
            player = new Player(Vector3.Zero);
            gameObjects.Add(player);
        }
    }

    public override void HandleInput(InputHelper inputhelper)
    {
        Find("player").HandleInput(inputhelper);
        if (inputhelper.KeyPressed(Keys.R))
        {
            Completed = true;
        }
    }

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
                    }
                }
            }

            else
                obj.Update(gameTime);
        }
            
    }

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
            
            if (gameObject is GameObjectGrid)
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
        }        
    }

    public bool Completed
    {
        get { return completed; }
        set { completed = value; }
    }
}
