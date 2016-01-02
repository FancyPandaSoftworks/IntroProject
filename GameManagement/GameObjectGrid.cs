using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class GameObjectGrid : GameObject
{
    protected GameObject[,] grid;
    protected static int cellWidth, cellHeight;


    public GameObjectGrid(int columns, int rows, string id = "") : base(id)
    {
        cellHeight = 200;
        cellWidth = 200;
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
                grid[x, y] = null;
    }

    public void Add(GameObject obj, int x, int y)
    {
        grid[x, y] = obj;
        obj.Parent = this;

        obj.Position = new Vector3(x * cellWidth, 0, y * cellHeight);
    }

    public GameObject get(int x, int y)
    {
        if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
            return grid[x, y];
        else
            return null;
    }

    public GameObject[,] Objects
    {
        get
        {
            return grid;
        }
    }

    public Vector2 GetAnchorPosition(GameObject g)
    {
        for (int x = 0; x < Columns; x++)
            for (int y = 0; y < Rows; y++)
                if (grid[x, y] == g)
                    return new Vector2(x * cellWidth, y * cellHeight);
        return Vector2.Zero;
    }

    public int Columns
    {
        get { return grid.GetLength(0); }
    }

    public int Rows
    {
        get { return grid.GetLength(1); }
    }

    public static int CellWidth
    {
        get { return cellWidth; }
        set { cellWidth = value; }
    }

    public static int CellHeight
    {
        get { return cellHeight; }
        set { cellWidth = value; }
    }

    public override void HandleInput(InputHelper inputhelper)
    {
        foreach (GameObject obj in grid)
            obj.HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in grid)
            obj.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in grid)
            obj.Reset();
    }
}
