namespace robot.Models;

public interface IGrid
{
    public bool checkPosition(Point point);

    public char getCell(Point point);

    public void setCell(char value, Point point);
}