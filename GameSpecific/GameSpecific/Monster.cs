using System;
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
    public Matrix world;
    public float velocity, xzdifference;
    private bool groanPlaying;
    private float realXdif, realZdif;
    private AudioEmitter monsterEmitter;
    private AudioListener playerListener;

    public Monster(GameObject[,] grid)
        : base("Misc Level Objects\\Monster\\Monster Model")
    {
        ResetGrid();
    }

    public void LoadContent()
    {
        //Finding and declaring the grid
        Level parentLevel = parent as Level;
        TileGrid tileGrid = parentLevel.Find("TileGrid") as TileGrid;
        this.grid = tileGrid.Objects;
        gridWidth = grid.GetLength(0);
        gridHeight = grid.GetLength(1);
        stepgrid = new int[gridWidth, gridHeight];

        //Counting the amount of path-tiles in the room
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] is Tile && !(grid[x, y] is WallTile))
                {
                    tiles++;
                }
            }
        }

        //Monster's position
        foreach(GameObject tile in tileGrid.Objects)
            if (tile != null)
                if (tile.ID == "EntryTile")
                    monsterPosition = tile.Position + new Vector3(0, 200, 0);

        //Monster's velocity
        velocity = 150;

        //Setting the emitter and listener for 3D sound
        playerListener = new AudioListener();
        monsterEmitter = new AudioEmitter();
    }

    public void UpdateMonster(GameTime gameTime)
    {
        this.Position = monsterPosition;
        Level level = parent as Level;
        player = level.Find("Player") as Player;
        playerPosition = player.Position;
        ResetGrid();

        //setting the tile the player is standing on to 0, in the stepgrid
        stepgrid[(int)(playerPosition.X / GameObjectGrid.cellWidth), (int)(playerPosition.Z / GameObjectGrid.cellHeight)] = 0;
        CalculateTileCost(new Vector2((int)((playerPosition.X) / GameObjectGrid.cellWidth), (int)((playerPosition.Z) / GameObjectGrid.cellHeight)), 1);

        //calculating the x- and y-difference between player and monster
        float xdifference = playerPosition.X - monsterPosition.X;
        realXdif = xdifference / 40; // change value to adjust volume
        xdifference = Math.Abs(xdifference);
        float zdifference = playerPosition.Z - monsterPosition.Z;
        realZdif = zdifference / 40;
        zdifference = Math.Abs(zdifference);

        //this switches the monster's AI depending on whether the player is in the monster's line of sight
        if (PlayerInSight(xdifference, zdifference, new Vector2(monsterPosition.X, monsterPosition.Z)))
            SimplePathFinding(gameTime, xdifference, zdifference);
        else
            AdvancedPathFinding(gameTime);

        //Making the danger level depentent on the position difference of the player and monster
        xzdifference = (float)Math.Sqrt(Math.Pow(xdifference, 2) + Math.Pow(zdifference, 2));

        for (int i = 10; i >= 0; i--)
            if (xzdifference < GameObjectGrid.cellWidth * 2 * i)
                MusicPlayer.dangerLevel = 10 - i;

        //Checking if a groan-sound is playing
        foreach (Sound sound in MusicPlayer.SoundEffect3D)
            for (int i = 0; i < 10; i++)
                if (sound.Name == "Monster" + i)
                    if (sound.SoundState == SoundState.Playing)
                        groanPlaying = true;
                    else
                        groanPlaying = false;

        //Setting the positions of the listener and emitter
        playerListener.Position = playerPosition;
        monsterEmitter.Position = new Vector3(playerPosition.X + realXdif, monsterPosition.Y, playerPosition.Z + realZdif);

        //Playing a random groan-sound
        if (!groanPlaying && GameEnvironment.Random.Next(110) == 0)
            foreach (Sound sound in MusicPlayer.SoundEffect3D)
                if (sound.Name == "Monster" + GameEnvironment.Random.Next(10))
                {
                    Console.WriteLine("Playing: {0}", sound.Name);
                    sound.Play3DSound(playerListener, monsterEmitter);
                }

        //Switch to gameOver state
        if (xzdifference < 100)
        {
            foreach (Sound sound in MusicPlayer.SoundEffect)
                if (sound.Name == "GameOver")
                    sound.PlaySound();
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        }

    }

    //a simple method for moving the monster straight towards the player
    public void SimplePathFinding(GameTime gameTime, float xdifference, float ydifference)
    {
        if (playerPosition.X > monsterPosition.X)
            monsterPosition.X += velocity * (float)Math.Cos(Math.Atan(ydifference / xdifference)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (playerPosition.X < monsterPosition.X)
            monsterPosition.X -= velocity * (float)Math.Cos(Math.Atan(ydifference / xdifference)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (playerPosition.Z > monsterPosition.Z)
            monsterPosition.Z += velocity * (float)Math.Sin(Math.Atan(ydifference / xdifference)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (playerPosition.Z < monsterPosition.Z)
            monsterPosition.Z -= velocity * (float)Math.Sin(Math.Atan(ydifference / xdifference)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

    }

    //this method makes the monster follow the shortest path to the player, using the tile cost method and the stepgrid
    public void AdvancedPathFinding(GameTime gameTime)
    {
        if ((int)monsterPosition.X / GameObjectGrid.cellWidth > 0)
            if (stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth - 1, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight] < stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight])
                if (!(grid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth - 1, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight] is WallTile))
                    monsterPosition.X -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if ((int)monsterPosition.X / GameObjectGrid.cellWidth < gridWidth - 1)
            if (stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth + 1, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight] < stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight])
                if (!(grid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth + 1, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight] is WallTile))
                    monsterPosition.X += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if ((int)monsterPosition.Z / GameObjectGrid.cellHeight > 0)
            if (stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight - 1] < stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight])
                if (!(grid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight - 1] is WallTile))
                    monsterPosition.Z -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if ((int)monsterPosition.Z / GameObjectGrid.cellHeight < gridHeight - 1)
            if (stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight + 1] < stepgrid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight])
                if (!(grid[(int)(monsterPosition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(monsterPosition.Z + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellHeight + 1] is WallTile))
                    monsterPosition.Z += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)checkposition.Y / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is Tile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
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
                    if (!(grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile) || grid[(int)(checkposition.X + GameObjectGrid.cellWidth / 2) / GameObjectGrid.cellWidth, (int)(checkposition.Y + GameObjectGrid.cellHeight / 2) / GameObjectGrid.cellHeight] is WallTile)
                    {
                        return false;
                    }
                    checkposition.X -= 1;
                }
            }
        }
        return true;
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
            for (int y = 0; y < gridHeight; y++)
                stepgrid[x, y] = tiles;
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Code for turning the monster towards the player
        Vector3 direction = new Vector3(playercamera.Position.X - Position.X, 0, playercamera.Position.Z - Position.Z);
        direction.Normalize(); //matrix with length 0
        world = Matrix.CreateWorld(Position, direction, Vector3.Up);
        Matrix[] transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        foreach (ModelMesh mesh in model.Meshes)
        {
            //Set the effects for the meshes
            foreach (BasicEffect effect in mesh.Effects)
            {
                spriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                effect.EnableDefaultLighting();
                effect.World = transforms[mesh.ParentBone.Index] * world;
                effect.View = Matrix.CreateLookAt(playercamera.Position, playercamera.ViewVertex, Vector3.Up);
                effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
                aspectRatio, 1, 1000);
                effect.FogEnabled = true;
                effect.FogStart = -1500;
                effect.FogEnd = 750;
                effect.Alpha = 1.0f;
            }
            mesh.Draw();
        }
    }
}



