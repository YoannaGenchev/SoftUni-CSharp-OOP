using PizzaCalories.Enums;
using PizzaCalories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough : IIngredient
    {
        private const int MinWeight = 1;
        private const int MaxWeight = 200;

        public double Weight { get; private set; }
        public FlourType FlourType { get; private set; }
        public BackingTechnique BackingTechnique { get; private set; }

        private static readonly Dictionary<FlourType, double> FlourTypeModifiers = new () 
        { 
            [FlourType.White] = 1.5,
            [FlourType.Wholegrain] = 1 
        };

        private static readonly Dictionary<BackingTechnique, double> BackingTechniqueModifiers = new()
        {
            [BackingTechnique.Crispy] = 0.9,
            [BackingTechnique.Chewy] = 1.1,
            [BackingTechnique.Homemade] = 1
        };

        public Dough(string flourType, string backingTechnique, double weight)
        {
            if (!Enum.TryParse<FlourType>(flourType, ignoreCase: true, out var parsedFlourType) ||
                !Enum.TryParse<BackingTechnique>(backingTechnique, ignoreCase: true, out var parsedBackingTechnique))
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            if (weight < MinWeight ||  weight > MaxWeight)
            {
                throw new ArgumentException($"Dough weight should be in the range [{MinWeight}..{MaxWeight}].");
            }

            this.FlourType = parsedFlourType;
            this.BackingTechnique = parsedBackingTechnique;
            this.Weight = weight;
        }

        public double CalculateCalories()
        {
            return IIngredient.BaseWeightModifier * this.Weight * FlourTypeModifiers[this.FlourType] * BackingTechniqueModifiers[this.BackingTechnique];
        }
    }
}
