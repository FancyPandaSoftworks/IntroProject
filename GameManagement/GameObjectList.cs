using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectList : GameObject
{
    protected List<GameObject> gameObjects;

    public GameObjectList(string id = "")
    {
        gameObjects = new List<GameObject>();
    }

    public void Remove(GameObject obj)
    {
        gameObjects.Remove(obj);
        obj.Parent = null;
    }

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

    public List<GameObject> Objects
    {
        get { return gameObjects; }
    }

    public override void HandleInput(InputHelper inputhelper)
    {
        foreach (GameObject obj in gameObjects)
            obj.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in gameObjects)
            obj.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        List<GameObject>.Enumerator e = gameObjects.GetEnumerator();
        while (e.MoveNext())
            e.Current.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in gameObjects)
            obj.Reset();
    }
}
