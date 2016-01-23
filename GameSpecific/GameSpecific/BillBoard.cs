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

class BillBoard : Object3D
{
    public Matrix world;

    public BillBoard(Vector3 position)
        : base("Misc Level Objects\\Hanging Man\\Hanging Man Model", "Hanging Man")
    {
        this.Position = position;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Console.WriteLine(this.position);

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
                aspectRatio, 1, 10000);
                effect.FogEnabled = true;
                effect.FogStart = -1500;
                effect.FogEnd = 750;
                effect.Alpha = 1.0f;
            }
            mesh.Draw();
        }
    }
}

