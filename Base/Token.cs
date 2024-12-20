
using System.Drawing;
public enum State
{
    None,
    Paralyzed,
}
class Tokens
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Speed { get; set; }
    public State State { get; set; }
    public Skill Skill { get; set; }
    public Tokens(string name, int x, int y, int speed, Skill skill)
    {
        Name = name;
        X = x;
        Y = y;
        State = State.None;
        Speed = speed;
        Skill = skill;
    }

}

