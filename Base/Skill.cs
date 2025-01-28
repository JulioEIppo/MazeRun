abstract class Skill
{
    public string Name { get; set; }
    public int CoolTime { get; set; } // amount of turns required to reuse skill
    public int Count { get; set; } // amount of turns left to use skill
    public bool IsActive { get; set; }
    public Skill(string name, int coolTime)
    {
        Name = name;
        CoolTime = coolTime;
        Count = 0;
        IsActive = false;
        
    }

    public bool CanUseSkill()
    {
        return Count <= 0;
    }
}


class SkillParalyze : Skill
{
    public SkillParalyze(string name, int coolTime) : base(name, coolTime)
    {

    }
    public void UseSkill(Token token, Cell[,] maze, List<Token> list)
    {
        int random = new Random().Next(1, 6);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == token)
            {
                continue;
            }
            list[i].State = State.Paralyzed;
        }
        Count = CoolTime;
    }
}
class SkillSpeedUpgrade : Skill
{
    public SkillSpeedUpgrade(string name, int coolTime) : base(name, coolTime)
    {

    }
    public void UseSkill(Token token, Cell[,] maze)
    {
        token.Speed += 1;
        Count = CoolTime;
        IsActive = true;
    }
}
class SkillBreakObstacle : Skill
{
    public SkillBreakObstacle(string name, int coolTime) : base(name, coolTime) { }

    public void UseSkill(Token token, Cell[,] maze, int index)
    {
        int row = token.GetRow(index);
        int col = token.GetCol(index);
        maze[row, col] = new CellPath(row, col);
        Count = CoolTime;
    }
}
class SkillTeleport : Skill
{
    public SkillTeleport(string name, int coolTime) : base(name, coolTime)
    {

    }
    public void UseSkill(Token token, Cell[,] maze, int index)
    {
        // int row = token.GetRow(index) + token.GetRow(token.GetRow(index));
        // int col = token.GetCol(index) + token.GetCol(token.GetCol(index));
        // if (maze[row, col] is CellPath) 
        // {
        //     token.X += token.GetRow(token.GetRow(index));
        //     token.Y += token.GetCol(token.GetCol(index));
        // }
        // fix this shit
    }
}
class SkillSight : Skill
{
    public SkillSight(string name, int coolTime) : base(name, coolTime)
    {

    }
    // public void UseSkill(Token token, Cell[,] maze)
    // {
    //     token.PrintTokenMaze(maze, token, token.Sight);
    // }
}