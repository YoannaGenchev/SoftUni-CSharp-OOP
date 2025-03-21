using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Foods;
using WildFarm.Interfaces;

namespace WildFarm.Animals.Birds
{
    public class Owl : Bird
    {
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        protected override double GrowthCoeff => 0.25;

        public override string AskForFood()
        {
            return "Hoot Hoot";
        }

        public override bool Eat(IFood food)
        {
            return food is Meat && base.Eat(food);
        }
    }
}
