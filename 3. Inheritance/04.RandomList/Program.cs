namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList list = new RandomList { "first", "second", "third", "hello", "world"};

            while (list.Count > 0)
            {
                Console.WriteLine(list.RandomString());
            }
        }
    }
}
