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

public class AssetManager
{
    protected ContentManager contentmanager;

    public AssetManager(ContentManager Content)
    {
        this.contentmanager = Content;
    }

    /// <summary>
    /// Load a Texture2D
    /// </summary>
    /// <param name="assetName">The name of the file</param>
    /// <returns>The sprite if the name is correct, null if the assetName is empty</returns>
    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<Texture2D>(assetName);
    }

    /// <summary>
    /// Load a Model
    /// </summary>
    /// <param name="assetName">The name of the file</param>
    /// <returns>The model in the file that can be found with the assetname, or null incase the assetName is empty</returns>
    public Model GetModel(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<Model>(assetName);
    }

    /// <summary>
    /// Load a Font
    /// </summary>
    /// <param name="assetName">The name of the file</param>
    /// <returns>The spriteFont that can be found with the assetName, or null in case the assetName is empty</returns>
    public SpriteFont GetSpriteFont(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<SpriteFont>(assetName);
    }

    /// <summary>
    /// Load a Sound and play it
    /// </summary>
    /// <param name="assetName">The name of the file</param>
    public void Playsound(string assetName)
    {
        SoundEffect soundeffect = contentmanager.Load<SoundEffect>(assetName);
        soundeffect.Play();

    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="assetName">The name of the file</param>
    /// <param name="repeat">Defines whether the music needs to repeat or not</param>
    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(contentmanager.Load<Song>(assetName));

    }

    /// <summary>
    /// Load an Effect
    /// </summary>
    /// <param name="assetName">The name of the file</param>
    /// <returns>The effect that can be found with the assetName, or null in case the assetName is empty</returns>
    public Effect GetEffect(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<Effect>(assetName);
    }

    /// <summary>
    /// Returns the ContentManager
    /// </summary>
    public ContentManager Content
    {
        get { return contentmanager; }
    }
}