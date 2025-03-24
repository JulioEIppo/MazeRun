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
        Console.Clear();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Bienvenido a Maze Run")
            .PageSize(10)
            .AddChoices([
                "Game Start", "Guia", "Salir"
            ])
        );
        switch (option)
        {
            case "Game Start":
                List<Player> players = GeneratePlayers();
                Cell[,] maze = GenerateMaze(players);
                Game game = new Game(maze, players);
                game.Play(); break;
            case "Guia":
                GameGuide(); break;
            case "Exit":
                return;
        }
    }

    static List<Token> defaultTokens = new List<Token>() {
        new Token("Luffy", 5, new Skill("ExtraStep", 2), "👒"),
        new Token("Optimus Prime", 4, new Skill("BreakObstacle", 2), "🤖"),
        new Token("Harry Potter", 4 , new Skill("Swap", 2), "⚡"),
        new Token("T-Rex", 4, new Skill("Paralyze", 2), "🦖"),
        new Token("Dracula", 5, new Skill("Teleport", 2), "🦇"),
        new Token("John Pork", 4, new Skill("Swap", 2), "🐷")
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
        var playersAmount = AnsiConsole.Prompt(
            new TextPrompt<int>("Ingrese la cantidad de jugadores")
            .Validate((n) =>
            {
                {
                    if (n > 0 && n <= defaultTokens.Count)
                    {
                        return ValidationResult.Success();
                    }
                    else if (n > defaultTokens.Count)
                    {
                        return ValidationResult.Error($"La cantidad maxima de jugadores es {defaultTokens.Count}");
                    }
                    else
                    {
                        return ValidationResult.Error("[red]Entrada invalida,[/] intente de nuevo");
                    }
                }
            }
            )
        );
        for (int i = 0; i < playersAmount; i++)
        {
            var token = AnsiConsole.Prompt(
                new SelectionPrompt<Token>()
                .Title("Selecciona una ficha")
                .PageSize(10)
                .AddChoices(defaultTokens)
            );
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
    public static void Guide()
    {
        Console.Clear();
        // AnsiConsole.Write("Selecciona una ficha para ver su descripcion");
        var token = AnsiConsole.Prompt(
        new SelectionPrompt<Token>()
        .AddChoices(defaultTokens)
        );
        token.Description();
        // Console.WriteLine("Fichas disponibles:");
        // int count = 1;
        // for (int i = 0; i < defaultTokens.Count; i++)
        // {
        //     Console.WriteLine($"{count} - {defaultTokens[i].Name}");
        //     count++;
        // }
        // Console.WriteLine("Selecciona el numero de una ficha para ver su descripcion");
        // string option = Console.ReadLine()!;
        // int selected;
        // while (!int.TryParse(option, out selected) || selected > defaultTokens.Count || selected <= 0)
        // {
        //     Console.WriteLine("Numero invalido. Intente de nuevo");
        //     option = Console.ReadLine()!;
        // }
        Console.ReadKey();
        GameMenu();
    }
    public static void GameGuide()
    {
        AnsiConsole.MarkupLine("Bienvenido a [yellow]Maze Run[/]");
        AnsiConsole.MarkupLine("Nuestros [green]Héroes[/] han sido transportados misteriosamente a un laberinto, una voz omnipresente les asegura q en alguna parte del laberinto hay un portal que los sacara de alli");
        AnsiConsole.MarkupLine("Sin embargo solo una persona puede escapar, una vez que alguien llegue al portal este se cerrara irremediablemente, dejando atrapados a todos aquellos que hayan fallado ");
        AnsiConsole.MarkupLine("Tu objetivo sera llevar a tu [green]Héroe[/] al portal y escapar del laberinto antes que el resto");
        AnsiConsole.MarkupLine("Pero cuidado!!! Hay trampas esparcidas por todo el laberinto para evitar que escapes");
        AnsiConsole.MarkupLine("Sin embargo tu [green]Héroe[/] tiene una habilidad especial que sera vital para lograr escapar. Usala a tu favor!!!");
        AnsiConsole.MarkupLine("[blue]Buena Suerte[/]");
        AnsiConsole.MarkupLine("Pulsa [red]T[/] para ver las trampas y sus efectos, o bien pulsa [green]H[/] para ver los Héroes y sus habilidades");
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.T)
        {
            TrapsDescription();
        }
        else if (key.Key == ConsoleKey.H)
        {
            Guide();
        }
        else
        {
            GameMenu();
        }

    }
    public static void TrapsDescription()
    {
        Console.Clear();
        var trap = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Selecciona una trampa para ver su descripcion")
            .PageSize(10)
            .AddChoices(new[] {
                "SpeedDown", "Paralize", "Teleport"
            })
        );
        switch (trap)
        {
            case "SpeedDown":
                AnsiConsole.WriteLine("Esta trampa  dismniuira la velocidad de tu Héroe en 1"); break;
            case "Paralize":
                AnsiConsole.WriteLine("Tu Héroe quedará paralizado por 2 turnos"); break;
            case "Teleport":
                AnsiConsole.WriteLine("Tu Héroe se teletransportara a una casilla aleatoria del laberinto"); break;
        }
        Console.ReadKey();
        GameMenu();
    }
}