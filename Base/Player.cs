class Player
{
    public List<Token> Tokens { get; set; }
    public int Rounds { get; set; } //amount of rounds won
    public Player(List<Token> tokens)
    {
        Tokens = tokens;
        Rounds = 0;
    }
}