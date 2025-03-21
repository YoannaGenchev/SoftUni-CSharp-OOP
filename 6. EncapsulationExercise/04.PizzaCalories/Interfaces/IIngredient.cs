using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Interfaces
{
    public interface IIngredient
    {
        const double BaseWeightModifier = 2;

        double Weight { get; }
        double CalculateCalories();
    }
}
