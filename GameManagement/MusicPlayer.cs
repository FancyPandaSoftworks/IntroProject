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

    /*static void Main()
    {
        MusicPlayer musicPlayer = new MusicPlayer();
        musicPlayer.Run();
    }*/

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

        Console.WriteLine("The {0} {1} has loaded succesfully, with length of {2} bars fadein time of {3} beats and fadeout time of {4} beats", sound.Type, sound.Name, sound.Length, sound.FadeIn, sound.FadeOut);
    }

    public MusicPlayer()
    {
        //graphics = new GraphicsDeviceManager(this);
        beatLength = 60000 / (double)bpm;
        timer.Interval = beatLength;
        timer.Elapsed += MusicTimer;
        timer.AutoReset = true;
    }

    //Play the music in sync
    //TODO add fadeIn/Out
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
                            //if (sound.FadeOut >= 0)
                            //{
                            //    sound.Playingstate = "fadingOut";
                            //    //FadeOut(sound);
                            //}
                            //else
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

    //add new sounds here: newsound("type_name@length@fadein@fadeout")
    //protected override void loadcontent()
    //{
    //player = new audiolistener();
    //emitter = new audioemitter();
    /*content.rootdirectory = "content";
    spritebatch = new spritebatch(graphicsdevice);
    font = content.load<spritefont>("spritefont1");*/
    /*#region musicinstruments
    newsound("min_ambiencehigh@32@0@0");
    newsound("min_ambiencelow@32@0@0");
    newsound("min_violin@2@8@8", 4);
    newsound("min_drumsfast@2@8@8", 4);
    #endregion*/
    //newsound("3ds_beeptone");
    //newsound("sfx_clocktick");
    //timer.enabled = true;
    //}

    //Draw info on screen (debug)
    /*protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        spriteBatch.DrawString(font, "Beats: " + beatCount, pos, Color.Black);
        pos.Y += 40;
        spriteBatch.DrawString(font, "Bars:   " + barCount, pos, Color.Black);
        pos.Y += 80;
        spriteBatch.DrawString(font, "DangerLevel: " + dangerLevel, pos, Color.Black);
        pos.Y += 80;
        spriteBatch.DrawString(font, "GameTime: " + gameTime.TotalGameTime.Seconds, pos, Color.Black);
        pos.Y = 0;
        spriteBatch.End();
    }*/

    //protected override void Update(GameTime gameTime)
    //{
    //    emitter.Position = new Vector3(
    //        (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds), // left right
    //        (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds), // front back
    //        (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) // up down
    //                                                );

    //    foreach (Sound sound in SoundEffect3D)
    //    {
    //        sound.SoundApply3D(player, emitter);
    //    }
    //}

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

        /*if (TimerCount == 8) // ??
        {
            timer2.Enabled = false;
            TimerCount = 0;
        }*/
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

    //TIJDELIJK ERUIT GECOMMENT
    //public void FadeOut(Sound sound)
    //{
    //    Console.WriteLine("FadeOutTimer start");
    //    sound.FadeOutTimer.Elapsed += FadeOutTimer;
    //    sound.FadeOutTimer.Interval = beatLength * sound.FadeOut / 100;
    //    sound.FadeOutTimer.AutoReset = true;
    //    sound.FadeOutTimer.Enabled = true;
    //}

    //--------------------Timers----------------------//

    //execute every beat
    private void MusicTimer(Object source, ElapsedEventArgs e)
    {
        //if (dangerLevel >= 5)
        //    dangerLevel = 0;
        //else
        //    dangerLevel += 0.05f;
        Console.WriteLine("BarCount = {0}", barCount);

        BeatSyncer();
        //Metronome();
        //SyncPlayer();
        BeatCounter();
    }

    //private void FadeOutTimer(Object source, ElapsedEventArgs e)
    //{
    //    foreach (Sound sound in musicInstruments)
    //    {
    //        if (sound.Playingstate == "fadingOut")
    //        {
    //            sound.FadeOutCounter++;


    //            if (sound.FadeOutCounter < 100)
    //                sound.Volume = 1.0f - (float)sound.FadeOutCounter / 100;
    //            else
    //            {
    //                sound.FadeOutCounter = 0;
    //                sound.FadeOutTimer.Enabled = false;
    //                sound.StopSound();
    //            }
    //            Console.WriteLine("Timer executed {0} times", sound.FadeOutCounter);
    //            Console.WriteLine("volume = {0}", sound.Volume);
    //        }
    //    }
    //}


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