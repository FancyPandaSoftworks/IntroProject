using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class TextGameObject : GameObject
{
    SpriteFont spriteFont;
    Color color;
    string text;
    

    public TextGameObject(string assetName, string id = "")
    {
        spriteFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>(assetName);
        color = Color.White;

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

        if (visible)
            spriteBatch.DrawString(spriteFont, text, Vector2.Zero, color);
          
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
}
