namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<string, Vehicle>();

            var command = Console.ReadLine();
            var commandArgsCar = command.Split(" ");
            vehicles["Car"] = new Car(double.Parse(commandArgsCar[1]), double.Parse(commandArgsCar[2]));
            

            command = Console.ReadLine();
            var commandArgsTruck = command.Split(" ");
            vehicles["Truck"] = new Truck(double.Parse(commandArgsTruck[1]), double.Parse(commandArgsTruck[2]));
            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                command = Console.ReadLine();
                var commandArgs = command.Split(" ");
                if (commandArgs[0] == "Drive")
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
        }
    }
}
