using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Player : Camera
{
    float velocity;
    int stamina;
    bool exhausted, ShiftDown, WDown, ADown, SDown, DDown;

    public Player(Vector3 startPos) : base("player")
    {
        position = startPos;
        stamina = 1000;
        velocity = 5f;
        exhausted = false;
    }

    public override void HandleInput(InputHelper input)
    {
        ShiftDown = false;
        WDown = false;
        ADown = false;
        SDown = false;
        DDown = false;

        if (input.IsKeyDown(Keys.LeftShift))
            ShiftDown = true;
        if (input.IsKeyDown(Keys.W))
            WDown = true;
        if (input.IsKeyDown(Keys.A))
            ADown = true;
        if (input.IsKeyDown(Keys.S))
            SDown = true;
        if (input.IsKeyDown(Keys.D))
            DDown = true;

        base.HandleInput(InputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        if (ShiftDown && stamina > 0 && exhausted == false &&
            (WDown || SDown || DDown || ADown))
        {
            velocity = 20f;
            stamina = stamina - 20;
            if (stamina < 20)
                exhausted = true;
        }
        else
        {
            velocity = 5f;
            stamina = stamina + 5;

            if (stamina > 1000)
                stamina = 1000;

            if (exhausted == true && stamina > 200)
            {
                exhausted = false;
            }
        }

        if (WDown && ADown ||
            WDown && DDown ||
            SDown && ADown ||
            SDown && DDown)
        {
            velocity = velocity / 2;
        }
        if (WDown)
        {
            position.X += velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z += velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (SDown)
        {
            position.X -= velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z -= velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (DDown)
        {
            position.Z += velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X -= velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (ADown)
        {
            position.Z -= velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X += velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        //if (input.IsKeyDown(Keys.Space))
        //    position.Y += velocity;
        //if (input.IsKeyDown(Keys.Z))
        //    position.Y -= velocity;

        base.Update(gameTime);
    }
}
