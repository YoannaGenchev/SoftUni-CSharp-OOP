using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Pizza
    {
        private const int MaxNameLength = 15;
        private const int MaxToppingCount = 10;

        private readonly List<Topping> toppings;

        public Pizza(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
            this.Name = name;
            this.toppings = new List<Topping>();
        }

        public string Name { get; private set; }
        public IReadOnlyCollection<Topping> Toppings
        {
            get
            {
                return this.toppings.AsReadOnly();
            }
        }
        public Dough Dough { get; set; }
        public double TotalCalories
        {
            get
            {
                return this.Dough.CalculateCalories() + this.toppings.Sum(topping => topping.CalculateCalories());
            }
        }

        public void AddTopping(Topping topping)
        {
            if (toppings.Count == MaxToppingCount)
            {
                throw new InvalidOperationException($"Number of toppings should be in range [0..{MaxToppingCount}].");
            }
            toppings.Add(topping);
        }

        public override string ToString()
        {
            return $"{Name} - {TotalCalories:F2} Calories.";
        }
    }
}
