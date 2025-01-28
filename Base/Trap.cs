abstract class TrapType
{
    public bool IsActive { get; set; }
    public TrapType()
    {
        IsActive = false;
    }

}

class TrapParalyze : TrapType
{
    // public TrapParalyze()
    // {

    // }
    // public override void ApplyTrap(Token token)
    // {
    //     token.State = State.Paralyzed;
    //     IsActive = true;
    // }


}
class TrapSpeedDown : TrapType
{
    // public TrapSpeedDown()
    // {

    // }
    // public override void ApplyTrap(Token token)
    // {

    //     IsActive = true;
    // }
}
class TrapTeleport : TrapType
{
}