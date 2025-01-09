abstract class TrapType
{
    public bool IsActive { get; set; }
    public TrapType()
    {
        IsActive = false;
    }
}
class TrapNone : TrapType
{
    public TrapNone()
    {

    }
}
class TrapParalyze : TrapType
{
    public TrapParalyze()
    {

    }
    public void ApplyTrap(Token token)
    {
        token.State = State.Paralyzed;
        IsActive = true;
    }
}
class TrapSpeedDown : TrapType
{
    public TrapSpeedDown()
    {

    }
    public void ApplyTrap(Token token)
    {

        IsActive = true;
    }
}
class TrapTeleport : TrapType
{
    public TrapTeleport()
    {

    }
    public void ApplyTrap(Token token, Cell[,] maze)
    {
        int randomX = new Random().Next(1, maze.GetLength(0) - 2);
        int randomY = new Random().Next(1, maze.GetLength(1) - 2);
        token.X = randomX;
        token.Y = randomY;
        IsActive = true;
    }

}