using Spectre.Console;

public class Game
{
    public int Turn { get; set; }
    public List<Player> Players { get; set; }
    public Cell[,] Maze { get; set; }
    public (int, int) Exit { get; set; }

    public bool GameStop { get; set; }
    public Random random = new Random();
    public Game(Cell[,] maze, List<Player> players)
    {
        Maze = maze;
        Players = players;
        GameStop = false;
        SetExit();
    }
    public void Move(Token token, int index)
    {
        if (ValidMove(index) && !GameStop)
        {
            token.MoveToken(index);
            if (Maze[token.X, token.Y] is CellTrap)
            {
                Maze[token.X, token.Y] = new CellPath(token.X, token.Y);
                Console.WriteLine($"{token.Name} ha caido en una trampa");
                int temp = random.Next(0, 3);
                switch (temp)
                {
                    case 0:
                        ApplyTrapSpeedDown(token); break;
                    case 1:
                        ApplyTrapParalyze(token); break;
                    case 2:
                        Teleport(token); break;
                }
            }
            CheckExit();
        }
    }

    public void Play()
    {
        Console.Clear();
        while (!GameStop)
        {
            Console.WriteLine($"Turno de {Players[Turn].Token.Name}");
            Token token = Players[Turn].Token;
            if (token.State == State.Paralyzed)
            {
                Console.WriteLine($"{token.Name} esta paralizado por {token.Count} turnos");
            }
            int steps = 0;
            while (steps < token.Speed)
            {
                PrintMaze();
                if (GameStop)
                {
                    break;
                }
                Console.WriteLine($"Quedan {token.Speed - steps} pasos restantes");
                Console.WriteLine("Muevase con W, A, S, D.");
                Console.WriteLine("Para usar la habilidad presione H");
                Console.WriteLine("Presione enter para terminar el turno");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        Console.Clear();
                        Move(token, 0);
                        steps++;
                        break;
                    case ConsoleKey.S:
                        Console.Clear();
                        Move(token, 1);
                        steps++;
                        break;
                    case ConsoleKey.D:
                        Console.Clear();
                        Move(token, 2);
                        steps++;
                        break;
                    case ConsoleKey.A:
                        Console.Clear();
                        Move(token, 3);
                        steps++;
                        break;
                    case ConsoleKey.H:
                        Console.Clear();
                        UseSkill(token);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        steps = token.Speed;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Direccion invalida, muevase con W, A, S, D");
                        break;
                }
            }
            if (!GameStop)
            {
                ChangeTurn();
            }
        }
        Console.WriteLine($"El ganador ha sido {Players[Turn].Token.Name} {Players[Turn].Token.Icon}");
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
    public void UseSkill(Token token)
    {
        if (!token.CanUseSkill())
        {
            Console.WriteLine($"No es posible usar la habilidad. Faltan {token.Skill.Count} turnos");
            return;
        }
        switch (token.Skill.Name)
        {
            case "BreakObstacle":
                PrintMaze();
                Console.WriteLine("Selecciona la direccion en la que quieres romper la pared");
                var direction = Console.ReadKey();
                BreakObstacle(token, direction);
                Console.Clear();
                break;
            case "Paralyze":
                Paralyze(token);
                break;
            case "Teleport":
                Teleport(token);
                break;
            case "ExtraStep":
                ExtraStep(token);
                break;
            case "Swap":
                Swap(token);
                break;
            default:
                Console.WriteLine("La habilidad ha fallado :("); break;
        }
    }
    public void Swap(Token token)
    {
        int player = random.Next(0, Players.Count);
        while (player == Turn)
        {
            player = random.Next(0, Players.Count);
        }
        (Players[Turn].Token.X, Players[player].Token.X) = (Players[player].Token.X, Players[Turn].Token.X);
        (Players[Turn].Token.Y, Players[player].Token.Y) = (Players[player].Token.Y, Players[Turn].Token.Y);
        token.SetSkillCount();
        Console.WriteLine($"{Players[Turn].Token.Name} y {Players[player].Token.Name} han intercambiado posiciones");
    }
    public void ExtraStep(Token token)
    {
        token.Speed += 1;
        token.SetSkillCount();

    }
    public Cell GetCell(Token token, int index)
    {
        return Maze[token.GetRow(index), token.GetCol(index)];
    }
    public void BreakObstacle(Token token, ConsoleKeyInfo direction)
    {
        switch (direction.Key)
        {
            case ConsoleKey.W:
                if (GetCell(token, 0) is CellObstacle)
                {
                    Maze[token.GetRow(0), token.GetCol(0)] = new CellPath(token.GetRow(0), token.GetCol(0));
                    token.SetSkillCount();
                }
                break;
            case ConsoleKey.S:
                if (GetCell(token, 1) is CellObstacle)
                {
                    Maze[token.GetRow(1), token.GetCol(1)] = new CellPath(token.GetRow(1), token.GetCol(1));
                }
                token.SetSkillCount();
                break;
            case ConsoleKey.D:
                if (GetCell(token, 2) is CellObstacle)
                {
                    Maze[token.GetRow(2), token.GetCol(2)] = new CellPath(token.GetRow(2), token.GetCol(2));
                }
                token.SetSkillCount();
                break;
            case ConsoleKey.A:
                if (GetCell(token, 3) is CellObstacle)
                {
                    Maze[token.GetRow(3), token.GetCol(3)] = new CellPath(token.GetRow(3), token.GetCol(3));
                }
                token.SetSkillCount();
                break;
            default:
                Console.WriteLine("La habilidad ha fallado"); break;
        }
    }
    public void Teleport(Token token)
    {
        int row = random.Next(1, Maze.GetLength(0) - 1);
        int col = random.Next(1, Maze.GetLength(1) - 1);
        while (!(Maze[row, col] is CellPath))
        {
            row = random.Next(1, Maze.GetLength(0) - 1);
            col = random.Next(1, Maze.GetLength(1) - 1);
        }
        token.X = row;
        token.Y = col;
        token.SetSkillCount();
        Console.WriteLine($"{token.Name} se ha teletransportado");
    }

    public void ApplyTrapSpeedDown(Token token)
    {
        token.Speed -= 1;
        token.TrapTurns = 3;
        Console.WriteLine($"La velocidad de {token.Name} ahora es {token.Speed}");
    }

    public void ApplyTrapParalyze(Token token)
    {
        token.State = State.Paralyzed;
        token.Count = 2;
        Console.WriteLine($"{token.Name} ahora esta paralizado por {token.Count} turnos");
    }
    public void CheckExit()
    {

        if (Players[Turn].Token.X == Exit.Item1 && Players[Turn].Token.Y == Exit.Item2)
        {
            GameStop = true;
        }

    }
    public void Paralyze(Token token)
    {
        int player = random.Next(0, Players.Count);
        while (player == Turn)
        {
            player = random.Next(0, Players.Count);
        }
        Players[player].Paralyze(2);
        token.SetSkillCount();
        Console.WriteLine($"{Players[player].Token.Name} esta paralizado");
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