public class Skill
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