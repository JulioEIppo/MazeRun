
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

    public State State { get; set; }
    public int Sight { get; set; }
    public string Icon { get; set; }
    public Skill Skill { get; set; }
    //                                       N  S  E  W
    public int[] DirectionRow = new int[] { -1, 1, 0, 0 };
    public int[] DirectionCol = new int[] { 0, 0, 1, -1 };
    public Token(string name, int speed, Skill skill, string icon)
    {
        Name = name;
        State = State.Normal;
        Speed = speed;
        Skill = skill;
        Count = 0;
        Sight = 3;
        Icon = icon;
    }

    // public void SetPosition(int x, int y)
    // {
    //     X = x;
    //     Y = y;
    // }

    public void MoveToken(int index)
    {
        if (State == State.Normal)
        {
            X += DirectionRow[index];
            Y += DirectionCol[index];
        }
    }
    public int GetRow(int index)// row to move token
    {
        return X + DirectionRow[index];
    }
    public int GetCol(int index) // col to move token
    {
        return Y + DirectionCol[index];
    }

    public void Print()
    {
        AnsiConsole.Markup(Icon);
    }
    public void PrintTokenMaze(Cell[,] maze, int gridSize)
    {
        int aux = gridSize / 2;
        int startX = Math.Max(0, X - aux);
        int startY = Math.Max(0, Y - aux);
        int endX = Math.Min(maze.GetLength(0) - 1, X + aux);
        int endY = Math.Min(maze.GetLength(1) - 1, Y + aux);
        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                if (i == X && j == Y)
                {
                    Print();
                    continue;
                }
                maze[i, j].Print();
            }
        }

    }

    public void Update()
    {
        if (State != State.Normal)
        {
            Count -= 1;
            if (Count == 0)
            {
                State = State.Normal;
            }
        }
    }

    public bool CanUseSkill()
    {
        return this.Skill.CanUseSkill();
    }

    public void ApplyTrap(TrapSpeedDown trap)
    {
        if (!trap.IsActive) return;
        this.Speed -= 1;
    }

    public void ApplyTrap(TrapParalyze trap)
    {
        if (!trap.IsActive) return;
        this.State = State.Paralyzed;
    }


    public void ApplyTrap(TrapTeleport trap, int x, int y)
    {
        if (!trap.IsActive) return;
        this.X = x;
        this.Y = y;
    }

}

