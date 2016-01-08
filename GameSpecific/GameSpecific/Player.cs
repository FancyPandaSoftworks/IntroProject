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

    /// <summary>
    /// Constructing the player
    /// </summary>
    /// <param name="startPos">Where to start</param>
    public Player(Vector3 startPos) : base("player")
    {
        position = startPos;
        
    }

    /// <summary>
    /// HandleInput for the player
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {

        ShiftDown = false;
        WDown = false;
        ADown = false;
        SDown = false;
        DDown = false;
        EDown = false;

        if (inputHelper.IsKeyDown(Keys.LeftShift))
            ShiftDown = true;
        if (inputHelper.IsKeyDown(Keys.W))
            WDown = true;
        if (inputHelper.IsKeyDown(Keys.A))
            ADown = true;
        if (inputHelper.IsKeyDown(Keys.S))
            SDown = true;
        if (inputHelper.IsKeyDown(Keys.D))
            DDown = true;
        if (inputHelper.IsKeyDown(Keys.E))
            EDown = true;

        base.HandleInput(inputHelper);
    }

    /// <summary>
    /// Updating the player
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
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
            position.X += 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z += 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (SDown)
        {
            position.X -= 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.Z -= 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (DDown)
        {
            position.Z += 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X -= 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        if (ADown)
        {
            position.Z -= 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
            position.X += 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }
        /* if (inputHelper.IsKeyDown(Keys.Space))
            position.Y += 40f;
        if (inputHelper.IsKeyDown(Keys.LeftShift))
            position.Y -= 40f; */

        base.Update(gameTime);
        }
}
