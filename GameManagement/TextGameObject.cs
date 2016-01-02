using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class TextGameObject : GameObject
{
    SpriteFont spriteFont;
    Color color;
    public string text;
    Vector2 position;

    public TextGameObject(string assetName)
    {
        spriteFont = GameEnvironment.AssetManager.GetSpriteFont(assetName);
        color = Color.White;
        text = " ";
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(spriteFont, text, this.position, color);
        spriteBatch.End();
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public Vector2 Size
    {
        get { return spriteFont.MeasureString(text); }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
}
