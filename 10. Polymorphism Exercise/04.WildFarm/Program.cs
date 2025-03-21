using WildFarm.Animals.Birds;
using WildFarm.Animals.Mammals.Felines;
using WildFarm.Animals.Mammals;
using WildFarm.Interfaces;
using WildFarm.Foods;

namespace WildFarm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var animals = new List<IAnimal>();
            var command = Console.ReadLine();
            while (command != null && command != "End")
            {
                var animal = GenerateAnimal(command);
                var food = GenerateFood(Console.ReadLine());
                Console.WriteLine(animal.AskForFood());

                if (false == animal.Eat(food))
                {
                    Console.WriteLine($"{animal.GetType().Name} does not eat {food.GetType().Name}!");
                }

                animals.Add(animal);
                command = Console.ReadLine();
            }

            animals.ForEach(animal => Console.WriteLine(animal));
        }

        private static IAnimal GenerateAnimal(string command)
        {
            var commandArgs = command.Split();

            return commandArgs[0] switch
            {
                nameof(Hen) => new Hen(commandArgs[1], double.Parse(commandArgs[2]), double.Parse(commandArgs[3])),
                nameof(Owl) => new Owl(commandArgs[1], double.Parse(commandArgs[2]), double.Parse(commandArgs[3])),
                nameof(Cat) => new Cat(commandArgs[1], double.Parse(commandArgs[2]), commandArgs[3], commandArgs[4]),
                nameof(Tiger) => new Tiger(commandArgs[1], double.Parse(commandArgs[2]), commandArgs[3], commandArgs[4]),
                nameof(Dog) => new Dog(commandArgs[1], double.Parse(commandArgs[2]), commandArgs[3]),
                nameof(Mouse) => new Mouse(commandArgs[1], double.Parse(commandArgs[2]), commandArgs[3]),
                _ => throw new InvalidOperationException("Invalid animal type")
            };
        }

        private static IFood GenerateFood(string command)
        {
            var commandArgs = command.Split();

            return commandArgs[0] switch
            {
                nameof(Fruit) => new Fruit(int.Parse(commandArgs[1])),
                nameof(Meat) => new Meat(int.Parse(commandArgs[1])),
                nameof(Seeds) => new Seeds(int.Parse(commandArgs[1])),
                nameof(Vegetable) => new Vegetable(int.Parse(commandArgs[1])),
                _ => throw new InvalidOperationException("Invalid food type")
            };
        }
    }
}
