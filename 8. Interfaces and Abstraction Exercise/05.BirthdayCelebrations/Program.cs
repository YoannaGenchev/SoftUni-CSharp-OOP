namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var livingBeings = new List<IBirthable>();
            var identifiable = new List<IIdentifiable>();
            var command = Console.ReadLine();
            while (command != null && command != "End")
            {
                var commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                switch (commandArgs[0])
                {
                    case "Pet":
                        {
                            livingBeings.Add(new Pet(commandArgs[1], commandArgs[2]));
                            break;
                        }
                    case "Citizen":
                        {
                            livingBeings.Add(new Citizen(commandArgs[1], int.Parse(commandArgs[2]), commandArgs[3], commandArgs[4]));
                            break;
                        }
                    case "Robot":
                        {
                            identifiable.Add(new Robot(commandArgs[1], commandArgs[2]));
                            break;
                        }
                }

                command = Console.ReadLine();
            }

            var yearToCheck = Console.ReadLine();
            var bornIn = livingBeings.Where(i => i.Birthdate.EndsWith(yearToCheck)).Select(i => i.Birthdate).ToList(); ;
            bornIn.ForEach(i => Console.WriteLine(i));
        }
    }
}
