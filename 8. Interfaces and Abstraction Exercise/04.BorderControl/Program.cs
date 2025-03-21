namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var identifiers = new List<IIdentifiable>();
            var command = Console.ReadLine();
            while (command != null && command != "End")
            {
                var commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (commandArgs.Length == 2 )
                {
                    identifiers.Add(new Robot(commandArgs[0], commandArgs[1]));
                }
                else if (commandArgs.Length == 3 )
                {
                    identifiers.Add(new Citizen(commandArgs[0], int.Parse(commandArgs[1]), commandArgs[2]));
                }

                command = Console.ReadLine();
            }

            var fakeIds = Console.ReadLine();
            var criminals = identifiers.Where(i => i.Id.EndsWith(fakeIds)).Select(i => i.Id).ToList(); ;
            criminals.ForEach(i => Console.WriteLine(i));
        }
    }
}
