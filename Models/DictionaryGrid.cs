namespace robot.Models;

public class DictionaryGrid : IGrid
{

    private readonly Dictionary<Point, char> _grid = new();

    private readonly int rows;
    private readonly int cols;

    public DictionaryGrid(string[] grid)
    {
        if (grid.Length == 0)
            throw new Exception("Empty gird detected!");

        this.rows = grid.Length - 1;
        for (int x = 0; x < grid.Length; x++)
        {
            string row = grid[x];
            if (x == 0) this.cols = row.Length - 1;

            for (int y = 0; y < row.Length; y++)
                _grid.Add(new(x, y), row[y]);
        }
    }

    public bool checkPosition(Point point) =>
        point.X <= this.rows
            && point.X >= 0
            && point.Y <= this.cols
            && point.Y >= 0;


    public char getCell(Point point)
    {
        if (this.checkPosition(point))
            return this._grid[point];
        else throw new Exception($"point of range detected: {{{point.X}, {point.Y}}}");
    }

    public void setCell(char value, Point point)
    {
        this._grid[point] = value;
    }

    public override string ToString()
    {
        StringBuilder builder = new();

        for (int x = 0; x < this.rows; x++)
        {
            for (int y = 0; y < this.cols; y++)
                builder.Append(this._grid[new(x, y)]);

            builder.AppendLine();

        }

        return builder.ToString();
    }
}