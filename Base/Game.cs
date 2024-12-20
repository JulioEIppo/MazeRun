public enum Directions
{
    Up,
    Down,
    Left,
    Right,
}
class Game
{
    public int Turn { get; set; }
    public List<List<Tokens>> Players { get; set; }
    public MazeObjects[,] Maze { get; set; }
    public int[] DirectionRow { get; set; }
    public int[] DirectionCol { get; set; }

    public Game(MazeObjects[,] maze)
    {
        //                          N  S  E  W 
        DirectionRow = new int[] { -1, 1, 0, 0, };
        DirectionCol = new int[] { 0, 0, 1, -1 };
        Players = new List<List<Tokens>>();
        Maze = maze;
        Turn = 0;
    }
    public void MoveToken(int turn, int posToken, int index)
    {
        if (CanMove(turn, posToken) && ValidMove(turn, posToken, index))
        {
            Players[turn][posToken].X += DirectionRow[index];
            Players[turn][posToken].Y += DirectionCol[index];
        }
    }
    public bool ValidMove(int turn, int posToken, int index)
    {
        int row = Players[turn][posToken].X + DirectionRow[index];
        int col = Players[turn][posToken].Y + DirectionCol[index];
        if (ObjectAt(row, col) == MazeObjects.Wall)
        {
            return false;
        }
        
    }

    public bool CanMove(int turn, int posToken)
    {
        if (Players[turn][posToken].State == State.None)
        {
            return false;
        }
        return false;
    }
    public void ChangeTurn()
    {
        Turn++;
        if (Turn == Players.Count)
        {
            Turn = 0;
        }
    }
    public bool CanUseSkill(int posToken)
    {
        if (Players[Turn][posToken].Skill.Count == 0)
        {
            return true;
        }
        return false;
    }
    public MazeObjects ObjectAt(int row, int col)
    {
        return Maze[row, col];
    }
    public void Play(int posToken)
    {

    }
}