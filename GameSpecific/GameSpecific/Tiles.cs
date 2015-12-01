using Microsoft.Xna.Framework;

abstract class Tile
{
    public Point tilePosition;
}

class EntryTile : Tile
{
    public EntryTile()
    {

    }
}

class ExitTile : Tile
{
    public ExitTile(Point point)
    {
        tilePosition = point;
    }
}

class MainPathTile : Tile
{
    public MainPathTile(Point point)
    {
        tilePosition = point;
    }
}

class SidePathEntryTile : Tile
{
    public SidePathEntryTile(Point point)
    {
        tilePosition = point;
    }
}

class SidePathTile : Tile
{
    public SidePathTile(Point point)
    {
        tilePosition = point;
    }
}