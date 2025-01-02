class Trap
{
    public TrapType Type { get; set; }
    public bool Active { get; set; } 
    public Trap(TrapType type)
    {
        Type = type;
        Active = true;
    }

}
public enum TrapType
{
    Freeze,
    SpeedDowngrade,
    Teleport,
}