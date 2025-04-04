using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    using CommandPattern.Core.Contracts;

    public class Engine : IEngine
    {
        private readonly ICommandInterpreter _interpreter;

        public Engine(ICommandInterpreter interpreter)
        {
            _interpreter = interpreter ?? throw new ArgumentNullException(nameof(interpreter)); ;
        }

        public void Run()
        {
            var input = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(input))
            {
                var output = _interpreter.Read(input!);
                Console.WriteLine(output);

                input = Console.ReadLine();
            }
        }
    }
}
