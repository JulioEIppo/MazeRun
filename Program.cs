using Spectre.Console;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        while (true)
        {
            GameMenu();
            return;
        }
    }
    public static void GameMenu()
    {
        Console.WriteLine("Bienvenido a Maze Run");
        Console.WriteLine("Selecciona una opcion");
        Console.WriteLine("1- Iniciar Juego");
        Console.WriteLine("2- Salir");
        string option = Console.ReadLine()!;
        switch (option)
        {
            case "1":
                List<Player> players = GeneratePlayers();
                Cell[,] maze = GenerateMaze(players);
                Game game = new Game(maze, players);
                game.Play(); break;
            case "2":
                return;
            default:
                Console.WriteLine("Opcion invalida, intente de nuevo");
                break;
        }
    }

    static List<Token> defaultTokens = new List<Token>() {
        new Token("Luffy", 5, new Skill("SpeedUpgrade", 2), "👒"),
        new Token("Optimus Prime", 4, new Skill("BreakObstacle", 2), "🤖"),
        new Token("Harry Potter", 4 , new Skill("Swap", 2), "⚡"),
        new Token("T-Rex", 4, new Skill("Paralyze", 2), "🦖")
    };
    public static Cell[,] GenerateMaze(List<Player> players)
    {
        var generator = new MazeGenerator();
        Cell[,] maze = generator.Generate(15, 15, 15, players);
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
            while (!int.TryParse(s, out selected) || selected == 0)
            {
                Console.WriteLine("Numero invalido vuelva a intentarlo");
                s = Console.ReadLine()!;
            }
            Token token = defaultTokens[selected - 1];
            Player player = new Player(token);
            players.Add(player);
            defaultTokens.Remove(token);
            Console.Clear();
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