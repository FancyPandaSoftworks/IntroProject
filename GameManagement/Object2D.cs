using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// A 2D object
/// </summary>
public class Object2D : GameObject
{
    protected Vector2 origin, position;
    protected SpriteSheet spriteSheet;
    Texture2D player;

    public Object2D(string assetname, int sheetIndex = 0, string id = "")
    {
        if (assetname != "")
            spriteSheet = new SpriteSheet(assetname, sheetIndex);          
    }

    /// <summary>
    /// Drawing the object
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || spriteSheet == null)
            return;

        spriteSheet.Draw(spriteBatch, Position, origin);      
    }

    /// <summary>
    /// Property for the position on the screen
    /// </summary>
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    /// <summary>
    /// Property to get the spritesheet
    /// </summary>
    public SpriteSheet SpriteSheet
    {
        get { return spriteSheet; }
    }

    /// <summary>
    /// Property to get the spriteSheet width
    /// </summary>
    public int Width
    {
        get { return spriteSheet.Width; }
    }

    /// <summary>
    /// Property to get the spriteSheet height
    /// </summary>
    public int Height
    {
        get { return spriteSheet.Height; }
    }

    /// <summary>
    /// Property to mirror the object or check wheter it is already mirrored
    /// </summary>
    public bool Mirror
    {
        get { return spriteSheet.Mirror; }
        set { spriteSheet.Mirror = value; }
    }

    /// <summary>
    /// Property for the origin of the spritesheet, in this case the middle
    /// </summary>
    public Vector2 Origin
    {
        get { return this.origin; }
        set { this.origin = value; }
    }
    
    /// <summary>
    /// Property to get the boundingbox for collision
    /// </summary>
    public  Rectangle BoundingBox
    {
        get
        {
            int left = (int)(Position.X - origin.X);
            int top = (int)(Position.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    /// <summary>
    /// Collision of object2D with pixels
    /// </summary>
    /// <param name="obj2D">The object to check collision with</param>
    /// <returns>True if it collides, false if it doesn't</returns>
    public bool CollidesWith(Object2D obj2D)
    {
            if (!this.Visible || !obj2D.Visible || !BoundingBox.Intersects(obj2D.BoundingBox))
                return false;
            Rectangle b = Collision.Intersection(BoundingBox, obj2D.BoundingBox);
            for(int x = 0; x<b.Width; x++)
                for(int y = 0; y<b.Height; y++)
                {
                    int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                    int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                    int objx = b.X - (int)(obj2D.GlobalPosition.X - obj2D.origin.X) + x;
                    int objy = b.Y - (int)(obj2D.GlobalPosition.Y - obj2D.origin.Y) + y;
                    if (SpriteSheet.GetPixelColor(thisx, thisy).A != 0 && obj2D.spriteSheet.GetPixelColor(objx, objy).A != 0)
                        return true;
                } 
            return false;  
           
    }
}
