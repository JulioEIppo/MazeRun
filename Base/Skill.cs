public class Skill
{
    public string Name { get; set; }
    public int CoolTime { get; set; } // amount of turns required to reuse skill
    public int Count { get; set; } // amount of turns left to use skill
    public Skill(string name, int coolTime)
    {
        Name = name;
        CoolTime = coolTime;
        Count = 0;
    }
    public bool CanUseSkill()
    {
        return Count <= 0;
    }
    public void PrintSkillDescription()
    {
        Console.WriteLine($"Habilidad: {Name}");
        Console.WriteLine($"Tiempo de enfriamiento de {CoolTime} turnos");
        Console.WriteLine("Efecto:");
        switch (Name)
        {
            case "SpeedUpgrade":
                Console.WriteLine("Aumenta la velocidad de la ficha en 1"); break;
            case "BreakObstacle":
                Console.WriteLine("Permite romper una pared adyacente a la ficha"); break;
            case "Swap":
                Console.WriteLine("Intercambia las posiciones de una ficha con otra ficha aleatoria"); break;
            case "Teleport":
                Console.WriteLine("Permite a la ficha teletransportarse a una casilla aleatoria del laberinto"); break;
            case "Paralyze":
                Console.WriteLine("Paraliza una ficha aleatoria del juego"); break;
            default:
                Console.WriteLine(""); break;
        }
    }
}