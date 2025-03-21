namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var buyers = new Dictionary<string, IBuyer>();
            var count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                var commandArgs = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (commandArgs.Length == 3)
                {
                    buyers[commandArgs[0]] = new Rebel(commandArgs[0], int.Parse(commandArgs[1]), commandArgs[2]);
                }
                else if (commandArgs.Length == 4)
                {
                    buyers[commandArgs[0]] = new Citizen(commandArgs[0], int.Parse(commandArgs[1]), commandArgs[2], commandArgs[3]);
                }
            }

            var command = Console.ReadLine();
            while (command != null && command != "End")
            {
                if (buyers.ContainsKey(command))
                {
                    buyers[command].BuyFood();
                }
                command = Console.ReadLine();
            }

            Console.WriteLine(buyers.Sum(b => b.Value.Food));
        }
    }
}
