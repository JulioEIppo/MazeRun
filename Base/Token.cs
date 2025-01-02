
using System.Drawing;
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
    public Skill Skill { get; set; }
    //                                              N  S  E  W
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
        if (State == State.Normal && count <= Speed)
        {
            return true;
        }
        return false;
    }
    


}

