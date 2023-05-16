namespace MarsRover;

internal record Coordinates(int X, int Y)
{
    public Coordinates MoveAlongYAxis(int displacement)
    {
        return new Coordinates(X, Y + displacement);
    }

    public Coordinates MoveAlongXAxis(int displacement)
    {
        return new Coordinates(X + displacement, Y);
    }
}