using Spectre.Console;

public class Player
{
    public Token Token { get; set; }
    public Player(Token token)
    {
        Token = token;
    }
    public void Paralyze(int count)
    {
        Token.State = State.Paralyzed;
        Token.Count = count;
        Console.WriteLine($"{Token.Name} ahora esta paralizado por {Token.Count} turnos");
    }

}