using System;
using ValidationAttributes.Models;

namespace ValidationAttributes
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person = new Person(null, -1);

            var isValidEntity = Validator.IsValid(person);
            Console.WriteLine(isValidEntity);
        }
    }
}
