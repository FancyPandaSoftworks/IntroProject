using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The very basic start of the game
/// </summary>
class MainGame : GameEnvironment
{
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

        GameState.GameVariable = this;
        gameStateManager.AddGameState("titleScreenState", new TitleScreenState());
        gameStateManager.AddGameState("noteViewingState", new NoteViewingState());
        gameStateManager.AddGameState("playingState", new PlayingState());
        gameStateManager.AddGameState("pauseScreenState", new PauseScreenState());
        gameStateManager.SwitchTo("titleScreenState");
    }
}

