using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectList : GameObject
{
    protected List<GameObject> gameObjects;

    //list that contains the GameObjects
    public GameObjectList(string id = "")
    {
        gameObjects = new List<GameObject>();
    }

    //remove the object from the list
    public void Remove(GameObject obj)
    {
        gameObjects.Remove(obj);
        obj.Parent = null;
    }

    //finding the GameObject by looking for the id
    public GameObject Find(string id)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj.ID == id)
                return obj;
            if (obj is GameObjectList)
            {
                GameObjectList objlist = obj as GameObjectList;
                GameObject subobj = objlist.Find(id);
                if (subobj != null)
                    return subobj;
            }
        }
        return null;
    }

    //propertt to return gameObjects
    public List<GameObject> Objects
    {
        get { return gameObjects; }
    }

   //Inputhelper for the objects in the list
    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in gameObjects)
            obj.HandleInput(inputHelper);
    }

    //update the objects in the list
    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in gameObjects)
            obj.Update(gameTime);
    }

    //draws the objects in the list
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        List<GameObject>.Enumerator e = gameObjects.GetEnumerator();
        while (e.MoveNext())
            e.Current.Draw(gameTime, spriteBatch);
    }

    //resets the objects in the list
    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in gameObjects)
            obj.Reset();
    }
}
