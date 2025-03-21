namespace Stealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var Spy = new Spy();
            var result = Spy.StealFieldInfo("Stealer.Hacker", "username", "password");
            Console.WriteLine(result);
        }
    }
}
