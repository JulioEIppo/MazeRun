using System.Drawing;

public enum MazeObjects
{
    Path,
    Wall,
    Trap,
    Obstacle,
}
class Maze
{
    public int Row { get; set; }
    public int Column { get; set; }
    public MazeObjects[,] Labyrinth { get; set; }
   
    public Maze(char[,] charsMaze)
    { 
        Row = charsMaze.GetLength(0);
        Column = charsMaze.GetLength(1);
        Labyrinth = MazeGenerate(charsMaze);
    }
    MazeObjects[,] MazeGenerate(char[,] charsMaze)
    {
        MazeObjects[,] maze = new MazeObjects[charsMaze.GetLength(0), charsMaze.GetLength(1)];
        for (int i = 0; i < charsMaze.GetLength(0); i++)
        {
            for (int j = 0; j < charsMaze.GetLength(1); j++)
            {
                if (charsMaze[i, j] == '#')
                {
                    maze[i, j] = MazeObjects.Wall;
                }
                else
                {
                    maze[i, j] = MazeObjects.Path;
                }
            }
        }
        return maze;
    }
    // public char[,] CharsMaze(int row, int column)
    // {
    //     char[,] charsMaze = new char[row, column];
    //     for (int i = 0; i < row; i++)
    //     {
    //         for (int j = 0; j < column; j++)
    //         {
    //             charsMaze[i, j] = '#';
    //         }
    //     }



}
