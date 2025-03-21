using Stealer;

namespace Collector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var spy = new Spy();
            string result = spy.CollectGettersAndSetters("Stealer.Hacker");
            Console.WriteLine(result);
        }
    }
}
