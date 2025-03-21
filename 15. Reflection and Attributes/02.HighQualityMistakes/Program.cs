using Stealer;

namespace HighQualityMistakes
{
    public class StartUp
    {
        static void Main(string[] args)
        {
           var spy = new Spy();
            string result = spy.AnalyzeAccessModifiers("Stealer.Hacker");
            Console.WriteLine(result);
        }
    }
}
