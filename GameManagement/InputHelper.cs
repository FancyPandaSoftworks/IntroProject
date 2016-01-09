using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// The class which manages the input
/// </summary>
public class InputHelper
{
    protected MouseState currentMouseState, previousMouseState;
    protected KeyboardState currentKeyboardState, previousKeyboardState;
    protected Vector2 scale;

    public InputHelper()
    {
        scale = Vector2.One;
    }

    /// <summary>
    /// Update the current keyboardstate and mousestate
    /// </summary>
    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
    }

    /// <summary>
    /// Property for the scale of the screen
    /// </summary>
    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    /// <summary>
    /// Property for the position of the mouse
    /// </summary>
    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y) / scale; }
    }

    /// <summary>
    /// Check whether the left mouse button is pressed
    /// </summary>
    /// <returns>True if the Mouse is pressed, thus down now but released in the last state, false if it is not pressed</returns>
    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    /// <summary>
    /// Check whether the left mouse button is down
    /// </summary>
    /// <returns>True if the key is down, and false if it is not down</returns>
    public bool MouseLeftButtonDown()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }

    /// <summary>
    /// Check whether a given key is down
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns>True if the key is pressed, thus down now but released in the last state, false if it is not pressed</returns>
    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    /// <summary>
    /// Check whether the given key is down
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns>True if the key is down, false if the key is not down</returns>
    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    /// <summary>
    /// Check whether a key has been pressed
    /// </summary>
    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }
}

