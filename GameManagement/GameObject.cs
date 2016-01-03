using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class GameObject : Root
{
    protected GameObject parent;
    protected Vector3 velocity;
    protected Vector3 position;
    protected bool visible;
    protected bool overlaySprite = false;
    protected InputHelper inputHelper;
    protected string id;

    //An Object to be updated and/or drawn
    public GameObject(string id = "")
    {

        this.velocity = Vector3.Zero;
        this.visible = true;
        inputHelper = new InputHelper();
        this.id = id;
    }

    //Create a method to handle the input with in all game objects
    public virtual void HandleInput(InputHelper inputhelper)
    {
    }

    //Create a method to update the game objects
    public virtual void Update(GameTime gameTime)
    {
    }

    //Create a method to draw the object
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
    }

    //gets the absolute position of an object in relation to its parents
    public virtual Vector3 GlobalPosition
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

    //property for the object's position
    public virtual Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    //property for the object's velocity
    public virtual Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    //return the object id
    public string ID
    {
        get { return id; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    //property for the parent of the object
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
