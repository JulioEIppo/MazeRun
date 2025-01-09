using Spectre.Console;

class Program
{
    public static void Main(string[] args)
    {
        var generator = new MazeGenerator();
        Cell[,] maze = generator.MazeGenerate(11,11,5);
        MazeGenerator.PrintMaze(maze);
    }     
}