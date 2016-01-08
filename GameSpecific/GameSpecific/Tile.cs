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

/// <summary>
/// The standard things for a tile
/// </summary>
abstract class Tile : Object3D
{
    protected TileType type;

    /// <summary>
    /// Constructing the tile
    /// </summary>
    /// <param name="modelName">What model to use for the drawing of the tile</param>
    /// <param name="id">The id used to find this object</param>
    /// <param name="type">The type of tile</param>
    public Tile(string modelName, string id, TileType type = TileType.Empty)
        : base(modelName, id)
    {
        this.type = type;
    }

    /// <summary>
    /// Draw the tile
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (type == TileType.Empty)
            return;
        base.Draw(gameTime, spriteBatch);
    }
}

/// <summary>
/// A simple Tile
/// </summary>
class PathTile : Tile
{
    public PathTile(string modelName = "untitled", string id = "PathTile")
        : base(modelName, id, TileType.Path)
    {

    }

    /// <summary>
    /// Draw the tile
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
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

/// <summary>
/// A piece of the wall
/// </summary>
class WallTile : Tile
{
    public WallTile() 
        : base("untitled","WallTile",TileType.Wall)
    {
    }
    
    /// <summary>
    /// Drawing the tile
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
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

/// <summary>
/// The Entry
/// </summary>
class EntryTile : PathTile
{
    public EntryTile()
        : base("untitled", "EntryTile")
    {

    }
}

/// <summary>
/// The Exit
/// </summary>
class ExitTile : PathTile
{
    TextGameObject text;

    public ExitTile()
        : base("untitled", "ExitTile")
    {
        text = new TextGameObject("text");
        text.Position = Vector2.Zero;
        text.text = "Press E to proceed";
    }

    /// <summary>
    /// Updating the tile
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
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

    /// <summary>
    /// Drawing the tile
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
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

/// <summary>
/// The Entry to a sidePath
/// </summary>
class SidePathEntryTile : PathTile
{
    public SidePathEntryTile()
        : base()
    { }
}
