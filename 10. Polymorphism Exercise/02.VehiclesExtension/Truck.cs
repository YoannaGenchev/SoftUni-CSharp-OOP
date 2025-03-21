using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            FuelConsumption = fuelConsumption + 1.6;
            TankCapacity = tankCapacity;
            if (fuelQuantity <= TankCapacity)
            {
                FuelQuantity = fuelQuantity;
            }
            else
            {
                FuelQuantity = 0;
            }
        }

        public override double FuelQuantity { get; protected set; }

        public override double FuelConsumption { get; protected set; }

        public override double TankCapacity { get; protected set; }

        public override void Drive(double kilometers, bool withPeople = false)
        {
            if (kilometers * FuelConsumption <= FuelQuantity)
            {
                FuelQuantity -= kilometers * FuelConsumption;
                Console.WriteLine($"Truck travelled {kilometers} km");
            }
            else
            {
                Console.WriteLine("Truck needs refueling");
            }
        }

        public override void Refuel(double fuelLitres)
        {
            if (fuelLitres <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }

            if ((FuelQuantity + fuelLitres * 0.95) <= TankCapacity)
            {
                FuelQuantity += fuelLitres * 0.95;
            }
            else
            {
                Console.WriteLine($"Cannot fit {fuelLitres} fuel in the tank");
            }
        }
    }
}
