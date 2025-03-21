using System;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var commandType = Console.ReadLine();
            while (commandType != "Beast!")
            {
                var commandData = Console.ReadLine();
                var commandArgs = commandData.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                switch (commandType)
                {
                    case "Dog":
                        {
                            var dog = new Dog(commandArgs[0], int.Parse(commandArgs[1]), commandArgs[2]);
                            Console.WriteLine(dog.ToString());
                            break;
                        }
                    case "Frog":
                        {
                            var frog = new Frog(commandArgs[0], int.Parse(commandArgs[1]), commandArgs[2]);
                            Console.WriteLine(frog.ToString());
                            break;
                        }
                    case "Cat":
                        {
                            var cat = new Cat(commandArgs[0], int.Parse(commandArgs[1]), commandArgs[2]);
                            Console.WriteLine(cat.ToString());
                            break;
                        }
                    case "Kitten":
                        {
                            var kitten = new Kitten(commandArgs[0], int.Parse(commandArgs[1]));
                            Console.WriteLine(kitten.ToString());
                            break;
                        }
                    case "Tomcat":
                        {
                            var tomcat = new Tomcat(commandArgs[0], int.Parse(commandArgs[1]));
                            Console.WriteLine(tomcat.ToString());
                            break;
                        }
                }


                commandType = Console.ReadLine();
            }
        }
    }
}
