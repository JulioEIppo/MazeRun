using Spectre.Console;

class Program
{
    public static void Main(string[] args)
    {
        var generator = new MazeGenerator();
        Cell[,] maze = generator.MazeGenerate(10,10);
        MazeGenerator.PrintMaze(maze);
    }     
}