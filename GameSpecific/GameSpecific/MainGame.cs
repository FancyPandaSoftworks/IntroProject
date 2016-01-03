using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MainGame : GameEnvironment
{
    static void Main()
    {
        MainGame game = new MainGame();
        game.Run();
    }

    public MainGame()
    {
        Content.RootDirectory = "Content";
        this.IsMouseVisible = false;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        gameStateManager.AddGameState("titleScreenState", new TitleScreenState());
        gameStateManager.AddGameState("playingState", new PlayingState());
        gameStateManager.AddGameState("pauseScreenState", new PauseScreenState());
        gameStateManager.SwitchTo("titleScreenState");
    }
}

