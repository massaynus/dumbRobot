using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace robot
{
    public class Robot
    {
        private char _direction;
        private Point _position;
        private bool _debugMode = false;

        private readonly string[] _gird;
        private readonly char _obstacle;
        private readonly char _visited;
        private readonly char _clean;
        private readonly char _dirty;

        private readonly int rows;
        private readonly int cols;

        private readonly HashSet<char> _allowedDirection = new HashSet<char>() { 'N', 'S', 'E', 'W' };
        private readonly HashSet<char> _invalidCells = new HashSet<char>();
        private readonly Dictionary<char, char> _nextDirection = new Dictionary<char, char>()
        {
            {'N', 'E'},
            {'E', 'S'},
            {'S', 'W'},
            {'W', 'N'},
        };

        public string[] Grid { get => this._gird; }

        public Robot(string[] grid, bool debugMode = false) : this(grid, 'X', '■', ' ', '.', debugMode)
        {
        }

        public Robot(string[] grid, char obstacle, char visited, char clean, char dirty, bool debugMode)
        {
            this._debugMode = debugMode;
            this._position = new Point(0, 0);
            this._direction = 'E';
            this._gird = grid;

            this._obstacle = obstacle;
            this._visited = visited;
            this._clean = clean;
            this._dirty = dirty;

            this._invalidCells.Add(this._obstacle);
            this._invalidCells.Add(this._visited);

            this.rows = grid.Length - 1;
            this.cols = grid[0].Length - 1;
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

        private bool checkPosition() => this.checkPosition(this._position);
        private bool checkPosition(Point point) =>
            point.X <= this.rows
                && point.X >= 0
                && point.Y <= this.cols
                && point.Y >= 0;

        private char getCurrentCell() => this.getCellAt(this._position);

        private char getCellAt(Point point)
        {
            if (this.checkPosition(point))
                return this._gird[point.X][point.Y];
            else throw new Exception($"point of range detected: {{{point.X}, {point.Y}}}");
        }

        private void setCurrentCell(char value)
        {
            string row = this._gird[this._position.X];
            string newRow = row.Remove(this._position.Y, 1).Insert(this._position.Y, value.ToString());

            this._gird[this._position.X] = newRow;

            if (this._debugMode)
            {
                Thread.Sleep(100);
                Console.Clear();
                foreach (string line in this._gird)
                    Console.WriteLine(line);
            }
        }

        private bool isValidDirection()
        {
            Point nextPoint = this.getNextPosition();

            return this.checkPosition(nextPoint)
                && !this._invalidCells.Contains(this.getCellAt(nextPoint));
        }

        public int Traverse()
        {
            int cleanedCells = 0;
            while (true)
            {
                char currentCell = this.getCurrentCell();
                this.setCurrentCell(this._visited);

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
}
