using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface Root
{
    void HandleInput(InputHelper inputhelper);

    void Update(GameTime gameTime);

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Reset();


}