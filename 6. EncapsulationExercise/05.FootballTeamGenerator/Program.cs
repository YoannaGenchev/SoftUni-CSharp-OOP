namespace FootballTeamGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var command = Console.ReadLine();
            var teams = new Dictionary<string, Team>();
            while (command != "END")
            {
                var commandArgs = command.Split(";", StringSplitOptions.RemoveEmptyEntries);
                switch(commandArgs[0])
                {
                    case "Team":
                        {
                            try
                            {
                                teams[commandArgs[1]] = new Team(commandArgs[1]);
                            }
                            catch(ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case "Add":
                        {
                            try
                            {
                                if (teams.ContainsKey(commandArgs[1]))
                                {
                                    teams[commandArgs[1]].AddPlayer(new Player(commandArgs[2],
                                                                               int.Parse(commandArgs[3]),
                                                                               int.Parse(commandArgs[4]),
                                                                               int.Parse(commandArgs[5]),
                                                                               int.Parse(commandArgs[6]),
                                                                               int.Parse(commandArgs[7])));
                                }
                                else
                                {
                                    Console.WriteLine($"Team {commandArgs[1]} does not exist.");
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case "Remove":
                        {
                            try
                            {
                                if (teams.ContainsKey(commandArgs[1]))
                                {
                                    teams[commandArgs[1]].RemovePlayer(commandArgs[2]);
                                }
                                else
                                {
                                    Console.WriteLine($"Team {commandArgs[1]} does not exist.");
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case "Rating":
                        {
                            try
                            {
                                if (teams.ContainsKey(commandArgs[1]))
                                {
                                    Console.WriteLine(teams[commandArgs[1]].GetRating());
                                }
                                else
                                {
                                    Console.WriteLine($"Team {commandArgs[1]} does not exist.");
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                }

                command = Console.ReadLine();
            }
        }
    }
}
