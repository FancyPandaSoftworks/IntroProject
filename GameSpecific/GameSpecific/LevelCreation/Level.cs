using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

 class Level : GameObjectList
{
    protected Player player;
    protected int roomNumber;
   

    public Level(int roomNumber)

    {
        
        if (!(this is RandomLevel))
        {
           
            
            TileGrid tileGrid = new TileGrid(6, 6, "TileGrid");
            Create(tileGrid);
            gameObjects.Add(tileGrid);
            player = new Player(Vector3.Zero);
            player.Parent = this;
            gameObjects.Add(player);
            AI ai = new AI("monsterTexture");
            ai.Parent = this;
            ai.LoadContent();
            gameObjects.Add(ai);
            
        }
        
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in gameObjects)
            obj.Update(gameTime);
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
            else if(gameObject is AI)
            {
                    gameObject.Draw(gameTime, spriteBatch);
                    
            }
                

        }        
    }

    public TileGrid Create(TileGrid tileGrid)
    {
        for(int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (y % 2 == 0 && x % 2 == 0 || y % 2 == 1 && x % 2 == 1)
                tileGrid.Add(new WallTile(), x, y);
            }
        }
        return tileGrid;    
    }
}
