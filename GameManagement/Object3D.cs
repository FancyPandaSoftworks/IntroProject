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
        float aspectRatio, modelRotation, viewAngleX, viewAngleY;
        Vector3 viewVertex, modelPosition, cameraPosition, position;
        Model myModel;
        GraphicsDevice graphicsDevice;
        ContentManager content;
        

        public Object3D(string modelName, string id = "")
        {
            myModel = content.Load<Model>(modelName);
            this.id = id;

        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            graphicsDevice.Clear(Color.Black);
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in myModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, viewVertex, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                        aspectRatio, 1.0f, 10000.0f);
                }
                mesh.Draw();
            }
            base.Draw(gameTime, spriteBatch);


        }
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector3 GlobalPosition
        {
            get
            {
                return this.position;
            }
        }


        public virtual BoundingBox BoundingBox
        {
            get { return new BoundingBox(new Vector3((int)GlobalPosition.X, (int)GlobalPosition.Y, (int)GlobalPosition.Z), new Vector3(0, 0, 0)); }

        }


    }

