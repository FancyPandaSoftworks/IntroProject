using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// A 3D object
/// </summary>
public class Object3D : GameObject
{
    public static float aspectRatio, modelRotation;
    public Model model;
    public static Camera playercamera;

    /// <summary>
    /// Create the object
    /// </summary>
    /// <param name="modelName">What model this object represents</param>
    /// <param name="id">The id used to find this object</param>
    public Object3D(string modelName = "", string id = "") : base(id)
    {
        modelRotation = 0.0f;
        aspectRatio = 1.6667f; //NEEDS TO BE DETERMINED, INSTEAD OF DECLARED
        model = GameEnvironment.AssetManager.GetModel(modelName);
       
    }

    /// <summary>
    /// Property for the Aspectratio
    /// </summary>
    public float AspectRatio
    {
        get { return aspectRatio; }
    }

    /// <summary>
    /// Draw the model in the world
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Matrix[] transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        foreach (ModelMesh mesh in model.Meshes)
        {
            //set the effects for the meshes
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.EnableDefaultLighting();
                effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation)
                    * Matrix.CreateTranslation(position);
                effect.View = Matrix.CreateLookAt(playercamera.position, playercamera.ViewVertex, Vector3.Up);
                effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                aspectRatio, 1.0f, 10000.0f);
                effect.FogEnabled = true;
                effect.FogStart = 0;
                effect.FogEnd = 500;
            }
            mesh.Draw();
        }
        base.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Property to get the model
    /// </summary>
    public Model Model
    {
        get { return model; }
    }

    /// <summary>
    /// Property to get a 3D boundingbox
    /// </summary>
    public virtual BoundingBox BoundingBox
    {
        get { return new BoundingBox(new Vector3((int)GlobalPosition.X, (int)GlobalPosition.Y, (int)GlobalPosition.Z), new Vector3(0, 0, 0)); }
    }

    /// <summary>
    /// Get the data of the player and set it in this object
    /// </summary>
    /// <param name="player">The player which you want to set</param>
    public void DrawCamera(Camera player)
    {
        playercamera = player;
    }
}
