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
    TileGrid grid;

    /// <summary>
    /// Constructing the player
    /// </summary>
    /// <param name="startPos">Where to start</param>
    public Player(Vector3 startPos)
        : base("player")
    {
        position = startPos;

    }


    public void LoadContent()
    {
        Level level = parent as Level;
        grid = level.Find("TileGrid") as TileGrid;
        stamina = 2000;
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
        velocity = 250f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (ShiftDown && stamina > 0 && exhausted == false &&
            (WDown || SDown || DDown || ADown))
        {
            velocity = 500f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            stamina = stamina - (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (stamina < 20)
                exhausted = true;
        }
        if (stamina < 2000 && !ShiftDown)
            stamina += (int)(0.3f * gameTime.ElapsedGameTime.TotalMilliseconds);
        if (stamina > 20)
            exhausted = false;

        if (WDown && !(grid.Objects[(int)((position.X + 20f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellWidth), (int)((position.Z + 100) / GameObjectGrid.CellHeight)] is WallTile))
        {
            position.X += velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (WDown && !(grid.Objects[(int)((position.X + 100) / GameObjectGrid.CellWidth), (int)((position.Z + 20f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight)] is WallTile))
        {
            position.Z += velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (SDown && !(grid.Objects[(int)(position.X - 20f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellWidth, (int)((position.Z + 100) / GameObjectGrid.CellHeight)] is WallTile))
        {
            position.X -= velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));

        }

        if (SDown && !(grid.Objects[(int)(position.X + 100) / GameObjectGrid.CellWidth, (int)(position.Z - 20f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.Z -= velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (DDown && !(grid.Objects[(int)(position.X - 20f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellWidth, (int)(position.Z + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.X -= velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (DDown && !(grid.Objects[(int)(position.X + 100) / GameObjectGrid.CellWidth, (int)(position.Z + 20f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.Z += velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (ADown && !(grid.Objects[(int)(position.X + (20f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY))) + 100) / GameObjectGrid.CellWidth, (int)(position.Z + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.X += velocity * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }


        if (ADown && !(grid.Objects[(int)(position.X + 100) / GameObjectGrid.CellWidth, (int)(position.Z - 20f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.Z -= velocity * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
        }

 

        /* if (input.IsKeyDown(Keys.Space))
            position.Y += 40f;
        if (input.IsKeyDown(Keys.LeftShift))
            position.Y -= 40f; */

        base.Update(gameTime);
    }
}
