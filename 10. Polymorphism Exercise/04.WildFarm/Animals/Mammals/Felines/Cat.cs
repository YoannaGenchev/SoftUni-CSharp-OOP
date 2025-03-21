using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Foods;
using WildFarm.Interfaces;

namespace WildFarm.Animals.Mammals.Felines
{
    public class Cat : Feline
    {
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        protected override double GrowthCoeff => 0.30;

        public override string AskForFood()
        {
            return "Meow";
        }

        public override bool Eat(IFood food)
        {
            return food is Vegetable or Meat && base.Eat(food);
        }
    }
}
