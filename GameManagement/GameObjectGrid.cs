using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class GameObjectGrid : GameObject
{
    protected GameObject[,] grid;
    protected static int cellWidth, cellHeight;

    //Create the world
    public GameObjectGrid(int columns, int rows, string id = "") : base(id)
    {
        cellHeight = 200;
        cellWidth = 200;
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
                grid[x, y] = null;
    }

    //Puts the GameObject to the GameObjectGrid
    public void Add(GameObject obj, int x, int y)
    {
        grid[x, y] = obj;
        obj.Parent = this;

        obj.Position = new Vector3(x * cellWidth, 0, y * cellHeight);
    }

    //returns the something in the grid
    public GameObject get(int x, int y)
    {
        if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
            return grid[x, y];
        else
            return null;
    }
    //property to get the grid
    public GameObject[,] Objects
    {
        get
        {
            return grid;
        }
    }

    //Gets the position in the grid
    public Vector2 GetAnchorPosition(GameObject g)
    {
        for (int x = 0; x < Columns; x++)
            for (int y = 0; y < Rows; y++)
                if (grid[x, y] == g)
                    return new Vector2(x * cellWidth, y * cellHeight);
        return Vector2.Zero;
    }

    //gets the width of the grid
    public int Columns
    {
        get { return grid.GetLength(0); }
    }

    //gets the height of the grid
    public int Rows
    {
        get { return grid.GetLength(1); }
    }
    
    //return the width of the grid
    public static int CellWidth
    {
        get { return cellWidth; }
        set { cellWidth = value; }
    }

    //return the height of the grid
    public static int CellHeight
    {
        get { return cellHeight; }
        set { cellWidth = value; }
    }

    //inputhelper for the gameobjects in the grid
    public override void HandleInput(InputHelper inputhelper)
    {
        foreach (GameObject obj in grid)
            obj.HandleInput(inputHelper);
    }

    //draws the gameobject in the grid
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in grid)
            obj.Draw(gameTime, spriteBatch);
    }

    //reset the gameobject in the grid
    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in grid)
            obj.Reset();
    }
}
