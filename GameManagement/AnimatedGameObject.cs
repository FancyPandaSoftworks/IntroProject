using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class AnimatedGameObject : Object2D
{
    protected Dictionary<string, Animation> animations;

    public AnimatedGameObject(string id = "")
        : base("", 0, id)
    {
        animations = new Dictionary<string, Animation>();
    }

    /// <summary>
    ///  Preparing a animation for use
    /// </summary>
    /// <param name="assetname">Name of the asset</param>
    /// <param name="id">The id used to find this object</param>
    /// <param name="looping">If the animation needs to be looped</param>
    /// <param name="frameTime">At what time the animation needs to start</param>
    public void LoadAnimation(string assetname, string id, bool looping, float frameTime = 0.1f)
    {
        Animation animation = new Animation(assetname, looping, frameTime);
        animations[id] = animation;
    }

    /// <summary>
    ///  Play the animation
    /// </summary>
    /// <param name="id">The id used to find this object</param>
    public void PlayAnimation(string id)
    {
        if (spriteSheet == animations[id])
            return;
        if (spriteSheet != null)
            animations[id].Mirror = spriteSheet.Mirror;
        animations[id].play();
        spriteSheet = animations[id];
        origin = new Vector2(spriteSheet.Width / 2, spriteSheet.Height);
    }

    public override void Update(GameTime gameTime)
    {
        if (spriteSheet == null)
            return;
        Current.Update(gameTime);
        base.Update(gameTime);
    }

    /// <summary>
    /// Returns the current Animation
    /// </summary>
    public Animation Current
    {
        get { return spriteSheet as Animation; }
    }


}

