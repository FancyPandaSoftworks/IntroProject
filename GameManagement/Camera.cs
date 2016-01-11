using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Camera : Object3D
{

    public float viewAngleX, viewAngleY;
    Vector3 viewVertex;
    Vector2 prevMousePos, mouseDiff;

    /// <summary>
    /// Creates a new 3D camera
    /// </summary>
    /// <param name="id">The id used to find this object</param>
    public Camera(string id = "")
        : base("", id)
    {
        prevMousePos = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
    }

    /// <summary>
    /// Return the point the player looks at
    /// </summary>
    public Vector3 ViewVertex
    {
        get { return viewVertex; }
        set { viewVertex = value; }
    }

    /// <summary>
    /// Checks the difference between the old and new mouse position
    /// </summary>
    /// <param name="inputHelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        inputHelper.Update();
        mouseDiff.X = inputHelper.MousePosition.X - prevMousePos.X;
        mouseDiff.Y = inputHelper.MousePosition.Y - prevMousePos.Y;
        prevMousePos = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        Mouse.SetPosition((int)prevMousePos.X, (int)prevMousePos.Y);
    }

    /// <summary>
    /// Updates the rotation of the camera in relation to the new mouse position
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    public override void Update(GameTime gameTime)
    {
        viewAngleX += mouseDiff.X * 0.005f;
        viewAngleY -= mouseDiff.Y * 0.005f;
        if (viewAngleY > 1)
            viewAngleY = 1;
        if (viewAngleY < -1)
            viewAngleY = -1;
        viewVertex = new Vector3(position.X + (float)(Math.Cos(viewAngleX) * Math.Cos(viewAngleY)),
                                position.Y + (float)Math.Sin(viewAngleY),
                                position.Z + (float)(Math.Sin(viewAngleX) * Math.Cos(viewAngleY)));
        Console.WriteLine(viewVertex);
        
        base.Update(gameTime);
    }
}