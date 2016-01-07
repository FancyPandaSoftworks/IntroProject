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

    /// <summary>
    /// An Object to be updated and/or drawn
    /// </summary>
    /// <param name="id">The id used to find this object</param>
    public GameObject(string id = "")
    {

        this.velocity = Vector3.Zero;
        this.visible = true;
        inputHelper = new InputHelper();
        this.id = id;
    }

    /// <summary>
    /// Create a method to handle the input with in all game objects
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    /// <summary>
    /// A method to update the game objects
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    public virtual void Update(GameTime gameTime)
    {
    }

    /// <summary>
    /// A method to draw the object
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
    }

    /// <summary>
    /// Gets the absolute position of an object in relation to its parents
    /// </summary>
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

    /// <summary>
    /// Reset the object to its original state
    /// </summary>
    public virtual void Reset()
    {
        visible = true;
    }

    /// <summary>
    /// Property for the object's position
    /// </summary>
    public virtual Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    /// <summary>
    /// Property for the object's velocity
    /// </summary>
    public virtual Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    /// <summary>
    /// Return the id of the object
    /// </summary>
    public string ID
    {
        get { return id; }
    }

    /// <summary>
    /// Property for the visible status
    /// </summary>
    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    /// <summary>
    /// Property for the parent of the object
    /// </summary>
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
