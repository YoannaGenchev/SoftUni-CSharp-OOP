namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private string make = GetRandomString();
        private string model = GetRandomString();
        private double fuelConsumption = Random.Shared.Next();
        private double fuelCapacity = Random.Shared.Next();
        private Car car;

        private static string GetRandomString()
        {
            var randomTextLength = Random.Shared.Next(minValue: 5, maxValue: 50);
            return GetRandomString(randomTextLength);
        }

        private static string GetRandomString(int length)
        {
            var symbols = new char[length];
            for (var i = 0; i < length; i++)
            {
                var randomLetterIndex = Random.Shared.Next(maxValue: 26);
                symbols[i] = (char)('a' + randomLetterIndex);
            }

            return new string(symbols);
        }

        [SetUp]
        public void SetUpCar()
        {
            car = new Car(make, model, fuelConsumption, fuelCapacity);
        }

        [Test]
        public void TestCarConstructor()
        {
            Assert.IsNotNull(car);

            var newCar = new Car(make, model,fuelConsumption,fuelCapacity);
            Assert.IsNotNull(newCar);
        }

        [Test]
        public void TestCarGettersWorkCorrectly()
        {
            Assert.AreEqual(0, car.FuelAmount);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
        }

        [Test]
        public void TestCarArgumentSettersThrowsCorrectly()
        {
            string nullString = null;
            string emptyString = string.Empty;
            double zero = 0;
            double negative = -1;

            Assert.Throws<ArgumentException>(() => _ = new Car(nullString, model, fuelConsumption, fuelCapacity));
            Assert.Throws<ArgumentException>(() => _ = new Car(emptyString, model, fuelConsumption, fuelCapacity));
            Assert.Throws<ArgumentException>(() => _ = new Car(make, nullString, fuelConsumption, fuelCapacity));
            Assert.Throws<ArgumentException>(() => _ = new Car(make, emptyString, fuelConsumption, fuelCapacity));
            Assert.Throws<ArgumentException>(() => _ = new Car(make, model, zero, fuelCapacity));
            Assert.Throws<ArgumentException>(() => _ = new Car(make, model, negative, fuelCapacity));
            Assert.Throws<ArgumentException>(() => _ = new Car(make, model, fuelConsumption, zero));
            Assert.Throws<ArgumentException>(() => _ = new Car(make, model, fuelConsumption, negative));
        }

        [Test]
        public void TestRefuelCarCorrectly()
        {
            var fuelAmount = Random.Shared.Next(minValue: 1, maxValue: (int)(car.FuelCapacity - car.FuelAmount));
            var currentCarFuel = car.FuelAmount;
            car.Refuel(fuelAmount);
            Assert.AreEqual(fuelAmount + currentCarFuel, car.FuelAmount);

            car.Refuel(car.FuelCapacity + 1);
            Assert.AreEqual(car.FuelCapacity, car.FuelAmount);
        }

        [Test]
        public void TestRefuelThrowsWithZeroOrNegativeFuelAmount()
        {
            double zero = 0;
            double negative = -1;

            Assert.Throws<ArgumentException>(() => car.Refuel(zero));
            Assert.Throws<ArgumentException>(() => car.Refuel(negative));
        }

        [Test]
        public void TestDriveCarWorksCorrectly()
        {
            car.Refuel(car.FuelCapacity);
            double distance = 100;
            var currentCarFuel = car.FuelAmount;
            double fuelNeeded = (distance / 100) * car.FuelConsumption;
            car.Drive(distance);

            Assert.AreEqual(currentCarFuel - fuelNeeded, car.FuelAmount);
        }

        [Test]
        public void TestDriveCarThrowsCorrectly()
        {
            car.Refuel(car.FuelCapacity);
            double distance = double.MaxValue;
            Assert.Throws<InvalidOperationException>(() => car.Drive(distance));
        }
    }
}