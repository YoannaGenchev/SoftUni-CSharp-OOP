namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                var command = Console.ReadLine();
                Pizza pizza = null;
                while (command != null && command != "END")
                {
                    var commandArgs = command.Split(' ');

                    switch (commandArgs[0])
                    {
                        case "Pizza":
                            {
                                pizza = new Pizza(commandArgs[1]);
                                break;
                            }
                        case "Dough":
                            {
                                pizza.Dough = new Dough(commandArgs[1], commandArgs[2], double.Parse(commandArgs[3]));
                                break;
                            }
                        case "Topping":
                            {
                                pizza.AddTopping(new Topping(commandArgs[1], double.Parse(commandArgs[2])));
                                break;
                            }
                    }

                    command = Console.ReadLine();
                }

                Console.WriteLine(pizza.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
