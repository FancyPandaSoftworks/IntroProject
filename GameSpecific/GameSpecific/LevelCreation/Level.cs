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
           
            player = new Player(Vector3.Zero);
            gameObjects.Add(player);
            TileGrid tileGrid = new TileGrid(6, 6, "TileGrid");
            Create(tileGrid);
            gameObjects.Add(tileGrid);
        }
        
    }

    /*
    protected Level()
    {

    }
    */

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
