using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Foods;
using WildFarm.Interfaces;

namespace WildFarm.Animals.Mammals
{
    public class Mouse : Mammal
    {
        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        protected override double GrowthCoeff => 0.10;

        public override string AskForFood()
        {
            return "Squeak";
        }

        public override bool Eat(IFood food)
        {
            return food is Vegetable or Fruit && base.Eat(food);
        }
    }
}
