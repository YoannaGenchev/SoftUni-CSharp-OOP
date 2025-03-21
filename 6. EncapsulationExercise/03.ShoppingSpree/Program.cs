using System.Collections.Specialized;

namespace ShoppingSpree
{
    public class Program
    {
        static void Main(string[] args)
        {
            var people = new List<KeyValuePair<string, Person>>();
            var peopleData = Console.ReadLine()
                                        .Split(";", StringSplitOptions.RemoveEmptyEntries);

            try
            {
                foreach (var person in peopleData)
                {
                    var personData = person.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    people.Add(new KeyValuePair<string, Person>(personData[0], new Person(personData[0], decimal.Parse(personData[1]))));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            var products = new Dictionary<string, Product>();
            var productsData = Console.ReadLine()
                                    .Split(";", StringSplitOptions.RemoveEmptyEntries);

            try
            {
                foreach (var product in productsData)
                {
                    var productData = product.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    products[productData[0]] = new Product(decimal.Parse(productData[1]), productData[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            var command = Console.ReadLine();
            while (command != "END")
            {
                var commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                people.Find(p => p.Key == commandArgs[0]).Value.TryPurchase(products[commandArgs[1]]);

                command = Console.ReadLine();
            }

            people.ForEach(pair => Console.WriteLine(pair.Value.ToString()));
        }
    }
}
