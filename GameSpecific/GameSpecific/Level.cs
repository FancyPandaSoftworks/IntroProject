using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

class Level : GameObjectList
{    
    public Level()

    {
        Player player = new Player(Vector3.Zero);
        gameObjects.Add(player);
        TileGrid tileGrid = new TileGrid(3, 3, "TileGrid");
        gameObjects.Add(tileGrid);
        Create(tileGrid);
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
                    gameObject3D.Draw(gameTime, spriteBatch);
            }               
        }        
    }

    public TileGrid Create(TileGrid tileGrid)
    {
        for(int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                tileGrid.Add(new Tile("box", "Tile", TileType.Wall), x, y);
            }
        }
        return tileGrid;    
    }
}
