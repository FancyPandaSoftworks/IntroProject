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
    public Point tilePosition; //TODO: GETS WORKED OUT #ENGLISHSKILLS

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
    public PathTile(Point point)
        : base("box", "MainPath", TileType.Path)
    {
        tilePosition = point;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int layers = 6;
        base.Draw(gameTime, spriteBatch);
        Position = new Vector3(Position.X, Position.Y + (200 * (layers + 1)), Position.Z);
        base.Draw(gameTime, spriteBatch);
        Position = new Vector3(Position.X, Position.Y - (200 * (layers + 1)), Position.Z);
    }
}

class WallTile : Tile
{
    public WallTile(Point point) 
        : base("box","WallTile",TileType.Wall)
    {
        tilePosition = point;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int layers = 6;
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
        : base(new Point(0,0))
    {
        tilePosition = new Point(0, 0);
    }
}

class ExitTile : PathTile
{
    public ExitTile(Point point)
        : base(point)
    {
        tilePosition = point;
    }
}

class SidePathEntryTile : PathTile
{
    public SidePathEntryTile(Point point)
        : base(point)
    {
        tilePosition = point;
    }
}