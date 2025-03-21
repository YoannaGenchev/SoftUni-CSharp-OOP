using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public abstract class Vehicle
    {
        public abstract double FuelQuantity { get; protected set; }
        public abstract double FuelConsumption { get; protected set; }
        public abstract double TankCapacity { get; protected set; }

        public abstract void Drive(double kilometers);
        public abstract void Refuel(double fuelLitres);

        public override string ToString()
        {
            return $"{GetType().Name}: {FuelQuantity:F2}";
        }
    }
}
