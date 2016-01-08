using System;
using Microsoft.Xna.Framework;

/// <summary>
/// The basics that a Gamestate needs to have
/// </summary>
abstract class GameState : GameObjectList
{
    protected static Game game;
    private static bool gameVariableSet;

    /// <summary>
    /// A reference to the original game
    /// </summary>
    public static Game GameVariable
    {
        set{ if (!gameVariableSet)
                { 
                game = value;
                gameVariableSet = true;
                }
        }
    }

    /// <summary>
    /// Draw the basic things
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        game.IsMouseVisible = true;
        base.Draw(gameTime, spriteBatch);
    }
}
