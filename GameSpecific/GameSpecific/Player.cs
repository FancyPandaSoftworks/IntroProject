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
    public Player(Vector3 startPos)
        : base("player")
    {
        position = startPos;

    }


    public void LoadContent()
    {
        Level level = parent as RandomLevel;
        grid = level.Find("TileGrid") as TileGrid;
    }



    //HandleInput for the player

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

        base.HandleInput(inputHelper);
    }

    //updating the player
    public override void Update(GameTime gameTime)
    {
        if (ShiftDown && stamina > 0 && exhausted == false &&
            (WDown || SDown || DDown || ADown))
        {
            velocity = 5f;
            stamina = stamina - 20;
            if (stamina < 20)
                exhausted = true;
        }
        if (WDown && !(grid.Objects[(int)((position.X + 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellWidth), (int)((position.Z + 100) / GameObjectGrid.CellHeight)] is WallTile))
        {
            position.X += 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));

        }

        if (WDown && !(grid.Objects[(int)((position.X + 100) / GameObjectGrid.CellWidth), (int)((position.Z + 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight)] is WallTile))
        {
            position.Z += 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }


        if (SDown && !(grid.Objects[(int)(position.X - 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellWidth, (int)((position.Z + 100) / GameObjectGrid.CellHeight)] is WallTile))
        {
            position.X -= 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));

        }

        if (SDown && !(grid.Objects[(int)(position.X + 100) / GameObjectGrid.CellWidth, (int)(position.Z - 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.Z -= 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (DDown && !(grid.Objects[(int)(position.X - 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)) + 95) / GameObjectGrid.CellWidth, (int)position.Z / GameObjectGrid.CellHeight] is WallTile))
        {
            position.X -= 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (DDown && !(grid.Objects[(int)(position.X + 100) / GameObjectGrid.CellWidth, (int)(position.Z + 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.Z += 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
        }

        if (ADown && !(grid.Objects[(int)(position.X + (5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY))) + 100) / GameObjectGrid.CellWidth, (int)(position.Z + 100) / GameObjectGrid.CellHeight] is WallTile))
        {
            position.X += 5f * (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY));
        }


        if (ADown && !(grid.Objects[(int)(position.X + 100) / GameObjectGrid.CellWidth, (int)(position.Z - 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)) + 100) / GameObjectGrid.CellHeight] is WallTile))
        {

            position.Z -= 5f * (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY));
        }






        /* if (input.IsKeyDown(Keys.Space))
            position.Y += 40f;
        if (input.IsKeyDown(Keys.LeftShift))
            position.Y -= 40f; */

        base.Update(gameTime);
    }
}
