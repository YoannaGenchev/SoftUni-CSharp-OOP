namespace _01._Square_Root
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var num = int.Parse(Console.ReadLine());
                if (num < 0)
                {
                    throw new ArgumentException();
                }

                Console.WriteLine(Math.Sqrt(num));
            }
            catch
            {
                Console.WriteLine("Invalid number.");
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
