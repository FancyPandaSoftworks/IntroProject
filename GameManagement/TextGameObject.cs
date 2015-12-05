using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



    class TextGameObject : GameObject
    {
        SpriteFont spriteFont;
        Color color;
        string text;


        public TextGameObject(string assetName)
        {
            spriteFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>(assetName);
            color = Color.White;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        
            if (visible)
                spriteBatch.DrawString(spriteFont, text, this.position, color);
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

