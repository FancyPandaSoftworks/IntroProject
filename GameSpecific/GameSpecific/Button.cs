using Microsoft.Xna.Framework;
using System;

/// <summary>
/// A clickable button
/// </summary>
class Button : Object2D
{
    protected bool buttonIsPressed, isMouseOver;

    /// <summary>
    /// Create the button
    /// </summary>
    /// <param name="assetName">What the button looks like</param>
    /// <param name="layer">In what layer it is drawn</param>
    /// <param name="id">The id used to find this object</param>
    public Button(string assetName, int layer = 0, string id = "")
        : base(assetName, layer, id)
    {
        buttonIsPressed = false;
        isMouseOver = false;
    }
    
    /// <summary>
    /// HandleInput for the button
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        //Checking whether the mouse is hovering over a button or not
        if (BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
            isMouseOver = true;
        else
            isMouseOver = false;

        //Checking whether you are pressing the button or not
        if (inputHelper.MouseLeftButtonPressed() && isMouseOver)
            buttonIsPressed = true;
        else
            buttonIsPressed = false;
    }

    /// <summary>
    /// Property to check the status of the button
    /// </summary>
    public bool ButtonIsPressed
    {
        get { return buttonIsPressed; }
    }

    public bool IsMouseOver
    {
        get { return isMouseOver; }
    }
}
