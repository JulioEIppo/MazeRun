
using System.Drawing;
using Spectre.Console;
public enum State
{
    Normal,
    Paralyzed,
}
public class Token
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Speed { get; set; }
    public int OriginalSpeed { get; set; }
    public int Count { get; set; } // amount of turns left with abnormal state
    public int TrapTurns { get; set; }

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
        OriginalSpeed = Speed;
        Skill = skill;
        Count = 0;
        TrapTurns = 0;
        Sight = 3;
        Icon = icon;
    }
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
        if (TrapTurns != 0)
        {
            TrapTurns--;
            if (TrapTurns == 0)
            {
                Speed++;
            }
        }
        if (TrapTurns == 0 &&Speed != OriginalSpeed)
        {
            Speed--;
        }
        Skill.Count--;
    }

    public bool CanUseSkill()
    {
        return Skill.CanUseSkill();
    }
    public void SetSkillCount()
    {
        Skill.Count = Skill.CoolTime;
    }
}

