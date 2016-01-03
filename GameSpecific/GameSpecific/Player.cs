﻿using System;
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
    
    public Player(Vector3 startPos) : base("player")
    {
        position = startPos;
        
    }


    public override void HandleInput(InputHelper input)
    {
        if (input.IsKeyDown(Keys.W))
        {
            position.X += 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z += 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (input.IsKeyDown(Keys.S))
        {
            position.X -= 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z -= 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (input.IsKeyDown(Keys.D))
        {
            position.Z += 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X -= 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (input.IsKeyDown(Keys.A))
        {
            position.Z -= 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X += 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (input.IsKeyDown(Keys.Space))
            position.Y += 40f;
        if (input.IsKeyDown(Keys.LeftShift))
            position.Y -= 40f;

        base.HandleInput(input);
        }
}
