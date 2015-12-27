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

    public MainGame() : base()
    {        
        Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        gameStateManager.AddGameState("playingState", new PlayingState());
        gameStateManager.AddGameState("gameOverState", new GameOverState());
        gameStateManager.SwitchTo("playingState");
    }
}

