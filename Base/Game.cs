class Game
{
    public int Turn { get; set; }
    public List<Player> Players { get; set; }
    public Cell[,] Maze { get; set; }
    public List<Token> TokensInGame { get; set; }

    public Game(Cell[,] maze)
    {
        Players = new List<Player>();
        Maze = maze;
        Turn = 0;
        TokensInGame = new List<Token>();
    }
    public void Move(int turn, int posToken, int index)
    {
        if (ValidMove(turn, posToken, index))
        {
            Players[turn].Tokens[posToken].MoveToken(index);
        }
    }


    public bool ValidMove(int turn, int posToken, int index)
    {
        Token token = Players[turn].Tokens[posToken];
        int row = token.GetRow(index);
        int col = token.GetCol(index);
        return !(Maze[row, col].Type == Type.Wall || Maze[row, col].Type == Type.Obstacle);
    }


    public void ChangeTurn()
    {
        Turn++;
        if (Turn == Players.Count)
        {
            Turn = 0;
        }
    }
    public void Update()
    {
        for (int i = 0; i < Players[Turn].Tokens.Count; i++)
        {
            if (Players[Turn].Tokens[i].State != State.Normal && Players[Turn].Tokens[i].Count > 0)
            {
                Players[Turn].Tokens[i].Count -= 1;
                if (Players[Turn].Tokens[i].Count == 0)
                {
                    Players[Turn].Tokens[i].State = State.Normal;
                }
            }
        }
    }
    public bool CanUseSkill(int posToken)
    {
        return Players[Turn].Tokens[posToken].Skill.Count == 0;
    }
    //     public void Play(int posToken)
    // {

    // }
}