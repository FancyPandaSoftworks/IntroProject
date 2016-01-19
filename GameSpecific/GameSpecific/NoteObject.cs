using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

/// <summary>
/// The class representing notes
/// </summary>
public class NoteObject : Object3D
{
    public static List<string> idList;
    private string noteID;
    private bool inVicinity;

    /// <summary>
    /// Constructing the note
    /// </summary>
    /// <param name="noteID">What note this note should be</param>
    public NoteObject(string noteID) : base("Misc Level Objects\\Note\\Note Model", "note")
    {
        this.noteID = noteID;
    }

    /// <summary>
    /// What should happen if the note is picked up
    /// </summary>
    public void PickUp()
    {
        NoteViewingState state = GameEnvironment.GameStateManager.GetGameState("noteViewingState") as NoteViewingState;
        if (state != null)
        {
            state.NoteID = noteID;
            GameEnvironment.GameStateManager.SwitchTo("noteViewingState");
        }
        else throw(new InvalidOperationException());
    }
    
    /// <summary>
    /// Handling the input concernig the note
    /// </summary>
    /// <param name="inputhelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputhelper)
    {
        if (inputhelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.E) && inVicinity)
            PickUp();
    }
}
