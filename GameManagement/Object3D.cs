using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class Object3D : GameObject
{
    float aspectRatio, modelRotation;
    Model model;

    public Object3D(string modelName = "", string id = "") : base(id)
    {
        model = GameEnvironment.AssetManager.GetModel(modelName);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Matrix[] transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.EnableDefaultLighting();
                effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation)
                    * Matrix.CreateTranslation(position);
                effect.View = Matrix.CreateLookAt(GameEnvironment.Camera.position, GameEnvironment.Camera.ViewVertex, Vector3.Up);
                effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                    aspectRatio, 1.0f, 10000.0f);
            }
            mesh.Draw();
        }
        base.Draw(gameTime, spriteBatch);


    }

    public Vector3 GlobalPosition
    {
        get
        {
            return this.position;
        }
    }

    public Model Model
    {
        get { return model; }
    }


    public virtual BoundingBox BoundingBox
    {
        get { return new BoundingBox(new Vector3((int)GlobalPosition.X, (int)GlobalPosition.Y, (int)GlobalPosition.Z), new Vector3(0, 0, 0)); }

    }
}
