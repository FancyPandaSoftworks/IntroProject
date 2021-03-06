﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Introproject
{
    public class GameObject : Root
    {
        protected GameObject parent;
        protected Vector2 velocity;
        protected Vector2 position;
        protected bool visible;
        protected bool overlaySprite = false;
        protected InputHelper inputHelper;
        protected string id;

        public GameObject(string id = "")
        {

            this.velocity = Vector2.Zero;
            this.visible = true;
            inputHelper = new InputHelper();
            this.id = id;
        }


        public InputHelper InputHelper
        {
            get { return inputHelper; }
        }
        public virtual void HandleInput(InputHelper inputhelper)
        {
        }


        public virtual void Update(GameTime gameTime)
        {

        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
        public virtual Vector2 GlobalPosition
        {
            get
            {
                if (parent != null)
                    return parent.GlobalPosition + this.Position;
                else
                    return this.position;
            }
        }
        public virtual void Reset()
        {
            visible = true;
        }





        public virtual Vector2 Position //dit is voor 2d classe
        {
            get { return position; }
            set { position = value; }
        }

        public virtual Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public string ID
        {
            get { return id; }
        }


        public GameObject Root
        {
            get
            {
                return this;
            }
        }


        public bool Visible
        {
            get { return visible; }
            set { visible = value; }

        }

        public virtual GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public bool OverlaySprite
        {
            set { overlaySprite = value; }
        }



    }
}
