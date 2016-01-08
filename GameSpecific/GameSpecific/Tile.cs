using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

enum TileType
{
    Wall,
    Path,
    SidePathEntry,
    Exit,
    EntryTile,
    Empty
}

abstract class Tile : Object3D
{
    protected TileType type;

    public Tile(string modelName, string id, TileType type = TileType.Empty)
        : base(modelName, id)
    {
        this.type = type;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (type == TileType.Empty)
            return;
        base.Draw(gameTime, spriteBatch);
    }
}

class PathTile : Tile
{
    public PathTile(string modelName = "Wall Cubes\\Wall Cube 01", string id = "PathTile")
        : base(modelName, id, TileType.Path)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int layers = 1;

        //Draw the Path
        base.Draw(gameTime, spriteBatch);

        //Exclusively draw the ceilling, no update happens
        Position = new Vector3(Position.X, Position.Y + (200 * (layers + 1)), Position.Z);
        base.Draw(gameTime, spriteBatch);

        //Revert to original position to update the ground
        Position = new Vector3(Position.X, Position.Y - (200 * (layers + 1)), Position.Z);
    }
}

class WallTile : Tile
{
    public WallTile()
        : base("Wall Cubes\\Wall Cube 01", "WallTile", TileType.Wall)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int layers = 1;
        //Draw the entire height of the wall
        for (int i = 0; i < layers; i++)
        {
            base.Draw(gameTime, spriteBatch);
            Position = new Vector3(Position.X, Position.Y + 200, Position.Z);
        }

        //Revert to the original height
        Position = new Vector3(Position.X, Position.Y - (200 * layers), Position.Z);
    }
}

class EntryTile : PathTile
{
    public EntryTile()
        : base("Wall Cubes\\Wall Cube 01", "EntryTile")
    {

    }
}

class ExitTile : PathTile
{
    TextGameObject text;

    public ExitTile()
        : base("Wall Cubes\\Wall Cube 01", "ExitTile")
    {
        text = new TextGameObject("text");
        text.Position = Vector2.Zero;
        text.text = "Press E to proceed";
    }

    public override void Update(GameTime gameTime)
    {
        //Check if the player is in the ExitTile and if so, if they are pressing E to procceed
        Level level = parent.Parent as Level;
        foreach(GameObject obj in level.Objects)
        {
            if(obj != null)
            {
                if (obj.ID == "player")
                {
                    if (obj.Position.X > Position.X - 100 && obj.Position.X < Position.X + 100 && obj.Position.Z > Position.Z - 100 && obj.Position.Z < Position.Z + 100)
                    {
                        Player player = obj as Player;
                        if(player.EDown == true)
                            level.Completed = true;
                        
                    }
                }
            } 
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        //Draw text if the player is on the ExitTile
        Level level = parent.Parent as Level;
        foreach (GameObject obj in level.Objects)
        {
            if (obj != null)
            {
                if (obj.ID == "player")
                {
                    if (obj.Position.X > Position.X - 100 && obj.Position.X < Position.X + 100 && obj.Position.Z > Position.Z - 100 && obj.Position.Z < Position.Z + 100)
                    {
                        text.Draw(gameTime, spriteBatch);
                    }
                }
            }
        }
        
    }
}

class SidePathEntryTile : PathTile
{
    public SidePathEntryTile()
        : base()
    {

    }
}
