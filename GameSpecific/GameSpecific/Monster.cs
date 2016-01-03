﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

class Monster : Object3D
{
    public Vector3 monsterPosition, playerPosition;
    public int[,] stepgrid;
    public GameObject[,] grid;
    public int tiles = 0;
    public int gridHeight, gridWidth;
    public Player player;

    public Monster(GameObject[,] grid, Vector3 playerPosition) : base("untitled")
    {
        ResetGrid();
        this.playerPosition = playerPosition;
    }

    public void LoadContent()
    {
        Level parentLevel = parent as Level;
        TileGrid tileGrid = parentLevel.Find("TileGrid") as TileGrid;
        this.grid = tileGrid.Objects;
        gridWidth = grid.GetLength(0);
        gridHeight = grid.GetLength(1);
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                //counting the amount of path-tiles in the room
                if (grid[x, y] is Tile && !(grid[x, y] is WallTile))
                {
                    tiles++;
                }
            }
        }
        stepgrid = new int[gridWidth, gridHeight];
        monsterPosition = playerPosition;
    }

    public override void Update(GameTime gameTime)
    {
        this.Position = monsterPosition + new Vector3(-50,0,-50);
        Level level = parent as Level;
        player = level.Find("player") as Player;
        playerPosition = player.Position;
        ResetGrid();
        Console.WriteLine(playerPosition);
        //setting the tile the player is standing on to 0, in the stepgrid
        stepgrid[(int)(playerPosition.X / GameObjectGrid.cellWidth), (int)(playerPosition.Z / GameObjectGrid.cellHeight)] = 0;
        CalculateTileCost(new Vector2((int)(playerPosition.X / GameObjectGrid.cellWidth), (int)(playerPosition.Z / GameObjectGrid.cellHeight)), 1);

        //calculating the x- and y-difference between player and monster
        float xdifference = playerPosition.X - monsterPosition.X;
        xdifference = Math.Abs(xdifference);
        float zdifference = playerPosition.Z - monsterPosition.Z;
        zdifference = Math.Abs(zdifference);

        SimplePathFinding(xdifference, zdifference);

        //this switches the monster's AI depending on whether the player is in the monster's line of sight
        if (PlayerInSight(xdifference, zdifference, new Vector2(monsterPosition.X, monsterPosition.Z)))
        {
            SimplePathFinding(xdifference, zdifference);
            Console.WriteLine("simple");
        }
        else
        {
            AdvancedPathFinding();
            Console.WriteLine("adv");
        }
    }

    //method to check if the player is in the monster's 'line of sight'
    //it checks the position of the player compared to the monster's position and then checks to see which is bigger, the x- or ydifference
    //it then checks all positions in an imaginary line from monster to player, and if one of those positions is inside a wall, it returns the false value...
    //because that means that the player is not in the monster's line of sight
    public bool PlayerInSight(float xdifference, float zdifference, Vector2 checkposition)
    {
        if (playerPosition.X < monsterPosition.X && playerPosition.Z < monsterPosition.Z)
        {
            if (xdifference > zdifference)
            {
                while (checkposition.X > playerPosition.X)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X -= (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y -= (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }
            else if (zdifference > xdifference)
            {
                while (checkposition.Y > playerPosition.Y)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X -= (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y -= (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }

        }

        else if (playerPosition.X < monsterPosition.X && playerPosition.Z > monsterPosition.Z)
        {
            if (xdifference > zdifference)
            {
                while (checkposition.X > playerPosition.X)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X -= (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y += (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }
            else if (zdifference > xdifference)
            {
                while (checkposition.Y < playerPosition.Z)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X -= (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y += (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }

        }

        else if (playerPosition.X > monsterPosition.X && playerPosition.Z < monsterPosition.Z)
        {
            if (xdifference > zdifference)
            {
                while (checkposition.X < playerPosition.X)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X += (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y -= (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }
            else if (zdifference > xdifference)
            {
                while (checkposition.Y > playerPosition.Z)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X += (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y -= (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }

        }

        else if (playerPosition.X > monsterPosition.X && playerPosition.Z > monsterPosition.Z)
        {
            if (xdifference > zdifference)
            {
                while (checkposition.X < playerPosition.X)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X += (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y += (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }
            else if (zdifference > xdifference)
            {
                while (checkposition.Y < playerPosition.Z)
                {
                    if (!(grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile))
                    {
                        return false;
                    }
                    checkposition.X += (float)Math.Cos(Math.Atan(zdifference / xdifference));
                    checkposition.Y += (float)Math.Sin(Math.Atan(zdifference / xdifference));
                }
            }
        }

        else if (playerPosition.X == monsterPosition.X)
        {
            if (playerPosition.Z > monsterPosition.Z)
            {
                while (playerPosition.Z > monsterPosition.Z)
                {
                    if (grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is WallTile)
                    {
                        return false;
                    }
                    checkposition.Y += 1;
                }
            }
            else if (playerPosition.Z < monsterPosition.Z)
            {
                while (playerPosition.Z < monsterPosition.Z)
                {
                    if (grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is WallTile)
                    {
                        return false;
                    }
                    checkposition.Y -= 1;
                }
            }
        }

        else if (playerPosition.Z == monsterPosition.Z)
        {
            if (playerPosition.X > monsterPosition.X)
            {
                while (playerPosition.X > monsterPosition.X)
                {
                    if (grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is WallTile)
                    {
                        return false;
                    }
                    checkposition.X += 1;
                }
            }
            else if (playerPosition.X < monsterPosition.X)
            {
                while (playerPosition.X < monsterPosition.X)
                {
                    if (grid[(int)checkposition.X / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is WallTile)
                    {
                        return false;
                    }
                    checkposition.X -= 1;
                }
            }
        }
        return true;
    }

    //a simple method for moving the monster straight towards the player
    public void SimplePathFinding(float xdifference, float ydifference)
    {
        if (playerPosition.X > monsterPosition.X)
        {
            monsterPosition.X += (float)Math.Cos(Math.Atan(ydifference / xdifference));
        }
        if (playerPosition.X < monsterPosition.X)
        {
            monsterPosition.X -= (float)Math.Cos(Math.Atan(ydifference / xdifference));
        }
        if (playerPosition.Z > monsterPosition.Z)
        {
            monsterPosition.Z += (float)Math.Sin(Math.Atan(ydifference / xdifference));
        }
        if (playerPosition.Z < monsterPosition.Z)
        {
            monsterPosition.Z -= (float)Math.Sin(Math.Atan(ydifference / xdifference));
        }
    }

    //this method makes the monster follow the shortest path to the player, using the tile cost method and the stepgrid
    public void AdvancedPathFinding()
    {
        if ((int)monsterPosition.X / GameObjectGrid.cellWidth > 0)
        {
            if (stepgrid[(int)(monsterPosition.X) / GameObjectGrid.cellWidth - 1, (int)monsterPosition.Z / GameObjectGrid.cellHeight] < stepgrid[(int)monsterPosition.X / GameObjectGrid.cellWidth, (int)monsterPosition.Z / GameObjectGrid.cellHeight])
            {
                monsterPosition.X -= 1;
            }
        }
        if ((int)monsterPosition.X / GameObjectGrid.cellWidth < gridWidth - 1)
        {
            if (stepgrid[(int)(monsterPosition.X) / GameObjectGrid.cellWidth + 1, (int)monsterPosition.Z / GameObjectGrid.cellHeight] < stepgrid[(int)monsterPosition.X / GameObjectGrid.cellWidth, (int)monsterPosition.Z / GameObjectGrid.cellHeight])
            {
                monsterPosition.X += 1;
            }
        }
        if ((int)monsterPosition.Z / GameObjectGrid.cellHeight > 0)
        {
            if (stepgrid[(int)monsterPosition.X / GameObjectGrid.cellWidth, (int)(monsterPosition.Z) / GameObjectGrid.cellHeight - 1] < stepgrid[(int)monsterPosition.X / GameObjectGrid.cellWidth, (int)monsterPosition.Z / GameObjectGrid.cellHeight])
            {
                monsterPosition.Z -= 1;
            }
        }
        if ((int)monsterPosition.Z / GameObjectGrid.cellHeight < gridHeight - 1)
        {
            if (stepgrid[(int)monsterPosition.X / GameObjectGrid.cellWidth, (int)(monsterPosition.Z) / GameObjectGrid.cellHeight + 1] < stepgrid[(int)monsterPosition.X / GameObjectGrid.cellWidth, (int)monsterPosition.Z / GameObjectGrid.cellHeight])
            {
                monsterPosition.Z += 1;
            }
        }
    }

    //a method that calculates the total steps/tiles it takes to get to the player's position
    public void CalculateTileCost(Vector2 tilepos, int counter)
    {
        if ((int)tilepos.X > 0)
        {
            if (grid[(int)tilepos.X - 1, (int)tilepos.Y] is Tile && !(grid[(int)tilepos.X - 1, (int)tilepos.Y] is WallTile) && stepgrid[(int)tilepos.X - 1, (int)tilepos.Y] > counter)
            {
                stepgrid[(int)tilepos.X - 1, (int)tilepos.Y] = counter;
                CalculateTileCost(new Vector2((int)tilepos.X - 1, (int)tilepos.Y), counter + 1);
            }
        }
        if ((int)tilepos.Y > 0)
        {
            if (grid[(int)tilepos.X, (int)tilepos.Y - 1] is Tile && !(grid[(int)tilepos.X, (int)tilepos.Y - 1] is WallTile) && stepgrid[(int)tilepos.X, (int)tilepos.Y - 1] > counter)
            {
                stepgrid[(int)tilepos.X, (int)tilepos.Y - 1] = counter;
                CalculateTileCost(new Vector2((int)tilepos.X, (int)tilepos.Y - 1), counter + 1);
            }
        }
        if ((int)tilepos.X < gridWidth - 1)
        {
            if (grid[(int)tilepos.X + 1, (int)tilepos.Y] is Tile && !(grid[(int)tilepos.X + 1, (int)tilepos.Y] is WallTile) && stepgrid[(int)tilepos.X + 1, (int)tilepos.Y] > counter)
            {
                stepgrid[(int)tilepos.X + 1, (int)tilepos.Y] = counter;
                CalculateTileCost(new Vector2((int)tilepos.X + 1, (int)tilepos.Y), counter + 1);
            }
        }
        if ((int)tilepos.Y < gridHeight - 1)
        {
            if (grid[(int)tilepos.X, (int)tilepos.Y + 1] is Tile && !(grid[(int)tilepos.X, (int)tilepos.Y + 1] is WallTile) && stepgrid[(int)tilepos.X, (int)tilepos.Y + 1] > counter)
            {
                stepgrid[(int)tilepos.X, (int)tilepos.Y + 1] = counter;
                CalculateTileCost(new Vector2((int)tilepos.X, (int)tilepos.Y + 1), counter + 1);
            }
        }
    }

    public void ResetGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                stepgrid[x, y] = tiles;
            }
        }
    }
}

