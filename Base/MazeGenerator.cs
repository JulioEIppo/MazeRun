using Spectre.Console;

public enum Type
{
    Path,
    Wall,
    Obstacle,
    Trap,
}

class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public Type Type { get; set; }
    public TrapType TrapType { get; set; }
    public bool BeingUsed { get; set; }
    public bool Exit { get; set; }
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        Type = Type.Wall;
        TrapType = new TrapNone();
        BeingUsed = false;
        Exit = false;
    }
    public void SetPath()
    {
        Type = Type.Path;
    }
    public void SetTrap(TrapType trapType)
    {
        Type = Type.Trap;
        TrapType = trapType;
    }
}
class MazeGenerator
{
    public Random random = new Random();
    public Cell[,] MazeGenerate(int height, int width, int numberOfTraps, List<Token> tokens)
    {
        Cell[,] maze = InitializeMaze(height, width);
        GeneratePaths(maze);
        SetTraps(maze, numberOfTraps);
        SetExitAndSpawnTokens(maze, tokens);
        return maze;
    }
    public Cell[,] InitializeMaze(int height, int width)
    {
        Cell[,] maze = new Cell[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = new Cell(i, j);
            }
        }
        return maze;
    }
    private void GeneratePaths(Cell[,] maze)
    {
        HashSet<(int, int)> walls = new HashSet<(int, int)>(); //list to store walls
        int startX = random.Next(0, maze.GetLength(0));
        int startY = random.Next(0, maze.GetLength(1));
        maze[startX, startY].SetPath();
        walls.UnionWith(GetWalls(startX, startY, maze));
        while (walls.Count > 0)
        {
            (int, int) wall = walls.ElementAt(random.Next(walls.Count));
            walls.Remove(wall);
            int x = wall.Item1;
            int y = wall.Item2;
            int adjacentPaths = CountAdjacentPaths(x, y, maze);
            if (adjacentPaths == 1)
            {
                maze[x, y].SetPath();
                walls.UnionWith(GetWalls(x, y, maze));
            }
        }
    }

    private void SetTraps(Cell[,] maze, int numberOfTraps)
    {
        List<(int, int)> pathCells = new List<(int, int)>();
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j].Type == Type.Path)
                {
                    pathCells.Add((i, j));
                }
            }
        }
        for (int i = 0; i < numberOfTraps; i++)
        {
            int index = random.Next(pathCells.Count);
            var (x, y) = pathCells[index];
            maze[x, y].TrapType = GetRandomTrap();
        }
    }
    private TrapType GetRandomTrap()
    {
        int randomIndex = random.Next(0, 3);
        switch (randomIndex)
        {
            case 0: return new TrapParalyze();
            case 1: return new TrapTeleport();
            case 2: return new TrapSpeedDown();
            default: return new TrapNone();
        }
    }
    private HashSet<(int, int)> GetWalls(int x, int y, Cell[,] maze) //Get adjacent walls
    {
        HashSet<(int, int)> walls = new HashSet<(int, int)>();
        if (x > 1)
        {
            walls.Add((x - 1, y)); //Up
        }
        if (x < maze.GetLength(0) - 2)
        {
            walls.Add((x + 1, y));//Down
        }
        if (y > 1)
        {
            walls.Add((x, y - 1)); //Left
        }
        if (y < maze.GetLength(1) - 2)
        {
            walls.Add((x, y + 1));//Right
        }
        return walls;
    }
    private int CountAdjacentPaths(int x, int y, Cell[,] maze)
    {
        int count = 0;
        if (x > 0 && maze[x - 1, y].Type == Type.Path)
        {
            count++;
        }
        if (x < maze.GetLength(0) - 1 && maze[x + 1, y].Type == Type.Path)
        {
            count++;
        }
        if (y > 0 && maze[x, y - 1].Type == Type.Path)
        {
            count++;
        }
        if (y < maze.GetLength(1) - 1 && maze[x, y + 1].Type == Type.Path)
        {
            count++;
        }
        return count;
    }
    public void SetExitAndSpawnTokens(Cell[,] maze, List<Token> tokens)
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);
        int quarter = random.Next(0, 4);
        SetExit(maze, quarter, height, width);
        SpawnTokens(maze, tokens, quarter, height, width);
    }
    public void SetExit(Cell[,] maze, int quarter, int height, int width)
    {
        bool exitSet = false;
        while (!exitSet)
        {
            int x = random.Next(0, height);
            int y = random.Next(0, width);
            if (maze[x, y].Type == Type.Path && IsInQuarter(x, y, maze, quarter, height, width))
            {
                maze[x, y].Exit = true;
                exitSet = true;
            }
        }
    }
    public void SpawnTokens(Cell[,] maze, List<Token> tokens, int quarter, int height, int width)
    {
        int tokensSpawned = 0;
        while (tokensSpawned != tokens.Count)
        {
            int x = random.Next(0, height);
            int y = random.Next(0, width);
            
            if (maze[x,y].Type == Type.Path && !IsInQuarter(x,y,maze,quarter,height,width) )
            {
                tokens[tokensSpawned].X = x;
                tokens[tokensSpawned].Y = y;
                tokensSpawned++;
            }
        }
    }
    public bool IsInQuarter(int x, int y, Cell[,] maze, int quarter, int height, int width)
    {
        int startRow = (quarter / 2) * height / 2;
        int endRow = (quarter / 2) + height / 2;
        int startCol = (quarter % 2) * width / 2;
        int endCol = (quarter % 2) + width / 2;
        return x >= startRow && x <= endRow && y >= startCol && y <= endCol;
    }

    public static void PrintMaze(Cell[,] maze)
    {
        var wallColor = "[blue]■[/]";
        var pathColor = "[black]■[/]";
        var exitColor = "[yellow]■[/]";
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                AnsiConsole.Markup(maze[i, j].Type == Type.Wall ? wallColor : pathColor);
            }
            AnsiConsole.WriteLine();
        }
    }
}


