using System;
using System.Timers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


public class Sound
{
    private string type;
    private string name;
    private string playingState = "stopped"; //stopped, playing, fadingIn, fadingOut
    private int dangerLevel, maxDangerLevel;
    private bool instancePlaying = false;   //to check if an instance in playing or to create a new instance
    private int length;  // in bars
    private int fadeIn;     // in beats
    private int fadeOut;    // in beats
    private int fadeOutCounter = 0;
    private int fadeInCounter = 0;
    Timer fadeOutTimer = new Timer();

    SoundEffect sound;
    SoundEffectInstance iSound;

    protected void Initialize(string filename)
    {
        sound = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Music\\" + filename);
        iSound = sound.CreateInstance();
    }

    public Sound(string filename, int dangerlevel = 0, int maxdangerlevel = 10)
    {
        dangerLevel = dangerlevel;
        maxDangerLevel = maxdangerlevel;

        #region Getting File Info

        #region Check Type
        if (filename.Substring(0, 4) == "SFX_")
            type = "SoundEffect";
        else if (filename.Substring(0, 4) == "3DS_")
            type = "SoundEffect3D";
        else if (filename.Substring(0, 4) == "LFX_")
            type = "LoopedEffect";
        else if (filename.Substring(0, 4) == "mIn_")
            type = "MusicInstrument";
        else if (filename.Substring(0, 4) == "mus_")
            type = "Music";
        else
            type = "Unknown type";
        #endregion

        #region Initializing Variables
        //default values
        string localName = "Unknown name";
        int localLength = 0;
        int localFadeIn = 0;
        int localFadeOut = 0;

        // counters
        int ATcount = 0; //Count number of '@''s in the filename
        int i;
        int j;
        #endregion

        #region Get File Info
        i = 4;
        j = i;
        if (type == "SoundEffect")
        {
            #region SoundEffect Info

            localName = filename.Substring(i, filename.Length - i);

            #endregion
        }

        if (type == "Music")
        {
            #region SoundEffect Info

            localName = filename.Substring(i, filename.Length - i);

            #endregion
        }

        if (type == "LoopedEffect")
        {
            #region LoopedEffect Info
            //Check for marks
            while (i < 100)
            {
                //Check for out of bound error
                if (i == filename.Length)
                {
                    Console.WriteLine("Out of Bound");
                    break;
                }
                //for first marker (name)
                if (filename[i] == '@' && ATcount == 0)
                {
                    ATcount++;
                    localName = filename.Substring(j, i - j);
                    i++;
                    j = i;
                }
                //Check Rest of String
                if (ATcount == 1)
                {
                    if (j == filename.Length - 1)
                        localLength = filename[j];
                    else
                        break;
                    break;
                }
                i++;
            }   //end while
            #endregion
        }

        if (type == "SoundEffect3D")
        {
            #region SoundEffect3D info
            localName = filename.Substring(i, filename.Length - i);
            #endregion
        }

        if (type == "MusicInstrument")
        {
            #region MusicInstrument Info
            //Check for marks
            while (i < 100)
            {
                //Check for out of bound error
                if (i == filename.Length)
                {
                    Console.WriteLine("Out of Bound");
                    break;
                }
                //for first marker (name)
                if (filename[i] == '@' && ATcount == 0)
                {
                    ATcount++;
                    localName = filename.Substring(j, i - j);
                    i++;
                    j = i;
                }
                //Check for second marker (length)
                if (filename[i] == '@' && ATcount == 1)
                {
                    ATcount++;
                    length = Int32.Parse(filename.Substring(j, (i - j)));
                    i++;
                    j = i;
                }
                //check for third marker (FadeIn)
                if (filename[i] == '@' && ATcount == 2)
                {
                    ATcount++;
                    fadeIn = Int32.Parse(filename.Substring(j, (i - j)));
                    i++;
                    j = i;
                }
                //check rest of the string
                if (ATcount == 3)
                {
                    localFadeOut = Int32.Parse(filename.Substring(j, filename.Length - j));
                    break;
                }

                i++;
            }   //end while
            #endregion
        }

        if (type == "Unknown type")
        {
            #region Unknown type Info

            localName = filename.Substring(0, filename.Length);

            #endregion
        }
        #endregion

        #region Declare File info
        name = localName;

        fadeIn = (int)localFadeIn - 48;
        fadeOut = (int)localFadeOut - 48;
        #endregion

        #endregion

        Initialize(filename);
    }


    public void PlaySound(float volume = 1)
    {
        if (instancePlaying && type != "LoopedEffect")
            StopSound();

        if (!instancePlaying)
        {
            iSound = sound.CreateInstance();

            if (type == "LoopedEffect" || type == "MusicInstrument" || type == "Music")
                iSound.IsLooped = true;
            else
                iSound.IsLooped = false;
        }

        Console.WriteLine("Play: {0}", name);
        iSound.Play();
        instancePlaying = true;
        iSound.Volume = volume;
        playingState = "playing";
    }

    public void StopSound()
    {
        if (playingState != "stopped")
        {
            Console.WriteLine("Stop: {0}", name);

            iSound.Stop(true);
            iSound.Dispose();
            playingState = "stopped";
            instancePlaying = false;
        }
    }

    public void Play3DSound(AudioListener listener, AudioEmitter emitter)
    {
        iSound.Apply3D(listener, emitter);
        playingState = "playing";
        iSound.Play();
    }



    #region Accessors
    public string Name
    {
        get { return name; }
    }

    public int DangerLevel
    {
        get { return dangerLevel; }
    }

    public int MaxDangerLevel
    {
        get { return maxDangerLevel; }
    }

    public int Length
    {
        get { return length; }
    }

    public int FadeIn
    {
        get { return fadeIn; }
    }

    public int FadeOut
    {
        get { return fadeOut; }
    }

    public string Type
    {
        get { return type; }
    }

    public void Pitch(float value)
    {
        iSound.Pitch = value;
    }

    public void SoundApply3D(AudioListener listener, AudioEmitter emitter)
    {
        iSound.Apply3D(listener, emitter);
    }

    public float Volume
    {
        get { return iSound.Volume; }
        set { iSound.Volume = value; }
    }

    public string Playingstate
    {
        get { return playingState; }
        set { playingState = value; }
    }

    public int FadeInCounter
    {
        get { return fadeInCounter; }
        set { fadeInCounter = value; }
    }

    public int FadeOutCounter
    {
        get { return fadeOutCounter; }
        set { fadeOutCounter = value; }
    }

    public Timer FadeOutTimer
    {
        get { return fadeOutTimer; }
        set { fadeOutTimer = value; }
    }

    public SoundState SoundState
    {
        get { return iSound.State; }
    }

    #endregion
}