using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The very basic start of the game
/// </summary>
class MainGame : GameEnvironment
{
    MusicPlayer musicPlayer;
    /// <summary>
    /// The very start of the program
    /// </summary>
    static void Main()
    {
        MainGame game = new MainGame();
        game.Run();
    }

    /// <summary>
    /// The constructor
    /// </summary>
    public MainGame()
    {
        Content.RootDirectory = "Content";
        this.IsMouseVisible = false;
    }

    /// <summary>
    /// Loading in all contents required
    /// </summary>
    protected override void LoadContent()
    {
        base.LoadContent();
        musicPlayer = new MusicPlayer();

        #region Sounds
        musicPlayer.NewSound("mIn_AmbienceHigh@32@0@0");
        musicPlayer.NewSound("mIn_AmbienceLow@32@0@0");
        musicPlayer.NewSound("mIn_Violin@2@8@8", 8);
        musicPlayer.NewSound("mIn_DrumsFast@2@8@8", 8);
        musicPlayer.NewSound("mIn_DrumsSlow@2@0@0", 2, 7);

        musicPlayer.NewSound("SFX_paperrustle");
        musicPlayer.NewSound("SFX_paperrustle2");
        musicPlayer.NewSound("SFX_doorcreak");
        musicPlayer.NewSound("SFX_MouseClick");
        musicPlayer.NewSound("SFX_MouseOver");
        musicPlayer.NewSound("SFX_GunShot");
        musicPlayer.NewSound("SFX_GameOver");
        musicPlayer.NewSound("SFX_WindAMbience");

        musicPlayer.NewSound("LFX_Footsteps1@8");
        musicPlayer.NewSound("LFX_Footsteps2@4");

        musicPlayer.NewSound("3DS_Monster0");
        musicPlayer.NewSound("3DS_Monster1");
        musicPlayer.NewSound("3DS_Monster2");
        musicPlayer.NewSound("3DS_Monster3");
        musicPlayer.NewSound("3DS_Monster4");
        musicPlayer.NewSound("3DS_Monster5");
        musicPlayer.NewSound("3DS_Monster6");
        musicPlayer.NewSound("3DS_Monster7");
        musicPlayer.NewSound("3DS_Monster8");
        musicPlayer.NewSound("3DS_Monster9");

        musicPlayer.NewSound("mus_MainMenu");
        #endregion

        musicPlayer.timer.Enabled = true;
        GameState.GameVariable = this;
        gameStateManager.AddGameState("noteViewingState", new NoteViewingState());
        gameStateManager.AddGameState("playingState", new PlayingState());
        gameStateManager.AddGameState("pauseScreenState", new PauseScreenState());
        gameStateManager.AddGameState("gameOverState", new GameOverState());
        gameStateManager.AddGameState("endGameState", new EndGameState());
        gameStateManager.AddGameState("creditState", new CreditState());
        gameStateManager.AddGameState("titleScreenState", new TitleScreenState());
        SetFullscreen(true);
        gameStateManager.SwitchTo("titleScreenState");
    }
}

