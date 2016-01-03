using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameEnvironment : Game
{
    protected SpriteBatch spriteBatch;
    protected Model model;
    protected Matrix modelScale;
    protected GraphicsDeviceManager graphics;
    protected InputHelper inputHelper;
    protected static GameStateManager gameStateManager;
    protected static Random random;
    protected static AssetManager assetManager;
    protected static GameSettingsManager gameSettingsManager;
    protected static Camera camera;
    protected static Point screen;
    protected static GraphicsDevice graphicsDevice;

    public GameEnvironment()
    {
        modelScale = Matrix.CreateScale(1, 1, 1);
        inputHelper = new InputHelper();
        gameStateManager = new GameStateManager();
        random = new Random();
        assetManager = new AssetManager(Content);
        gameSettingsManager = new GameSettingsManager();
        graphics = new GraphicsDeviceManager(this);
        camera = new Camera();
        //IsMouseVisible = false;
    }
    public static GraphicsDevice Graphics
    {
        get { return graphicsDevice; }

    }

    //Returns the camera
    public static Camera Camera
    {
        get { return GameEnvironment.camera; }
    }

    //Returns the screen size
    public static Point Screen
    {
        get { return GameEnvironment.screen; }
    }

    //Returns an object of the Random Class
    public static Random Random
    {
        get { return random; }
    }

    //Return the AssetManager
    public static AssetManager AssetManager
    {
        get { return assetManager; }
    }

    //Return the GameSettingsManager
    public static GameSettingsManager GameSettingsManager
    {
        get { return gameSettingsManager; }
    }

    //Return the GameStateManager
    public static GameStateManager GameStateManager
    {
        get { return gameStateManager; }
    }

    //Return the InputHelper
    public InputHelper InputHelper
    {
        get { return inputHelper; }
    }

    //Change window mode
    public void SetFullscreen(bool fullscreen = true)
    {
        float scalex = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / (float)screen.X;
        float scaley = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / (float)screen.Y;
        float finalescale = 1f;

        if (!fullscreen)
        {
            if (scalex < 1f || scaley < 1f)
                finalescale = Math.Min(scalex, scaley);
        }

        else
        {
            finalescale = scalex;
            if (Math.Abs(1 - scaley) < Math.Abs(1 - scalex))
                finalescale = scaley;
        }
        graphics.PreferredBackBufferWidth = (int)(finalescale * screen.X);
        graphics.PreferredBackBufferHeight = (int)(finalescale * screen.Y);
        graphics.IsFullScreen = fullscreen;
        graphics.ApplyChanges();
        inputHelper.Scale = new Vector2((float)GraphicsDevice.Viewport.Width / screen.X, (float)GraphicsDevice.Viewport.Height / screen.Y);
        modelScale = Matrix.CreateScale(inputHelper.Scale.X, inputHelper.Scale.Y, 1);
    }

    //Make an instance of the SpriteBatch
    protected override void LoadContent()
    {

        graphicsDevice = graphics.GraphicsDevice;
        DrawingHelper.Initialize(this.GraphicsDevice);
        spriteBatch = new SpriteBatch(GraphicsDevice);
        screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
    }

    //Handles the input and allows for switching to and from fullscreen
    protected void HandleInput()
    {
        inputHelper.Update();
        if (inputHelper.KeyPressed(Keys.F5))
            SetFullscreen(!graphics.IsFullScreen);
        gameStateManager.HandleInput(inputHelper);

    }

    //Updates the game
    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
    }

    //Draw the game
    protected override void Draw(GameTime gameTime)
    {
        graphics.GraphicsDevice.Clear(Color.Black);
        gameStateManager.Draw(gameTime, spriteBatch);
    }
}


