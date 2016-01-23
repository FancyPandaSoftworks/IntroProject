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

class ControlsGameState : GameState
{
    Object2D controls, black;
    float timer, fader;
    
    public ControlsGameState()
    {
        controls = new Object2D("Controls");
        black = new Object2D("black");
        timer = 0;
        fader = 0;
    }

    public override void Update(GameTime gameTime)
    {
        MusicPlayer.beatCount = 0;
        MusicPlayer.barCount = 0;
        MusicPlayer.dangerLevel = -1;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        game.IsMouseVisible = false;

        foreach (Sound sound in MusicPlayer.Music)
            sound.StopSound();

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        spriteBatch.Begin();
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(controls.SpriteSheet.Sprite, new Vector2((GameEnvironment.Screen.X - controls.SpriteSheet.Width) / 2, (GameEnvironment.Screen.Y - controls.SpriteSheet.Height) / 2), Color.White * fader);
        spriteBatch.End();

            fader += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer > 2)
        {
            game.IsMouseVisible = false;
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
    }
}