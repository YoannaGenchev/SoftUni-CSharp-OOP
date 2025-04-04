namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class DatabaseTests
    {
        private const int MaxCapacity = 16;

        private static int[] GetRandomInts(int length)
        {
            var result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = Random.Shared.Next();
            }

            return result;
        }

        [TestCase(0)]
        [TestCase(MaxCapacity - 1)]
        [TestCase(MaxCapacity)]
        public void DatabaseShouldBeCreatesCorrectly(int elementsCount)
        {
            var data = GetRandomInts(elementsCount);
            var database = new Database(data);

            Assert.AreEqual(database.Count, elementsCount);
            Assert.AreEqual(database.Fetch(), data);
        }

        [TestCase(MaxCapacity + 1)]
        public void DatabaseShouldNotBeCreatedWithMoreElementsThanCapacity(int elementsCount)
        {
            var data = GetRandomInts(elementsCount);
            Assert.Throws<InvalidOperationException>(() => _ = new Database(data));
        }

        [Test]
        public void AddShouldWorkCorrectly()
        {
            var data = GetRandomInts(MaxCapacity);
            var database = new Database();

            foreach (var num in data)
            {
                database.Add(num);
            }

            Assert.AreEqual(database.Count, data.Length);
            Assert.AreEqual(database.Fetch(), data);

            var number = Random.Shared.Next();
            Assert.Throws<InvalidOperationException>(() => database.Add(number));
        }

        [Test]
        public void RemoveShouldWorkCorrectly()
        {
            var data = GetRandomInts(MaxCapacity);
            var database = new Database(data);
            var count = data.Length;

            for (var i = 0; i < data.Length; i++)
            {
                database.Remove();
                count--;

                Assert.AreEqual(database.Count, count);
                Assert.AreEqual(database.Fetch(), data.Take(count));
            }

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
    }
}
