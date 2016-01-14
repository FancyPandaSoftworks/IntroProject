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
        musicPlayer.NewSound("mIn_AmbienceHigh@32@0@0");
        musicPlayer.NewSound("mIn_AmbienceLow@32@0@0");
        musicPlayer.NewSound("mIn_Violin@2@8@8", 8);
        musicPlayer.NewSound("mIn_DrumsFast@2@8@8", 7);
        musicPlayer.timer.Enabled = true;
        GameState.GameVariable = this;
        gameStateManager.AddGameState("noteViewingState", new NoteViewingState());
        gameStateManager.AddGameState("playingState", new PlayingState());
        gameStateManager.AddGameState("pauseScreenState", new PauseScreenState());
        gameStateManager.AddGameState("gameOverState", new GameOverState());
        gameStateManager.AddGameState("titleScreenState", new TitleScreenState());
        gameStateManager.SwitchTo("titleScreenState");
    }
}

