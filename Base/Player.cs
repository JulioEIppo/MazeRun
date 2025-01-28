using Spectre.Console;

class Player
{
    public Token Token { get; set; }
    public int Rounds { get; set; } //amount of rounds won
    public Player(Token token)
    {
        Token = token;
        Rounds = 0;
    }
    
}