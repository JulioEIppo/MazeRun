class Skill
{
    public string Name { get; set; }
    public SkillType Type { get; set; }
    public int CoolTime { get; set; } // amount of turns required to reuse skill
    public int Count { get; set; } // amount of turns left to use skill
    public Skill(string name, int coolTime, SkillType skillType)
    {
        Name = name;
        CoolTime = coolTime;
        Count = 0;
        Type = skillType;
    }
    
}
public enum SkillType
{
    BreakObstacle,
    Paralyze,
    SpeedUpgrade,
    SpeedDowngrade,
    Teleport,
}
