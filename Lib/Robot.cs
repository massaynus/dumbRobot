namespace robot.Library;
public class Robot
{
    private char _direction;
    private Point _position;

    private readonly IGrid _grid;
    private readonly char _obstacle;
    private readonly char _visited;
    private readonly char _clean;
    private readonly char _dirty;

    private readonly HashSet<char> _allowedDirection = new HashSet<char>() { 'N', 'S', 'E', 'W' };
    private readonly HashSet<char> _invalidCells = new HashSet<char>();
    private readonly Dictionary<char, char> _nextDirection = new Dictionary<char, char>()
        {
            {'N', 'E'},
            {'E', 'S'},
            {'S', 'W'},
            {'W', 'N'},
        };

    public IGrid Grid { get => this._grid; }

    public Robot(string[] grid, bool debugMode = false) : this(grid, 'X', '■', ' ', '.', debugMode)
    {
    }

    public Robot(string[] grid, char obstacle, char visited, char clean, char dirty, bool debugMode)
    {
        this._position = new Point(0, 0);
        this._direction = 'E';

        if (debugMode)
            this._grid = new AnimatedGrid(grid);
        else
            this._grid = new DictionaryGrid(grid);

        this._obstacle = obstacle;
        this._visited = visited;
        this._clean = clean;
        this._dirty = dirty;

        this._invalidCells.Add(this._obstacle);
        this._invalidCells.Add(this._visited);

    }

    private Point getNextPosition()
    {

        if (!this._allowedDirection.Contains(this._direction))
            throw new Exception($"Invalid direction detected: {this._direction}");

        Point nextPoint = new Point(this._position.X, this._position.Y);

        switch (this._direction)
        {
            case 'N':
                nextPoint.X -= 1;
                break;
            case 'S':
                nextPoint.X += 1;
                break;
            case 'E':
                nextPoint.Y += 1;
                break;
            case 'W':
                nextPoint.Y -= 1;
                break;
        }

        return nextPoint;
    }

    private void rotate()
    {
        this._direction = _nextDirection[this._direction];
    }

    private bool isValidDirection()
    {
        Point nextPoint = this.getNextPosition();

        return this._grid.checkPosition(nextPoint)
            && !this._invalidCells.Contains(this._grid.getCell(nextPoint));
    }

    public int Traverse()
    {
        int cleanedCells = 0;
        while (true)
        {
            char currentCell = this._grid.getCell(this._position);
            this._grid.setCell(this._visited, this._position);

            int tries = 4;
            while (tries-- > 0 && !this.isValidDirection())
                this.rotate();

            if (currentCell == this._dirty)
                cleanedCells++;

            if (!this.isValidDirection())
                break;

            this._position = this.getNextPosition();
        }

        return cleanedCells;
    }
}