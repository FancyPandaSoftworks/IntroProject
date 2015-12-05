using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Camera : Object3D
{
    
    float  viewAngleX, viewAngleY;
    Vector3 viewVertex;
    Vector2 prevMousePos, mouseDiff;

    public Camera() : base("", "")
    {

    }

    protected void LoadContent()
    {
        prevMousePos = new Vector2(GameEnvironment.screen.X / 2, GameEnvironment.screen.Y / 2);
        LoadContent();
    }

    public Vector3 ViewVertex
    {
        get { return ViewVertex; }
    }

    public override void Update(GameTime gameTime)
    {

        InputHelper input = InputHelper;

            
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
        mouseDiff.X = input.MousePosition.X - prevMousePos.X;
        mouseDiff.Y = input.MousePosition.Y - prevMousePos.Y;
        viewAngleX += mouseDiff.X * 0.005f;
        viewAngleY -= mouseDiff.Y * 0.005f;
        if (viewAngleY > 1)
            viewAngleY = 1;
        if (viewAngleY < -1)
            viewAngleY = -1;
        viewVertex = new Vector3(position.X + (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)),
                                position.Y + (float)Math.Sin(viewAngleY),
                                position.Z + (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)));
        prevMousePos = new Vector2(input.MousePosition.X, input.MousePosition.Y);



    }
}