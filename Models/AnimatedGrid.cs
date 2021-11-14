namespace robot.Models;

public class AnimatedGrid : IGrid
{

    private readonly string[] _grid;

    private readonly int rows;
    private readonly int cols;

    public AnimatedGrid(string[] grid)
    {
        if (grid.Length == 0)
            throw new Exception("Empty gird detected!");

        this._grid = grid;

        this.rows = grid.Length - 1;
        this.cols = grid[0].Length - 1;
    }

    public bool checkPosition(Point point) =>
        point.X <= this.rows
            && point.X >= 0
            && point.Y <= this.cols
            && point.Y >= 0;


    public char getCell(Point point)
    {
        if (this.checkPosition(point))
            return this._grid[point.X][point.Y];
        else throw new Exception($"point of range detected: {{{point.X}, {point.Y}}}");
    }

    public void setCell(char value, Point point)
    {
        string row = this._grid[point.X];
        string newRow = row.Remove(point.Y, 1).Insert(point.Y, value.ToString());

        this._grid[point.X] = newRow;

        Thread.Sleep(100);
        Console.Clear();

        foreach (string line in this._grid)
            Console.WriteLine(line);
    }

    public override string ToString()
    {
        return string.Join('\n', this._grid);
    }
}