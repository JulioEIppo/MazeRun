using Spectre.Console;


class MazeGenerator
{
    public Random random = new Random();
    public Cell[,] Generate(int height, int width, int numberOfTraps, List<Player> players)
    {
        Cell[,] maze = InitializeMaze(height, width);
        GeneratePaths(maze);
        SetTraps(maze, numberOfTraps);
        SetExitAndSpawnTokens(maze, players);
        return maze;
    }
    private Cell[,] InitializeMaze(int height, int width)
    {
        Cell[,] maze = new Cell[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = new CellObstacle(i, j);
            }
        }
        return maze;
    }
    private void GeneratePaths(Cell[,] maze)
    {
        HashSet<(int, int)> obstacles = new HashSet<(int, int)>();
        int startX = random.Next(1, maze.GetLength(0) - 1);
        int startY = random.Next(1, maze.GetLength(1) - 1);
        maze[startX, startY] = new CellPath(startX, startY);
        obstacles.UnionWith(GetWalls(startX, startY, maze));
        while (obstacles.Count > 0)
        {
            (int, int) obstacle = obstacles.ElementAt(random.Next(obstacles.Count));
            obstacles.Remove(obstacle);
            int x = obstacle.Item1;
            int y = obstacle.Item2;
            int adjacentPaths = CountAdjacentPaths(x, y, maze);
            if (adjacentPaths == 1)
            {
                maze[x, y] = new CellPath(x, y);
                obstacles.UnionWith(GetWalls(x, y, maze));
            }
        }
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            maze[i, 0] = new CellWall(i, 0);
            maze[i, maze.GetLength(1) - 1] = new CellWall(i, maze.GetLength(1) - 1);
        }
        for (int i = 0; i < maze.GetLength(1); i++)
        {
            maze[0, i] = new CellWall(0, i);
            maze[maze.GetLength(0) - 1, i] = new CellWall(maze.GetLength(0) - 1, i);
        }
    }

    private void SetTraps(Cell[,] maze, int numberOfTraps)
    {
        List<(int, int)> pathCells = new List<(int, int)>();
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j] is CellPath)
                {
                    pathCells.Add((i, j));
                }
            }
        }
        for (int i = 0; i < numberOfTraps; i++)
        {
            int index = random.Next(pathCells.Count);
            var (x, y) = pathCells[index];
            maze[x, y] = new CellTrap(x,y);
        }
    }
    // private CellTrap GetRandomTrap(int x, int y)
    // {
    //     int randomIndex = random.Next(0, 3);
    //     TrapType trap;
    //     switch (randomIndex)
    //     {
    //         case 0: { trap = new TrapParalyze(); break; };
    //         case 1: { trap = new TrapTeleport(); break; };
    //         case 2: { trap = new TrapSpeedDown(); break; };
    //         default: { trap = new TrapParalyze(); break; };
    //     }

    //     return new CellTrap(x, y, trap);
    // }
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
        if (x > 0 && maze[x - 1, y] is CellPath)
        {
            count++;
        }
        if (x < maze.GetLength(0) - 1 && maze[x + 1, y] is CellPath)
        {
            count++;
        }
        if (y > 0 && maze[x, y - 1] is CellPath)
        {
            count++;
        }
        if (y < maze.GetLength(1) - 1 && maze[x, y + 1] is CellPath)
        {
            count++;
        }
        return count;
    }
    private void SetExitAndSpawnTokens(Cell[,] maze, List<Player> players)
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);
        int quarter = random.Next(0, 4);
        SetExit(maze, quarter, height, width);
        SpawnTokens(maze, players, quarter, height, width);
    }
    private void SetExit(Cell[,] maze, int quarter, int height, int width)
    {
        bool exitSet = false;
        while (!exitSet)
        {
            int x = random.Next(0, height);
            int y = random.Next(0, width);
            if (maze[x, y] is CellPath && IsInQuarter(x, y, maze, quarter, height, width))
            {
                maze[x, y] = new CellExit(x, y);
                exitSet = true;
            }
        }
    }
    private void SpawnTokens(Cell[,] maze, List<Player> players, int quarter, int height, int width)
    {
        int tokensSpawned = 0;
        while (tokensSpawned != players.Count)
        {
            int x = random.Next(0, height);
            int y = random.Next(0, width);

            if (maze[x, y] is CellPath && !IsInQuarter(x, y, maze, quarter, height, width))
            {
                players[tokensSpawned].Token.X = x;
                players[tokensSpawned].Token.Y = y;
                tokensSpawned++;
            }
        }
    }
    private bool IsInQuarter(int x, int y, Cell[,] maze, int quarter, int height, int width)
    {
        int startRow = (quarter / 2) * height / 2;
        int endRow = (quarter / 2) + height / 2;
        int startCol = (quarter % 2) * width / 2;
        int endCol = (quarter % 2) + width / 2;
        return x >= startRow && x <= endRow && y >= startCol && y <= endCol;
    }


}


