using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Tomcat : Cat
    {
        public Tomcat(string name, int age) : base(name, age, DefaultGender) { }

        private const string DefaultGender = "Male";

        public override string ProduceSound()
        {
            return "MEOW";
        }
    }
}
