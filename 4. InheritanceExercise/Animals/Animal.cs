using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public abstract class Animal
    {
        protected Animal(string name, int age, string gender)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Invalid input!");
            if (age < 0) throw new ArgumentException("Invalid input!");
            if (string.IsNullOrEmpty(gender)) throw new ArgumentException("Invalid input!");

            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public abstract string ProduceSound();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().Name);
            sb.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            sb.Append(this.ProduceSound());

            return sb.ToString();
        }
    }
}
