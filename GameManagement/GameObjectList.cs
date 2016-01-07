using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectList : GameObject
{
    protected List<GameObject> gameObjects;

    /// <summary>
    /// A list that contains the GameObjects
    /// </summary>
    /// <param name="id">The id used to find this object</param>
    public GameObjectList(string id = "")
    {
        gameObjects = new List<GameObject>();
    }

    /// <summary>
    /// Remove the object from the list
    /// </summary>
    /// <param name="obj">The object to be removed</param>
    public void Remove(GameObject obj)
    {
        gameObjects.Remove(obj);
        obj.Parent = null;
    }

    /// <summary>
    /// Finding the GameObject by looking for the id
    /// </summary>
    /// <param name="id">The id used to find this object</param>
    /// <returns>The reference to the object or null in case the id does not match with a object</returns>
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

    /// <summary>
    /// Property to return gameObjects, the list of all the objects
    /// </summary>
    public List<GameObject> Objects
    {
        get { return gameObjects; }
    }


    /// <summary>
    /// Inputhelper for the objects in the list
    /// </summary>
    /// <param name="inputhelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in gameObjects)
            obj.HandleInput(inputHelper);
    }

    /// <summary>
    /// Update the objects in the list
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in gameObjects)
            obj.Update(gameTime);
    }

    /// <summary>
    /// Draws the objects in the list
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        List<GameObject>.Enumerator e = gameObjects.GetEnumerator();
        while (e.MoveNext())
            e.Current.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Resets the objects in the list
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in gameObjects)
            obj.Reset();
    }
}
