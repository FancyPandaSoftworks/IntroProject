using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// The state where the game starts of in
/// </summary>
class TitleScreenState : GameState
{
    protected Button continueButton, newGameButton, exitButton;
    private int part;
    private Level level;
    Player player;

    public TitleScreenState()
    {
        /*
        //Add a background sprite
        Object2D background = new Object2D("White Sprite", 0);
        gameObjects.Add(background);
        */

        player = new Player(new Vector3(200, 200f, 200));
        player.ViewVertex = new Vector3(200,
                        200,
                        201);
        level = new Level(player);

        //Add a continue button
        continueButton = new Button("White Sprite", 0);
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2 - 200);
        gameObjects.Add(continueButton);

        //Add a new game button
        newGameButton = new Button("white Sprite", 0);
        newGameButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2 - 100);
        gameObjects.Add(newGameButton);

        //Add an exit button
        exitButton = new Button("white Sprite", 0);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2);
        gameObjects.Add(exitButton);

        part = 0;
    }

    public void ResetPositions()
    {
        continueButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2 - 200);
        newGameButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2 - 100);
        exitButton.Position = new Vector2((GameEnvironment.Screen.X - continueButton.Width) / 2, (GameEnvironment.Screen.X - continueButton.Width) / 2);
    }

    /// <summary>
    /// Handle the input
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {        
        base.HandleInput(inputHelper);
        inputHelper.Update();

        //Check if the playbutton is being pressed to go to the game
        if (continueButton.ButtonIsPressed)
        {
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            //TODO: VOEG TOE DAT JE BEGINT VANAF HET LAATSTE CHECKPOINT
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        if (newGameButton.ButtonIsPressed)
        {
            game.IsMouseVisible = false;
            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            //TODO: voeg een waarschuwing toe(?)
            //TODO: verwijder de laatste checkpoint in de txt file en maak er 0 van ofzo en begin bij kamer 1
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        if (exitButton.ButtonIsPressed)
        {
            game.Exit();
        }

    }

    /// <summary>
    /// Draw the menu
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    { 
        //Draw the endless room
        part +=2;
        part = part % 60;
        DrawEndless(part,gameTime,spriteBatch);

        //Draw the Menu
        spriteBatch.Begin();
        level.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
        spriteBatch.End();

        //Go back to 3D mode
        spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
        spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        //Resetting the positions of the buttons
        ResetPositions();
    }

    private void DrawEndless(int part,GameTime gameTime, SpriteBatch spriteBatch)
    {
        level = new Level(player);

        GameObjectGrid grid = new GameObjectGrid(20,20,"grid");
        
        for (int i = 0; i < grid.Rows; i++)
        {
            grid.Add(new WallTile("01"), 0, i);
            grid.Add(new WallTile("01"), 2, i);
            grid.Add(new PathTile("01"), 1, i);
        }

        for (int x = 0; x < grid.Objects.GetLength(0); x++)
        {
            for (int y = 0; y < grid.Objects.GetLength(1); y++)
            {
                if (grid.Objects[x, y] != null)
                {
                    grid.Objects[x, y].Position -= new Vector3(0, 0, GameObjectGrid.CellWidth * part / 60);
                    if (grid.Objects[x, y] is WallTile)
                    {
                        grid.Objects[x, y].Position += new Vector3(0, 200, 0);
                    }
                }
            }
        }

        level.Add(grid);

        level.Parent = this;

        foreach (GameObject obj in level.Objects)
        {
            obj.Parent = level;
        }
    }
}
