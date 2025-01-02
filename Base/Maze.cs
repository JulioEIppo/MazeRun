using System.Drawing;


class Maze
{
    public int Row { get; set; }
    public int Column { get; set; }
    public List<Player> Players { get; set; }
    public Maze(char[,] charsMaze, List<Player> players)
    {
        Row = charsMaze.GetLength(0);
        Column = charsMaze.GetLength(1);
        Players = players;
    }

}
