using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Introproject
{
    public class Camera : GameObject
    {

        float  viewAngleX, viewAngleY;
        Vector3 viewVertex,cameraPosition;
        Vector2 prevMousePos, mouseDiff;
        protected override void LoadContent()
        {
            prevMousePos = new Vector2(GraphicsDevice.Viewport.X / 2, GraphicsDevice.Viewport.Y / 2);
            LoadContent();
        }


        protected 
      

        protected override void Update(GameTime gameTime)
        {

            InputHelper input = InputHelper;

            
            if (input.IsKeyDown(Keys.W))
            {
                cameraPosition.X += 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
                cameraPosition.Z += 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
            }
            if (input.IsKeyDown(Keys.S))
            {
                cameraPosition.X -= 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
                cameraPosition.Z -= 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
            }
            if (input.IsKeyDown(Keys.D))
            {
                cameraPosition.Z += 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
                cameraPosition.X -= 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
            }
            if (input.IsKeyDown(Keys.A))
            {
                cameraPosition.Z -= 40f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
                cameraPosition.X += 40f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
            }
            if (input.IsKeyDown(Keys.Space))
                cameraPosition.Y += 40f;
            if (input.IsKeyDown(Keys.LeftShift))
                cameraPosition.Y -= 40f;
            mouseDiff.X = input.MousePosition.X - prevMousePos.X;
            mouseDiff.Y = input.MousePosition.Y - prevMousePos.Y;
            viewAngleX += mouseDiff.X * 0.005f;
            viewAngleY -= mouseDiff.Y * 0.005f;
            if (viewAngleY > 1)
                viewAngleY = 1;
            if (viewAngleY < -1)
                viewAngleY = -1;
            viewVertex = new Vector3(cameraPosition.X + (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)),
                                    cameraPosition.Y + (float)Math.Sin(viewAngleY),
                                    cameraPosition.Z + (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)));
            prevMousePos = new Vector2(input.MousePosition.X, input.MousePosition.Y);



        }
    }
}