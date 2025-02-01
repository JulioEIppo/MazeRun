

using Spectre.Console;

public abstract class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    abstract public string Emoji { get; set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public abstract void Print();

}
class CellPath : Cell
{
    public override string Emoji { get; set; }
    public CellPath(int x, int y) : base(x, y)
    {
        Emoji = "🔵"; // blue circle emoji

    }
    public override void Print()
    {
        AnsiConsole.Markup(Emoji);
    }
}
class CellTrap : Cell
{
    public override string Emoji { get; set; }

    public CellTrap(int x, int y) : base(x, y)
    {
        Emoji = "🔴"; // red circle
    }
    public override void Print()
    {
        AnsiConsole.Markup(Emoji);
    }
}
class CellWall : Cell
{
    public override string Emoji { get; set; }
    public CellWall(int x, int y) : base(x, y)
    {
        Emoji = "⛔"; // no entry emoji
    }
    public override void Print()
    {
        AnsiConsole.Markup(Emoji);
    }
}
class CellObstacle : Cell
{
    public override string Emoji { get; set; }
    public CellObstacle(int x, int y) : base(x, y)
    {
        Emoji = "🚧"; // construction sign emoji
    }
    public override void Print()
    {
        AnsiConsole.Markup(Emoji);
    }
}
class CellExit : Cell
{
    public override string Emoji { get; set; }
    public CellExit(int x, int y) : base(x, y)
    {
        Emoji = "🔚"; //finish flag
    }
    public override void Print()
    {
        AnsiConsole.Markup(Emoji);
    }
}