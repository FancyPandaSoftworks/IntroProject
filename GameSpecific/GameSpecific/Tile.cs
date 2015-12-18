using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum TileType
{
    Wall,
    MainPath,
    SidePathEntry,
    Exit,
    EntryTile,
    SidePath,
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

class WallTile : Tile
{
    public WallTile(Point point) 
        : base("box","WallTile",TileType.Wall)
    {
        tilePosition = point;
    }
}
class EntryTile : Tile
{
    public EntryTile()
        : base("box", "EntryTile", TileType.EntryTile)
    {
        tilePosition = new Point(0, 0);
    }
}

class ExitTile : Tile
{
    public ExitTile(Point point)
        : base("box", "ExitTile", TileType.Exit)
    {
        tilePosition = point;
    }
}

class MainPathTile : Tile
{
    public MainPathTile(Point point)
        : base("box", "MainPath", TileType.MainPath)
    {
        tilePosition = point;
    }
}

class SidePathEntryTile : Tile
{
    public SidePathEntryTile(Point point)
        : base("box", "SidePathEntryTile", TileType.SidePathEntry)
    {
        tilePosition = point;
    }
}

class SidePathTile : Tile
{
    public SidePathTile(Point point)
        : base("box", "SidePath", TileType.SidePath)
    {
        tilePosition = point;
    }
}