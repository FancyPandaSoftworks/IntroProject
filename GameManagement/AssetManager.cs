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

    //Load a Texture2D
    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<Texture2D>(assetName);
    }

    //Load a Model
    public Model GetModel(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<Model>(assetName);
    }

    //Load a Font
    public SpriteFont GetSpriteFont(string assetName)
    {
        if (assetName == "")
            return null;
        return contentmanager.Load<SpriteFont>(assetName);
    }

    //Load a Sound
    public void Playsound(string assetName)
    {
        SoundEffect soundeffect = contentmanager.Load<SoundEffect>(assetName);
        soundeffect.Play();

    }

    //Play Music
    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(contentmanager.Load<Song>(assetName));

    }

    //Returns the ContentManager
    public ContentManager Content
    {
        get { return contentmanager; }
    }
}