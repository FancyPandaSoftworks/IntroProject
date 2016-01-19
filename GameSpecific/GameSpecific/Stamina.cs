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

public class Stamina : GameObject
{
    int stamina;
    Object2D backGround, exhaustedBar, fullBar;
    bool exhausted;
    Vector2 position;
    Player player;
    Level parentLevel;

    public Stamina()
    {
        backGround = new Object2D("HUD\\Stamina\\Exhausted Empty Stamina");
        exhaustedBar = new Object2D("HUD\\Stamina\\Exhausted Full Stamina");
        fullBar = new Object2D("HUD\\Stamina\\Full Stamina");
        position = new Vector2(20, 20);
    }

    public override void Update(GameTime gameTime)
    {
        parentLevel = parent as Level;
        player = parentLevel.Find("Player") as Player;
        if (player.exhausted)
            exhausted = true;
        else
            exhausted = false;
        stamina = player.stamina;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(backGround.SpriteSheet.Sprite, position, new Rectangle(0, 0, backGround.Width, backGround.Height), Color.White, 0.0f, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0.0f);
        if(exhausted)
            spriteBatch.Draw(exhaustedBar.SpriteSheet.Sprite, position, new Rectangle(0, 0, (int)(fullBar.Height + stamina / 5.5 - 5), fullBar.Height), Color.White, 0.0f, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0.0f);
        else
            spriteBatch.Draw(fullBar.SpriteSheet.Sprite, position, new Rectangle(0, 0, (int)(fullBar.Height + stamina / 5.5 - 5), fullBar.Height), Color.White, 0.0f, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0.0f);
        spriteBatch.End();
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }
}
