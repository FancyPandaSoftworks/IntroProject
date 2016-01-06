using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class NoteObject : Object3D
{
    public static List<string> idList;
    private string noteID;
    private bool inVicinity;

    public NoteObject(string noteID) : base("Axis", "note")
    {
        this.noteID = noteID;
    }

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

    public override void HandleInput(InputHelper inputhelper)
    {
        if (inputhelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.E) && inVicinity)
            PickUp();
    }
}
