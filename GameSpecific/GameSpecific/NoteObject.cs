using System;
using System.Collections.Generic;

public class NoteObject : Object3D
{
    public static List<string> idList;
    private string noteID;

    public NoteObject(string noteID)
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
}
