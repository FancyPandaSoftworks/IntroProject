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

class EndGameState : GameState
{
    Object2D black;
    float fader;
    TextGameObject text;
    bool gunShotSound;

    public EndGameState()
    {
        black = new Object2D("black");
        fader = 0;
        gunShotSound = false;
    }

    public override void Update(GameTime gameTime)
    {
        MusicPlayer.beatCount = 0;
        MusicPlayer.barCount = 0;
        MusicPlayer.dangerLevel = -1;
        text = new TextGameObject("text");
        text.text = "End.";
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        GameEnvironment.GameStateManager.GetGameState("playingState").Draw(gameTime, spriteBatch);

        //Draw the black fade-out square
        spriteBatch.Begin();
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(black.SpriteSheet.Sprite , new Rectangle(0,0,GameEnvironment.Screen.X,GameEnvironment.Screen.Y), Color.White * fader);
        spriteBatch.End();
        
        //switch to 3D drawing
        spriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        fader += (float)gameTime.ElapsedGameTime.TotalSeconds;

        //Draw the "End." text on screen after 3 seconds
        if (fader > 3)
        {
            text.Position = new Vector2(GameEnvironment.Screen.X/2 - 10,GameEnvironment.Screen.Y/2 - 10);
            text.Draw(gameTime, spriteBatch);
        }
         
        //Play the gunshot sound after 3 seconds
        if (fader > 3 && !gunShotSound)
        {            
            foreach (Sound sound in MusicPlayer.SoundEffect)
                if (sound.Name == "GunShot")
                    sound.PlaySound();  //TODO: 1x afspelen
            gunShotSound = true;
        }

        //Switch to the credit state after 6 seconds
        if (fader > 6)
        {
            fader = 0;
            foreach (Sound sound in MusicPlayer.Music)
                sound.PlaySound();
            GameEnvironment.GameStateManager.SwitchTo("creditState");
        }
    }


}
