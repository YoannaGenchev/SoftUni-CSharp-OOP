using Stealer;

namespace MissionPrivateImpossible
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var spy = new Spy();
            string result = spy.RevealPrivateMethods("Stealer.Hacker");
            Console.WriteLine(result);
        }
    }
}
