using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


class CreditState : GameState
{
    public float timer, creditVelocity;
    TextGameObject credits;
    bool escDown;

    public CreditState()
    {
        credits = new TextGameObject("credits");
        credits.text = "Thank you for playing\n\n\n\n\n\nCoding:\n\nAllard\nJelle\nOscar\nRik\nWouter\n\n\nModel Design\n\nAllard\nOscar\n\n\nSprites\n\nAllard\nOscar\nRik\nJelle\nWouter\n\n\nMusic\n\nJelle\n\n\nSound Effects\n\nJelle\nWouter\n\n\n\nWe are Fancy Panda Softworks";
        credits.Position = new Vector2(100, GameEnvironment.Screen.Y*1.5f);
        timer = 0;
        creditVelocity = 16;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.Escape))
            escDown = true;
        else 
            escDown = false;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (escDown)
            credits.Position -= new Vector2(0, (float)gameTime.ElapsedGameTime.TotalMilliseconds/6);
        else 
            credits.Position -= new Vector2(0, (float)gameTime.ElapsedGameTime.TotalMilliseconds/16);

        credits.Draw(gameTime, spriteBatch);
        Console.WriteLine(credits.Position);

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (credits.Position.Y < -1400)
        {
            credits.Position = new Vector2(100, GameEnvironment.Screen.Y * 2);
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");
        }
    }

}

