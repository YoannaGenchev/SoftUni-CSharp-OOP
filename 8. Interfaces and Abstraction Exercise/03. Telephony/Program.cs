namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var phoneNumbers = Console.ReadLine()
                                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var smartPhone = new SmartPhone();
            var stationaryPhone = new StationaryPhone();
            foreach (var phoneNumber in phoneNumbers)
            {
                if (phoneNumber.Any(c => !char.IsDigit(c)))
                {
                    Console.WriteLine("Invalid number!");
                }
                else if (phoneNumber.Length == 7)
                {
                    Console.WriteLine(stationaryPhone.Call(phoneNumber));
                }
                else if (phoneNumber.Length == 10)
                {
                    Console.WriteLine(smartPhone.Call(phoneNumber));
                }
            }

            var websites = Console.ReadLine()
                                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var website in websites)
            {
                if (website.Any(char.IsDigit))
                {
                    Console.WriteLine("Invalid URL!");
                }
                else
                {
                    Console.WriteLine(smartPhone.Browse(website));
                }    
            }
        }
    }
}
