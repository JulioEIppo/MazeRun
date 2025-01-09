
using System.Drawing;
using Spectre.Console;
public enum State
{
    Normal,
    Paralyzed,
}
class Token
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Speed { get; set; }
    public int Count { get; set; } // amount of turns left with abnormal state
    public int Steps { get; set; } // amount of steps given
    public State State { get; set; }
    public int Sight { get; set; }
    public Skill Skill { get; set; }
    //                                       N  S  E  W
    public int[] DirectionRow = new int[] { -1, 1, 0, 0 };
    public int[] DirectionCol = new int[] { 0, 0, 1, -1 };
    public Token(string name, int x, int y, int speed, Skill skill)
    {
        Name = name;
        X = x;
        Y = y;
        State = State.Normal;
        Speed = speed;
        Skill = skill;
        Count = 0;
        Sight = 3;
    }
    public void MoveToken(int index)
    {
        if (CanMove(Steps))
        {
            X += DirectionRow[index];
            Y += DirectionCol[index];
        }
    }

    public bool CanMove(int count)
    {
        return State == State.Normal && count <= Speed;

    }
    public int GetRow(int index)// row to move token
    {
        return X + DirectionRow[index];
    }
    public int GetCol(int index) // col to move token
    {
        return Y + DirectionCol[index];
    }
    public void PrintTokenMaze(Cell[,] maze, Token token, int gridSize)
    {
        var wallColor = "[blue]■[/]";
        var pathColor = "[black]■[/]";
        int aux = gridSize / 2;
        int startX = Math.Max(0, token.X - aux);
        int startY = Math.Max(0, token.X - aux);
        int endX = Math.Min(maze.GetLength(0) - 1, token.X + aux);
        int endY = Math.Min(maze.GetLength(1) - 1, token.Y + aux);
        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                AnsiConsole.Markup(maze[i, j].Type == Type.Wall ? wallColor : pathColor);
            }
        }
    }


}

