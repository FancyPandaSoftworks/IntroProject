using System;
using System.Timers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

public class MusicPlayer
{
    #region Lists of sound types
    public static List<Sound> musicInstruments = new List<Sound>();
    public static List<Sound> SoundEffect3D = new List<Sound>();
    public static List<Sound> SoundEffect = new List<Sound>();
    public static List<Sound> LoopedEffect = new List<Sound>();
    public static List<Sound> Music = new List<Sound>();
    public static List<Sound> AllSounds = new List<Sound>();
    #endregion

    SpriteFont font;
    Vector2 pos = Vector2.Zero;
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    int bpm = 110;      //beats per second
    double beatLength;  //
    public static int beatCount = 0;  //
    public static int barCount = 0;   //

    //sync
    bool[] barSync = new bool[64];

    public static float dangerLevel;
    int TimerCount = 0;

    public Timer timer = new Timer();
    Timer timer2 = new Timer();
    Timer fadeOutTimer = new Timer();

    //3d sound test
    AudioListener player;
    AudioEmitter emitter;

    //----------------------------------Methods----------------------------------//

    //create a new sound, and add them to their type list
    public void NewSound(string filename, int dangerlevel = 0, int maxdangerlevel = 10)
    {
        Sound sound = new Sound(filename, dangerlevel, maxdangerlevel);

        AllSounds.Add(sound);

        if (sound.Type == "MusicInstrument")
            musicInstruments.Add(sound);
        if (sound.Type == "SoundEffect3D")
            SoundEffect3D.Add(sound);
        if (sound.Type == "SoundEffect")
            SoundEffect.Add(sound);
        if (sound.Type == "LoopedEffect")
            LoopedEffect.Add(sound);
        if (sound.Type == "Music")
            Music.Add(sound);
    }

    public MusicPlayer()
    {
        beatLength = 60000 / (double)bpm;
        timer.Interval = beatLength;
        timer.Elapsed += MusicTimer;
        timer.AutoReset = true;
    }

    //Play the music in sync
    public void SyncPlayer()
    {
        if (barSync[0])
        {
            foreach (Sound sound in musicInstruments)
            {
                if (barSync[sound.Length])
                {
                    if (dangerLevel >= sound.DangerLevel && dangerLevel < sound.MaxDangerLevel)
                    {
                        if (beatCount == 0)
                        {
                            sound.PlaySound();
                        }
                    }
                    else
                    {
                        if (sound.Playingstate == "playing")
                                sound.StopSound();
                    }
                }
            }
        }
    }

    //Call this method to play a sound
    public void SoundPlayer(string name, AudioListener listener, AudioEmitter emitter)
    {
        foreach (Sound sound in AllSounds)
        {
            if (sound.Name == name)
            {
                if (sound.Type == "SoundEffect")
                    sound.PlaySound();
                if (sound.Type == "SoundEffect3D")
                    sound.Play3DSound(listener, emitter);
                if (sound.Type == "LoopedEffect")
                    sound.PlaySound();
            }
        }
    }

    private void BeatCounter()
    {
        beatCount++;

        if (beatCount == 4)
        {
            barCount++;
            beatCount = 0;
        }
        if (barCount == 64)
        {
            barCount = 0;
        }
    }

    private void BeatSyncer()
    {
        if (beatCount == 0 || beatCount % 4 == 0)
            barSync[0] = true;
        else
            barSync[0] = false;

        if (barSync[0])
        {
            for (int i = 1; i < 63; i++)
                if (barCount % i == 0)
                    barSync[i] = true;
                else
                    barSync[i] = false;
        }

    }

    //--------------------Timers----------------------//

    //execute every beat
    private void MusicTimer(Object source, ElapsedEventArgs e)
    {
        BeatSyncer();
        SyncPlayer();
        BeatCounter();
    }

    //Metronome for debugging
    private void Metronome()
    {
        foreach (Sound sound in SoundEffect)
        {
            if (sound.Name == "ClockTick")
            {
                sound.PlaySound();

                if (barSync[0])
                    sound.Pitch(0.3f);
                else
                    sound.Pitch(0.0f);
            }
        }
    }
}