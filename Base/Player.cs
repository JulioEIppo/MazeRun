using Spectre.Console;

public class Player
{
    public Token Token { get; set; }
    // public int Rounds { get; set; } //amount of rounds won
    public Player(Token token)
    {
        Token = token;
        // Rounds = 0;
    }
    public void Paralyze(int count)
    {
        Token.State = State.Paralyzed;
        Token.Count = count;
        Console.WriteLine($"{Token.Name} ahora esta paralizado por {Token.Count} turnos");
    }

}