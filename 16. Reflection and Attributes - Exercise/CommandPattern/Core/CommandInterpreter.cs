using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    using CommandPattern.Core.Contracts;
    using System.Reflection;

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            var commandArgs = args.Split();

            var assembly = Assembly.GetCallingAssembly();
            var commandType = assembly.GetTypes().FirstOrDefault(c => typeof(ICommand).IsAssignableFrom(c) && c.Name.StartsWith(commandArgs[0]));
            if (commandType is null)
            {
                throw new InvalidOperationException("Invalid command");
            }

            var commandObj = Activator.CreateInstance(commandType);
            if (commandObj is ICommand command)
            {
                return command.Execute(commandArgs[1..]);
            }

            return string.Empty;
        }
    }
}
