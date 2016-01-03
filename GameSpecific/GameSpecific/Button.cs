using Microsoft.Xna.Framework;
using System;

class Button : Object2D
{
    protected bool buttonIsPressed;

    public Button(string assetName, int layer = 0, string id = "")
        : base(assetName, layer, id)
    {
        buttonIsPressed = false;
    }
    
    //HandleInput for the button
    public override void HandleInput(InputHelper inputHelper)
    {
        inputHelper.Update();
        Console.WriteLine(inputHelper.MouseLeftButtonPressed());
        
        //Checking whether you are pressing the button or not
        if (inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
        {
            Console.WriteLine(true);
            buttonIsPressed = true;
        }

        else
            buttonIsPressed = false;
    }

    //Is the button pressed?
    public bool ButtonIsPressed
    {
        get { return buttonIsPressed; }
    }
}
