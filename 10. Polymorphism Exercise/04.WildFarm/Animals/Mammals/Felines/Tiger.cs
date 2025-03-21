using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Foods;
using WildFarm.Interfaces;

namespace WildFarm.Animals.Mammals.Felines
{
    public class Tiger : Feline
    {
        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        protected override double GrowthCoeff => 1.00;

        public override string AskForFood()
        {
            return "ROAR!!!";
        }

        public override bool Eat(IFood food)
        {
            return food is Meat && base.Eat(food);
        }
    }
}
