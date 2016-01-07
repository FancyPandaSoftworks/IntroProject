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

    /// <summary>
    /// Returns the camera object
    /// </summary>
    public static Camera Camera
    {
        get { return GameEnvironment.camera; }
    }

    /// <summary>
    /// Returns the screen size
    /// </summary>
    public static Point Screen
    {
        get { return GameEnvironment.screen; }
    }

    /// <summary>
    /// Returns the object of the Random Class
    /// </summary>
    public static Random Random
    {
        get { return random; }
    }

    /// <summary>
    /// Return the AssetManager
    /// </summary>
    public static AssetManager AssetManager
    {
        get { return assetManager; }
    }

    /// <summary>
    /// Return the GameSettingsManager
    /// </summary>
    public static GameSettingsManager GameSettingsManager
    {
        get { return gameSettingsManager; }
    }

    /// <summary>
    /// Return the GameStateManager
    /// </summary>
    public static GameStateManager GameStateManager
    {
        get { return gameStateManager; }
    }

    /// <summary>
    /// Return the InputHelper
    /// </summary>
    public InputHelper InputHelper
    {
        get { return inputHelper; }
    }

    /// <summary>
    /// Change window mode
    /// </summary>
    /// <param name="fullscreen">Change the fullscreen status</param>
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

    /// <summary>
    /// Make an instance of the SpriteBatch
    /// </summary>
    protected override void LoadContent()
    {

        graphicsDevice = graphics.GraphicsDevice;
        DrawingHelper.Initialize(this.GraphicsDevice);
        spriteBatch = new SpriteBatch(GraphicsDevice);
        screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
    }

    /// <summary>
    /// Handles the input and allows for switching to and from fullscreen
    /// </summary>
    protected void HandleInput()
    {
        inputHelper.Update();
        if (inputHelper.KeyPressed(Keys.F5))
            SetFullscreen(!graphics.IsFullScreen);
        gameStateManager.HandleInput(inputHelper);

    }

    /// <summary>
    /// Updates the game
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
    }

    /// <summary>
    /// Draw the game
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    protected override void Draw(GameTime gameTime)
    {
        graphics.GraphicsDevice.Clear(Color.Black);
        gameStateManager.Draw(gameTime, spriteBatch);
    }
}


