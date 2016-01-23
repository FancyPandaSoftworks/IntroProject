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
    public float timer;
    TextGameObject credits;

    public CreditState()
    {
        credits = new TextGameObject("credits");
        credits.text = "Thank you for playing\n\n\n\n\n\nCoding:\n\nAllard\nJelle\nOscar\nRik\nWouter\n\nModel Design\n\nAllard\nOscar\n\nMusic\n\nJelle\n\nSound Effects\n\nJelle\nWouter\n\n\n\nWe are Fancy Panda Softworks";
        //1 regel text is 25 pixels, if statement aanpassen
        credits.Position = new Vector2(100, GameEnvironment.Screen.Y*2);
        timer = 0;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        credits.Position -= new Vector2(0, (float)gameTime.ElapsedGameTime.TotalMilliseconds);
        credits.Draw(gameTime, spriteBatch);
        Console.WriteLine(credits.Position);

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (credits.Position.Y < -1000)
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");
    }

}

