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
    //public Point tilePosition; //TODO: GETS WORKED OUT #ENGLISHSKILLS

    public Tile(string modelName, string id, TileType type = TileType.Empty) : base(modelName, id)
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
    public PathTile(string modelName = "box", string id = "PathTile")
        : base(modelName, id, TileType.Path)
    {
        
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int layers = 1;
        base.Draw(gameTime, spriteBatch);
        Position = new Vector3(Position.X, Position.Y + (200 * (layers + 1)), Position.Z);
        base.Draw(gameTime, spriteBatch);
        Position = new Vector3(Position.X, Position.Y - (200 * (layers + 1)), Position.Z);
    }
}

class WallTile : Tile
{
    public WallTile() 
        : base("box","WallTile",TileType.Wall)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int layers = 1;
        for (int i = 0; i < layers; i++)
        {
            base.Draw(gameTime, spriteBatch);
            Position = new Vector3(Position.X, Position.Y + 200, Position.Z);
        }
        Position = new Vector3(Position.X, Position.Y - (200 * layers), Position.Z);
    }
}
class EntryTile : PathTile
{
    public EntryTile()
        : base()
    {

    }
}

class ExitTile : PathTile
{
    TextGameObject text;

    public ExitTile()
        : base("Axis", "ExitTile")
    {
        text = new TextGameObject("text");
        text.Position = Vector2.Zero;
        text.text = "Press E to proceed";
    }

    public override void Update(GameTime gameTime)
    {
        Level level = parent as Level;
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
        Level level = parent as Level;
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