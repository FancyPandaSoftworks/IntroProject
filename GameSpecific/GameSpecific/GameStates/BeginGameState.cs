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


class BeginGameState : GameState
{
    Object2D logo, black;
    float timer, fader, fader2;

    public BeginGameState()
    {
        logo = new Object2D("FancyPandaLogo");
        black = new Object2D("black");
        timer = 0;
        fader = 0;
        fader2 = 0;
    }

    public override void Update(GameTime gameTime)
    {
        MusicPlayer.beatCount = 0;
        MusicPlayer.barCount = 0;
        MusicPlayer.dangerLevel = -1;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        GameEnvironment.Graphics.Clear(Color.Black);
        spriteBatch.Begin();
        spriteBatch.Draw(logo.SpriteSheet.Sprite , new Rectangle(0,0,GameEnvironment.Screen.X,GameEnvironment.Screen.Y), Color.White * (fader-fader2));
        spriteBatch.End();

        if (timer > 1.5)
        {
            fader2 += 0.001f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else
            fader += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer > 3.5)
            GameEnvironment.GameStateManager.SwitchTo("titleScreenState");
    }
}

