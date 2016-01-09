using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Interface from which everything (eventually) inherits that is used
/// </summary>
public interface Root
{
    void HandleInput(InputHelper inputHelper);

    void Update(GameTime gameTime);

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Reset();


}