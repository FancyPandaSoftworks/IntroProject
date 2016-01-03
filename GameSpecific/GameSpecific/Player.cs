using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : Camera
{
    float velocity;
    int stamina;
    public bool exhausted, ShiftDown, WDown, ADown, SDown, DDown, EDown;

    public Player(Vector3 startPos) : base("player")
    {
        position = startPos;
        
    }


    public override void HandleInput(InputHelper input)
    {

        ShiftDown = false;
        WDown = false;
        ADown = false;
        SDown = false;
        DDown = false;
        EDown = false;

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
        if (input.IsKeyDown(Keys.E))
            EDown = true;

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
        if (WDown)
        {
            position.X += 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z += 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (SDown)
        {
            position.X -= 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z -= 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (DDown)
        {
            position.Z += 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X -= 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (ADown)
        {
            position.Z -= 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X += 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        /* if (input.IsKeyDown(Keys.Space))
            position.Y += 40f;
        if (input.IsKeyDown(Keys.LeftShift))
            position.Y -= 40f; */

        base.Update(gameTime);
        }
}
