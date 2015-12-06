using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum TileType
{
    Wall,
    Path,
    Empty
}

class Tile : Object3D
{
    protected TileType type;

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

//class EntryTile : Tile
//{
//    public EntryTile()
//    {

//    }
//}

//class ExitTile : Tile
//{
//    public ExitTile(Point point)
//    {
//        gridPosition = point;
//    }
//}

//class MainPathTile : Tile
//{
//    public MainPathTile(Point point)
//    {
//        gridPosition = point;
//    }
//}

//class SidePathEntryTile : Tile
//{
//    public SidePathEntryTile(Point point)
//    {
//        gridPosition = point;
//    }
//}

//class SidePathTile : Tile
//{
//    public SidePathTile(Point point)
//    {
//        gridPosition = point;
//    }
//}