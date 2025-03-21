namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<string, Vehicle>();

            var command = Console.ReadLine();
            var commandArgsCar = command.Split(" ");
            vehicles["Car"] = new Car(double.Parse(commandArgsCar[1]), double.Parse(commandArgsCar[2]), double.Parse(commandArgsCar[3]));
            

            command = Console.ReadLine();
            var commandArgsTruck = command.Split(" ");
            vehicles["Truck"] = new Truck(double.Parse(commandArgsTruck[1]), double.Parse(commandArgsTruck[2]), double.Parse(commandArgsTruck[3]));

            command = Console.ReadLine();
            var commandArgsBus = command.Split(" ");
            vehicles["Bus"] = new Bus(double.Parse(commandArgsBus[1]), double.Parse(commandArgsBus[2]), double.Parse(commandArgsBus[3]));

            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                command = Console.ReadLine();
                var commandArgs = command.Split(" ");
                if (commandArgs[0] == "Drive")
                {
                    vehicles[commandArgs[1]].Drive(double.Parse(commandArgs[2]), true);
                }
                else if (commandArgs[0] == "DriveEmpty")
                {
                    vehicles[commandArgs[1]].Drive(double.Parse(commandArgs[2]));
                }
                else if (commandArgs[0] == "Refuel")
                {
                    vehicles[commandArgs[1]].Refuel(double.Parse(commandArgs[2]));
                }
            }

            Console.WriteLine(vehicles["Car"].ToString());
            Console.WriteLine(vehicles["Truck"].ToString());
            Console.WriteLine(vehicles["Bus"].ToString());
        }
    }
}
