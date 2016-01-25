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
    public int stamina;
    public bool exhausted, ShiftDown, WDown, ADown, SDown, DDown, EDown;
    TileGrid grid;
    Level level;

    /// <summary>
    /// Constructing the player
    /// </summary>
    /// <param name="startPos">Where to start</param>
    public Player(Vector3 startPos)
        : base("Player")
    {
        position = startPos;
        ViewVertex = startPos + new Vector3(0, 0, 1);
    }


    public void LoadContent()
    {
        level = parent as Level;
        grid = level.Find("TileGrid") as TileGrid;
        stamina = 2000;
    }

    public TileGrid Grid
    {
        set { grid = value; }
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

        if (((WDown || ADown || SDown || DDown) && !ShiftDown) || (WDown || ADown || SDown || DDown) && ShiftDown && exhausted)
        {
            foreach (Sound sound in MusicPlayer.LoopedEffect)
                if (sound.Name == "Footsteps1")
                {
                    sound.PlaySound(0.5f);
                }
        }
        else
        {
            foreach (Sound sound in MusicPlayer.LoopedEffect)
                if (sound.Name == "Footsteps1")
                    sound.StopSound();
        }

        if ((WDown || ADown || SDown || DDown) && ShiftDown && !exhausted)
        {
            foreach (Sound sound in MusicPlayer.LoopedEffect)
                if (sound.Name == "Footsteps2")
                {
                    sound.PlaySound(0.5f);
                }
        }
        else
        {
            foreach (Sound sound in MusicPlayer.LoopedEffect)
                if (sound.Name == "Footsteps2")
                    sound.StopSound();
        }
        base.HandleInput(inputHelper);
    }

    /// <summary>
    /// Updating the player
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    public override void Update(GameTime gameTime)
    {
        //Setting the velocity
        velocity = 250f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //Draining the stamina when running
        if (ShiftDown && stamina > 0 && exhausted == false &&
            (WDown || SDown || DDown || ADown))
        {
            velocity = 500f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            stamina = stamina - (int)(gameTime.ElapsedGameTime.TotalMilliseconds * 0.75);
            if (stamina < 20)
                exhausted = true;
        }
        //Adding stamina then you are not running
        if (stamina < 2000 && (!ShiftDown || exhausted))
            stamina += (int)(0.5f * gameTime.ElapsedGameTime.TotalMilliseconds);
        //Deciding when you are exhausted
        if (stamina > 400)
            exhausted = false;

        //Player movement + collision with walls
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

        //Collision with decoration objects
        foreach (GameObject gameObject in level.Objects)
        {
            if (gameObject is Decoration)
            {
                Decoration decoration = gameObject as Decoration;
                Rectangle decoRectangle;
                if (decoration.modelRotation == (float)Math.PI / 180 * 90 || decoration.modelRotation == (float)Math.PI / 180 * 270)
                {
                    decoRectangle = new Rectangle((int)decoration.Position.X - decoration.Width(decoration.ID) / 2, (int)decoration.Position.Z - decoration.Depth(decoration.ID) / 2
                        , decoration.Width(decoration.ID), decoration.Depth(decoration.ID));
                }
                else
                {
                    decoRectangle = new Rectangle((int)decoration.Position.X - decoration.Depth(decoration.ID) / 2, (int)decoration.Position.Z - decoration.Width(decoration.ID) / 2
                        , decoration.Depth(decoration.ID), decoration.Width(decoration.ID));
                }
                Rectangle playerRectangle = new Rectangle((int)Position.X - 30, (int)Position.Z - 30, 60, 60);
                if (playerRectangle.Intersects(decoRectangle))
                {
                    Rectangle intersected = Collision.Intersection(decoRectangle,playerRectangle);
                    if (intersected.Width < intersected.Height)
                    {
                        if (Position.X > decoration.Position.X && intersected.Width > 0)
                        {
                            position.X += intersected.Width;
                        }
                        else if (Position.X < decoration.Position.X && intersected.Width > 0)
                        {
                            position.X -= intersected.Width;
                        }
                    }
                    else
                    {
                        if (Position.Z > decoration.Position.Z && intersected.Height > 0)
                        {
                            position.Z += intersected.Height;
                        }
                        else if (Position.Z < decoration.Position.Z && intersected.Height > 0)
                        {
                            position.Z -= intersected.Height;
                        }
                    }
                }
            }
        }
        base.Update(gameTime);
    }
}
