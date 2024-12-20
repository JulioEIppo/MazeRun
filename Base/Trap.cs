class Trap
{
    public TrapType Type { get; set; }
    public Trap(TrapType type)
    {
        Type = type;
    }

}
public enum TrapType
{
    Type1,
    Type2,
    Type3,
}