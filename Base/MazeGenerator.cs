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
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        Type = Type.Wall;
    }
    public void SetPath()
    {
        Type = Type.Path;
    }
}
class MazeGenerator
{
    public Random random = new Random();
    public Cell[,] MazeGenerate(int height, int width)
    {
        Cell[,] maze = new Cell[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = new Cell(i, j);
            }
        }
        List<(int, int)> walls = new List<(int, int)>(); //list to store walls
        int startX = random.Next(0, height);
        int startY = random.Next(0, width);
        maze[startX, startY].SetPath();
        walls.AddRange(GetWalls(startX, startY, maze));
        while (walls.Count > 0)
        {
            (int, int) wall = walls[random.Next(walls.Count)];
            walls.Remove(wall);
            int x = wall.Item1;
            int y = wall.Item2;
            int adjacentPaths = CountAdjacentPaths(x, y, maze);
            if (adjacentPaths == 1)
            {
                maze[x, y].SetPath();
                walls.AddRange(GetWalls(x, y, maze));
            }
        }
        return maze;
    }

    private List<(int, int)> GetWalls(int x, int y, Cell[,] maze) //Get adjacent walls
    {
        List<(int, int)> walls = new List<(int, int)>();
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
    private void SetTrap(Cell[,] maze)
    {
        int count = 0;
        while (count < 5)
        {
            int x = random.Next(1, maze.GetLength(0) - 2);
            int y = random.Next(1, maze.GetLength(1) - 2);
            maze[x, y].Type = Type.Trap;
            if (maze[x, y].Type == Type.Path)
            {
                maze[x, y].Type = Type.Trap;
                count++;
            }
        }
    }
    public static void PrintMaze(Cell[,] maze)
    {
        var wallColor = "[blue]■[/]";
        var pathColor = "[black]■[/]";
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                AnsiConsole.Markup(maze[i, j].Type == Type.Wall ? wallColor : pathColor);
            }
            AnsiConsole.WriteLine();
        }
    }
    // public static void PrintPlayerMaze(int turn, int posToken, Cell[,] maze)
    // {

    // }

}