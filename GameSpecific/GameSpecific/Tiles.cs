using Microsoft.Xna.Framework;

//TODO: Comments and fixing modelname(?) and id(?)


class EntryTile : Tile
{
    public EntryTile(Point position) : base (modelName,id,TileType.Path)
    {
        tilePosition = position;
    }
}

class ExitTile : Tile
{
    public ExitTile(Point point)
        : base(modelName, id, TileType.Path)
    {
        tilePosition = point;
    }
}

class MainPathTile : Tile
{
    public MainPathTile(Point point)
        : base(modelName, id, TileType.Path)
    {
        tilePosition = point;
    }
}

class SidePathEntryTile : Tile
{
    public SidePathEntryTile(Point point)
        : base(modelName, id, TileType.Path)
    {
        tilePosition = point;
    }
}

class SidePathTile : Tile
{
    public SidePathTile(Point point)
        : base(modelName, id, TileType.Path)
    {
        tilePosition = point;
    }
}