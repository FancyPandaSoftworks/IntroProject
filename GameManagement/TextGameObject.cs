using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A gameObject consisting of text
/// </summary>
public class TextGameObject : GameObject
{
    SpriteFont spriteFont;
    Color color;
    public string text;
    Vector2 position;

    public TextGameObject(string assetName, string id = "")
    {
        spriteFont = GameEnvironment.AssetManager.GetSpriteFont(assetName);
        color = Color.White;
        text = " ";
    }

    /// <summary>
    /// Draws the text
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {          
        spriteBatch.Begin();
        spriteBatch.DrawString(spriteFont, text, this.position, color);
        spriteBatch.End();
        spriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

    }

    /// <summary>
    /// Property to get and set the text color
    /// </summary>
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    /// <summary>
    /// Property to get the size of the Text
    /// </summary>
    public Vector2 Size
    {
        get { return spriteFont.MeasureString(text); }
    }

    /// <summary>
    /// Property to get and set the position of the text
    /// </summary>
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
}
