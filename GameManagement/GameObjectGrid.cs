using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class GameObjectGrid : GameObject
{
    protected GameObject[,] grid;
    public static int cellWidth, cellHeight;

    /// <summary>
    /// Create a 2-dimensional grid
    /// </summary>
    /// <param name="columns">The amount of colums</param>
    /// <param name="rows">The amount of rows</param>
    /// <param name="id">The id used to find this object</param>
    public GameObjectGrid(int columns, int rows, string id = "") : base(id)
    {
        cellHeight = 200;
        cellWidth = 200;
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
                grid[x, y] = null;
        
    }

    /// <summary>
    /// Puts a GameObject in the GameObjectGrid in the given coördinates
    /// </summary>
    /// <param name="obj">The object to add</param>
    /// <param name="x">The x-coördinate</param>
    /// <param name="y">The y-coördinate</param>
    public void Add(GameObject obj, int x, int y)
    {
        grid[x, y] = obj;
        obj.Parent = this;

        obj.Position = new Vector3(x * cellWidth, 0, y * cellHeight);
    }

    /// <summary>
    /// Returns the object in the grid at the given coördinates
    /// </summary>
    /// <param name="x">The x-coördinate</param>
    /// <param name="y">The y-coördinate</param>
    /// <returns>Returns the object at the given coördinate, or if the coördinates are empty return null</returns>
    public GameObject Get(int x, int y)
    {
        if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
            return grid[x, y];
        else
            return null;
    }

    /// <summary>
    /// Property to get the grid
    /// </summary>
    public GameObject[,] Objects
    {
        get
        {
            return grid;
        }
    }

    /// <summary>
    /// Gets the position in the grid
    /// </summary>
    /// <param name="gameObject">The gameobject for which the anchorposition is desired</param>
    /// <returns>Returns a Vector2 with the position or return the Vector2.Zero in case the object is not in the grid</returns>
    public Vector2 GetAnchorPosition(GameObject gameObject)
    {
        for (int x = 0; x < Columns; x++)
            for (int y = 0; y < Rows; y++)
                if (grid[x, y] == gameObject)
                    return new Vector2(x * cellWidth, y * cellHeight);
        return Vector2.Zero;
    }

    /// <summary>
    /// Gets the width of the grid
    /// </summary>
    public int Columns
    {
        get { return grid.GetLength(0); }
    }

    /// <summary>
    /// Gets the height of the grid
    /// </summary>
    public int Rows
    {
        get { return grid.GetLength(1); }
    }
    
    /// <summary>
    /// Return the width of the cells
    /// </summary>
    public static int CellWidth
    {
        get { return cellWidth; }
        set { cellWidth = value; }
    }

    /// <summary>
    /// Return the height of the cells
    /// </summary>
    public static int CellHeight
    {
        get { return cellHeight; }
        set { cellHeight = value; }
    }

    /// <summary>
    /// Inputhelper for the gameobjects in the grid
    /// </summary>
    /// <param name="inputhelper">The inputhelper to react to input</param>
    public override void HandleInput(InputHelper inputhelper)
    {
        foreach (GameObject obj in grid)
            obj.HandleInput(inputHelper);
    }

    /// <summary>
    /// Draws the gameobjects in the grid
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in grid)
                obj.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Reset the gameobjects in the grid
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in grid)
            obj.Reset();
    }
}
