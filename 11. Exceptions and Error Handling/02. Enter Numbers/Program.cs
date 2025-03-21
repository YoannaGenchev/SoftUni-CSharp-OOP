namespace _02._Enter_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>();
            int currentNumber = 1;
            while (numbers.Count < 10)
            {
                var command = Console.ReadLine();
                try
                {
                    var num = int.Parse(command);
                    if (num <= currentNumber || num >= 100)
                    {
                        throw new ArgumentException($"Your number is not in range {currentNumber} - 100!");
                    }

                    currentNumber = num;
                    numbers.Add(num);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Invalid Number!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Number!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Invalid Number!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
