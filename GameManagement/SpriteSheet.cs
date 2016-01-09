using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class SpriteSheet
{
    protected Texture2D sprite;
    protected int sheetIndex;
    protected int sheetColumns;
    protected int sheetRows;
    protected bool mirror;

    public SpriteSheet(string assetname = "", int sheetIndex = 0)
    {
        sprite = GameEnvironment.AssetManager.GetSprite(assetname);
        this.sheetIndex = sheetIndex;
        this.sheetColumns = 1;
        this.sheetRows = 1;

        //extract number of sheet elements from the assetname
        string[] assetSplit = assetname.Split('@');
        if (assetSplit.Length <= 1)
            return;

        string sheetnrData = assetSplit[assetSplit.Length - 1];
        string[] colrow = sheetnrData.Split('x');
        this.sheetColumns = int.Parse(colrow[0]);
        if (colrow.Length == 2)
            this.sheetRows = int.Parse(colrow[1]);
    }

    /// <summary>
    /// Draw the sprite
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch</param>
    /// <param name="position">The position to draw the sprite</param>
    /// <param name="origin">The origin within the sprite</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        int columnIndex = sheetIndex % sheetColumns;
        int rowIndex = sheetIndex / sheetColumns % sheetRows;
        Rectangle spritePart = new Rectangle(columnIndex * this.Width, rowIndex * this.Height, this.Width, this.Height);
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (mirror)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }

        spriteBatch.Draw(sprite, position, spritePart, Color.White, 0.0f, origin, 1.0f, spriteEffects, 0.0f);
    }

    /// <summary>
    /// Get the color for pixels
    /// </summary>
    /// <param name="x">The x-coördinate</param>
    /// <param name="y">The y-coördinate</param>
    /// <returns>Returns the color of the defined pixel</returns>
    public Color GetPixelColor(int x, int y)
    {
        int column_index = sheetIndex % sheetColumns;
        int row_index = sheetIndex / sheetColumns % sheetRows;
        Rectangle sourceRectangle = new Rectangle(column_index * this.Width + x, row_index * this.Height + y, 1, 1);
        Color[] retrievedColor = new Color[1];
        sprite.GetData<Color>(0, sourceRectangle, retrievedColor, 0, 1);
        return retrievedColor[0];
    }

    /// <summary>
    /// Property to get the sprite
    /// </summary>
    public Texture2D Sprite
    {
        get { return sprite; }

    }

    /// <summary>
    /// Property to get the center of the spritesheet
    /// </summary>
    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    /// <summary>
    /// Property to get the center of the spritesheet
    /// </summary>
    public int Width
    {
        get { return sprite.Width / sheetColumns; }
    }

    /// <summary>
    /// Property to get the height of the spritesheet
    /// </summary>
    public int Height
    {
        get { return sprite.Height / sheetRows; }
    }

    /// <summary>
    /// Property to get the mirror status or mirror the sprite
    /// </summary>
    public bool Mirror
    {
        get { return mirror; }
        set { mirror = value; }
    }

    /// <summary>
    /// Property for the sheetindex
    /// </summary>
    public int SheetIndex
    {
        get { return this.sheetIndex; }
        set
        {
            if (value < this.sheetColumns * this.sheetRows && value >= 0)
                this.sheetIndex = value;
        }

    }

    /// <summary>
    /// Property to get the total of the elements in the spritesheet
    /// </summary>
    public int NumberSheetElements
    {
        get { return this.sheetColumns * this.sheetRows; }
    }
}

