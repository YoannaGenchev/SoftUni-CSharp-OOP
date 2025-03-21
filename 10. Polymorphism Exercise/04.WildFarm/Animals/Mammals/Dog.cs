using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Foods;
using WildFarm.Interfaces;

namespace WildFarm.Animals.Mammals
{
    public class Dog : Mammal
    {
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        protected override double GrowthCoeff => 0.40;

        public override string AskForFood()
        {
            return "Woof!";
        }

        public override bool Eat(IFood food)
        {
            return food is Meat && base.Eat(food);
        }
    }
}
