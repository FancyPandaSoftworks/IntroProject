/// <summary>
/// The tilegrid, a grid which holds all the tiles
/// </summary>
public class TileGrid : GameObjectGrid
{
    /// <summary>
    /// Creating the tileGrid
    /// </summary>
    /// <param name="rows">The amount of rows</param>
    /// <param name="columns">The amount of colums</param>
    /// <param name="id">The id used to find this object</param>
    public TileGrid(int rows, int columns, string id = "") : base(rows, columns, id)
    {
    }
}
