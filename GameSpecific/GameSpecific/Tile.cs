using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    public PathTile()
        : base("box", "MainPath", TileType.Path)
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
        : base("box", "WallTile", TileType.Wall)
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
    public ExitTile()
        : base()
    {

    }
}

class SidePathEntryTile : PathTile
{
    public SidePathEntryTile()
        : base()
    {

    }
}
