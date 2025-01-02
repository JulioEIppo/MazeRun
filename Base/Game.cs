class Game
{
    public int Turn { get; set; }
    public List<Player> Players { get; set; }
    public Cell[,] Maze { get; set; }

    public Game(Cell[,] maze)
    {
        Players = new List<Player>();
        Maze = maze;
        Turn = 0;
    }
    public void Move(int turn,int posToken,int index)
    {
        if (ValidMove(turn,posToken, index))
        {
            Players[turn].Tokens[posToken].MoveToken(index);
        }
    }
    public bool ValidMove(int turn, int posToken, int index)
    {
        int row = Players[turn].Tokens[posToken].X + Players[turn].Tokens[posToken].DirectionRow[index];
        int col = Players[turn].Tokens[posToken].Y + Players[turn].Tokens[posToken].DirectionRow[index];
        if (Maze[row, col].Type == Type.Wall || Maze[row, col].Type == Type.Obstacle)
        {
            return false;
        }
        return true;
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
    public void UseSkill(int posToken, int index)
    {
        SkillType skill = Players[Turn].Tokens[posToken].Skill.Type;
        switch (skill)
        {
            case SkillType.BreakObstacle:
                int row = Players[Turn].Tokens[posToken].DirectionRow[index];
                int col = Players[Turn].Tokens[posToken].DirectionCol[index];
                Maze[row, col].Type = Type.Path;
                Players[Turn].Tokens[posToken].Skill.Count = Players[Turn].Tokens[posToken].Skill.CoolTime;
                break;
            case SkillType.Paralyze:
                int random = new Random().Next(1, 6);
                for (int i = 0; i < Players.Count; i++)
                {
                    if (i == Turn)
                    {
                        continue;
                    }
                    for (int j = 0; j < Players[i].Tokens.Count; j++)
                    {
                        Players[i].Tokens[random].State = State.Paralyzed;
                        Players[i].Tokens[random].Count = 1;
                    }
                }
                Players[Turn].Tokens[posToken].Skill.Count = Players[Turn].Tokens[posToken].Skill.CoolTime;
                break;
            case SkillType.SpeedUpgrade:
                Players[Turn].Tokens[posToken].Speed += 1;
                Players[Turn].Tokens[posToken].Skill.Count = Players[Turn].Tokens[posToken].Skill.CoolTime;
                break;
            case SkillType.SpeedDowngrade:
                Players[Turn].Tokens[posToken].Speed -= 1;
                Players[Turn].Tokens[posToken].Skill.Count = Players[Turn].Tokens[posToken].Skill.CoolTime;
                break;
            case SkillType.Teleport:
                int randomRow = new Random().Next(1, Maze.GetLength(0) - 1);
                int randomCol = new Random().Next(1, Maze.GetLength(1) - 1);
                Players[Turn].Tokens[posToken].X = randomRow;
                Players[Turn].Tokens[posToken].Y = randomCol;
                break;


        }
    }
    public bool CanUseSkill(int posToken)
    {
        if (Players[Turn].Tokens[posToken].Skill.Count == 0)
        {
            return true;
        }
        return false;
    }
    //     public void Play(int posToken)
    // {

    // }
}