using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Interfaces;

namespace WildFarm.Animals
{
    public abstract class Animal : IAnimal
    {
        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        protected abstract double GrowthCoeff { get; }

        public string Name { get; }
        public double Weight { get; private set; }

        public int FoodEaten { get; private set; }

        public abstract string AskForFood();

        public virtual bool Eat(IFood food)
        {
            Weight += food.Quantity * GrowthCoeff;
            FoodEaten += food.Quantity;
            return true;
        }
    }
}
