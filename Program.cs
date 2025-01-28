using Spectre.Console;

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        List<Player> players = GeneratePlayers();
        Cell[,] maze = GenerateMaze(players);
        Game game = new Game(maze, players);
        // game.InitializeRound(maze, players);
        game.Play();
        // game.Stop();
        // game.PrintMaze();
    }

    static List<Token> defaultTokens = new List<Token>() {
        new Token("Luffy", 5, new SkillBreakObstacle("Gomu Gomu", 3), "👒"),
        new Token("Zoro", 4, new SkillSight("Eye Sight", 3), "🤖"),
        new Token("Sanji", 4 , new SkillSight("Eye Sight", 3), "🦵"),
    };
    public static Cell[,] GenerateMaze(List<Player> players)
    {
        var generator = new MazeGenerator();
        Cell[,] maze = generator.Generate(11, 11, 5, players);
        return maze;
    }


    public static List<Player> GeneratePlayers()
    {
        List<Player> players = new List<Player>();
        Console.WriteLine(" Ingrese la cantidad de jugadores");
        int playersAmount = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < playersAmount; i++)
        {
            Console.WriteLine("Selecciona el numero de una ficha");
            PrintTokens();
            string s = Console.ReadLine()!;
            int selected;
            while (!int.TryParse(s, out selected))
            {
                Console.WriteLine("Numero invalido vuelva a intentarlo");
                s = Console.ReadLine()!;
            }
            Token token = defaultTokens[selected - 1];
            Player player = new Player(token);
            players.Add(player);
            defaultTokens.Remove(token);
        }

        return players;
    }

    public static void PrintTokens()
    {
        for (int i = 0; i < defaultTokens.Count; i++)
        {
            Console.WriteLine(i + 1 + " - " + defaultTokens[i].Name);
        }

    }
}