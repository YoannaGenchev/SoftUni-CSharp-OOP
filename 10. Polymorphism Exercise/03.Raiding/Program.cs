namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var list = new List<BaseHero>();

            while (list.Count < n)
            {
                var name = Console.ReadLine();
                var heroClass = Console.ReadLine();
                switch (heroClass)
                {
                    case "Druid":
                        {
                            list.Add(new Druid(name));
                            break;
                        }
                    case "Paladin":
                        {
                            list.Add(new Paladin(name));
                            break;
                        }
                    case "Rogue":
                        {
                            list.Add(new Rogue(name));
                            break;
                        }
                    case "Warrior":
                        {
                            list.Add(new Warrior(name));
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid hero!");
                            break;
                        }
                }
            }

            list.ForEach(h => Console.WriteLine(h.CastAbility()));
            var bossPower = int.Parse(Console.ReadLine());

            if (bossPower <= list.Sum(h => h.Power))
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
