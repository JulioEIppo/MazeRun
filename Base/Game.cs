using Spectre.Console;

class Game
{
    public int Turn { get; set; }
    public List<Player> Players { get; set; }
    public Cell[,] Maze { get; set; }
    public int Round { get; set; }
    public (int, int) Exit { get; set; }

    public bool GameStop { get; set; }
    public Game(Cell[,] maze, List<Player> players)
    {
        Maze = maze;
        Players = players;
        Round = 0;
        GameStop = false;
        SetExit();
    }

    public void InitializeRound(Cell[,] maze, List<Player> players)
    {

        Players = players;
        Maze = maze;
        Turn = 0;
        Round++;
    }


    public void Move(Token token, int index)
    {
        if (ValidMove(index) && !GameStop)
        {
            token.MoveToken(index);
            CheckExit();
        }
    }

    public void Play()
    {
        while (!GameStop)
        {
            PrintMaze();
            Console.WriteLine($"Turno del player {Turn}");
            Token token = Players[Turn].Token;
            int steps = 0;
            while (steps < token.Speed)
            {
                if (GameStop)
                {
                    break;
                }
                Console.WriteLine($"Quedan {token.Speed - steps} pasos restantes");
                Console.WriteLine("Muevase con W, A, S, D");
                Console.WriteLine("Presione enter para terminar el turno");
                string direction = Console.ReadLine()!;
                switch (direction)
                {
                    case "w":
                        Move(token, 0);
                        steps++;
                        PrintMaze();
                        break;
                    case "s":
                        Move(token, 1);
                        steps++;
                        PrintMaze();
                        break;
                    case "d":
                        Move(token, 2);
                        steps++;
                        PrintMaze();
                        break;
                    case "a":
                        Move(token, 3);
                        steps++;
                        PrintMaze();
                        break;
                    case "h":
                        // skill here 
                        PrintMaze();
                        break;
                    case "":
                        steps = token.Speed;
                        // ChangeTurn();
                        break;
                    default:
                        Console.WriteLine("Direccion invalida, muevase con W, A, S, D");
                        break;
                }
            }
            if (!GameStop)
            {
                ChangeTurn();
            }
        }
        Console.WriteLine($"Ha ganado el jugador {Turn}");
    }
    public void SetExit()
    {
        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            for (int j = 0; j < Maze.GetLength(1); j++)
            {
                if (Maze[i, j] is CellExit)
                {
                    Exit = (i, j);
                }
            }
        }
    }
    public void CheckExit()
    {

        if (Players[Turn].Token.X == Exit.Item1 && Players[Turn].Token.Y == Exit.Item2)
        {
            GameStop = true;
            // Players[Turn].Rounds++;
        }

    }
    public void SkillParalyze(Token token)
    {
        int random = new Random().Next(1, 6);
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].Token == token)
            {
                continue;
            }
            Players[i].Token.State = State.Paralyzed;
        }
        token.Skill.Count = token.Skill.CoolTime;

    }

    public bool ValidMove(int index)
    {
        Token token = Players[Turn].Token;
        int row = token.GetRow(index);
        int col = token.GetCol(index);
        return !(Maze[row, col] is CellObstacle || Maze[row, col] is CellWall);
    }


    public void ChangeTurn()
    {
        Players[Turn].Token.Update();
        Turn++;
        if (Turn == Players.Count)
        {
            Turn = 0;
        }
    }

    public bool CanUseSkill()
    {
        return Players[Turn].Token.CanUseSkill();
    }

    public void PrintMaze()
    {
        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            for (int j = 0; j < Maze.GetLength(1); j++)
            {

                if (i == Players[Turn].Token.X && j == Players[Turn].Token.Y)
                {
                    Players[Turn].Token.Print();
                    continue;
                }
                Maze[i, j].Print();

            }
            AnsiConsole.WriteLine();
        }
    }
}