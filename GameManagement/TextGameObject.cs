using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

    //draws the text
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

        if (visible)
            spriteBatch.DrawString(spriteFont, text, Vector2.Zero, color);
          
        spriteBatch.Begin();
        spriteBatch.DrawString(spriteFont, text, this.position, color);
        spriteBatch.End();
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }

    //returns and gives the text a color
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    //returns the size of the text
    public Vector2 Size
    {
        get { return spriteFont.MeasureString(text); }
    }

    //returns the position and gives the text a position
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
}
