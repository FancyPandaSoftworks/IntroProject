using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

class Level
{
    private Dictionary<Point, Tile> tileList;
    private List<Point> keyList;
    private Tile newTile;
    private Point position;
    private Random random;
    private int maxTiles;

    public static void Main()
    {
        Level level = new Level(20);

    }

    public Level(int maxTiles = 10)
    {
        tileList = new Dictionary<Point, Tile>();
        keyList = new List<Point>();
        newTile = new EntryTile();
        random = new Random();

        newTile.gridPosition = Point.Zero;
        position = newTile.gridPosition;
        tileList.Add(position, newTile);
        this.maxTiles = maxTiles;

        CreateMainPath();

        int i;
        i = random.Next(1, maxTiles / 4);
        while (i > 0)
        {
            CreateSidePath(random.Next(3, maxTiles / 2));
            i--;
        }
    }

    private void CreateMainPath()
    {
        while (maxTiles >= 0)
        {
            List<Point> possiblePositions = new List<Point>();

            //Look where a Tile can be placed
            Point position = new Point(newTile.gridPosition.X - 1, newTile.gridPosition.Y);

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            position.X = newTile.gridPosition.X;
            position.Y = newTile.gridPosition.Y + 1;

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            position.X = newTile.gridPosition.X + 1;
            position.Y = newTile.gridPosition.Y;

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);

            position.X = newTile.gridPosition.X;
            position.Y = newTile.gridPosition.Y - 1;

            if (CanPlaceMainPathTile(position))
                possiblePositions.Add(position);


            if (maxTiles > 0)
            {
                //Choose where to place the Tile
                newTile = CreateMainPathTile(possiblePositions[random.Next(0, possiblePositions.Count - 1)]);
                tileList.Add(newTile.gridPosition, newTile);
            }
            else
            {
                //Choose where to place the Tile
                newTile = CreateExitTile(possiblePositions[random.Next(0, possiblePositions.Count - 1)]);
                tileList.Add(newTile.gridPosition, newTile);
            }

            keyList.Add(newTile.gridPosition);

            Console.WriteLine("X:" + newTile.gridPosition.X + "\nY:" + newTile.gridPosition.Y + "");
            maxTiles--;

        }

    }

    private Tile CreateMainPathTile(Point point)
    {
        return new MainPathTile(point);
    }

    private Tile CreateExitTile(Point point)
    {
        return new ExitTile(point);
    }

    private bool CanPlaceMainPathTile(Point currentPosition)
    {
        if (!tileList.ContainsKey(currentPosition))
        {


            return (CanPlaceTile(currentPosition, true));
        }
        else { return false; }
    }


    //Creating SidePath From here
    private void CreateSidePath(int tiles)
    {
        Point nextPosition = keyList[random.Next(0, keyList.Count - 1)];
        Tile nextTile = CanCreateSidePath(nextPosition);

        if (nextTile != null)
        {

            tileList.Add(nextTile.gridPosition, nextTile);

            while (tiles > 0)
            {
                List<Point> possiblePositions = new List<Point>();

                //Look where a Tile can be placed
                Point position = new Point(nextTile.gridPosition.X - 1, nextTile.gridPosition.Y);

                if (!tileList.ContainsKey(position))
                    possiblePositions.Add(position);

                position.X = nextTile.gridPosition.X;
                position.Y = nextTile.gridPosition.Y + 1;

                if (!tileList.ContainsKey(position))
                    possiblePositions.Add(position);

                position.X = nextTile.gridPosition.X + 1;
                position.Y = nextTile.gridPosition.Y;

                if (!tileList.ContainsKey(position))
                    possiblePositions.Add(position);

                position.X = nextTile.gridPosition.X;
                position.Y = nextTile.gridPosition.Y - 1;

                if (!tileList.ContainsKey(position))
                    possiblePositions.Add(position);

                if (possiblePositions.Count > 0)
                {
                    //Choose where to place the Tile
                    nextTile = new SidePathTile(possiblePositions[random.Next(0, possiblePositions.Count - 1)]);
                    tileList.Add(nextTile.gridPosition, nextTile);
                    Console.WriteLine("Side: \nX: " + nextTile.gridPosition.X + "\nY: " + nextTile.gridPosition.Y);
                }
                else
                {
                    break;
                }
                tiles--;
            }

        }
        else
        {

        }


    }

    private Tile CanCreateSidePath(Point nextPosition)
    {
        List<Point> possibleEntrys = new List<Point>();
        Point test = nextPosition;

        test.X = nextPosition.X - 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        test.X = nextPosition.X + 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        test.X = nextPosition.X;
        test.Y = nextPosition.Y - 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        test.Y = nextPosition.Y + 1;
        if (CanPlaceSideEntry(test))
        {
            possibleEntrys.Add(test);
        }
        return (new SidePathEntryTile(possibleEntrys[random.Next(0, possibleEntrys.Count - 1)]));
    }
    private bool CanPlaceSideEntry(Point currentPosition)
    {
        if (!tileList.ContainsKey(currentPosition))
        {

            return CanPlaceTile(currentPosition, false);
        }

        return false;
    }

    //Checking if a tile can be placed
    private bool CanPlaceTile(Point currentPosition, bool IsMain)
    {
        List<Point> possiblePositions = new List<Point>();
        Point position = new Point(currentPosition.X - 1, currentPosition.Y);

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        position.X = currentPosition.X;
        position.Y = currentPosition.Y + 1;

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        position.X = currentPosition.X + 1;
        position.Y = currentPosition.Y;

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        position.X = currentPosition.X;
        position.Y = currentPosition.Y - 1;

        if (!tileList.ContainsKey(position))
            possiblePositions.Add(position);

        return ((IsMain && possiblePositions.Count == 3) || (!IsMain && possiblePositions.Count > 0));
    }
}


