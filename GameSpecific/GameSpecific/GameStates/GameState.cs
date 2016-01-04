using System;
using Microsoft.Xna.Framework;

abstract class GameState : GameObjectList
{
    protected static Game game;
    private static bool gameVariableSet;

    public static Game GameVariable
    {
        set{ if (!gameVariableSet)
                { 
                game = value;
                gameVariableSet = true;
                }
        }
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        game.IsMouseVisible = true;
        base.Draw(gameTime, spriteBatch);
    }
}
