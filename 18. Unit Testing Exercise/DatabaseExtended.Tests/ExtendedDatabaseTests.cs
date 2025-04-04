namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System.Linq;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private const int MaxCapacity = 16;

        private static Person[] GetRandomPeople(int length)
        {
            var result = new Person[length];
            for (var i = 0; i < length; i++)
            {
                var person = GetRandomPerson();
                if (i > 0)
                {
                    while (result.Take(i).Any(p => p.UserName == person.UserName || p.Id == person.Id))
                    {
                        person = GetRandomPerson();
                    }
                }

                result[i] = person;
            }

            return result;
        }

        private static Person GetRandomPerson()
        {
            return new Person(Random.Shared.NextInt64(), GetRandomString());
        }

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

        [TestCase(0)]
        [TestCase(MaxCapacity - 1)]
        [TestCase(MaxCapacity)]
        public void DatabaseShouldBeCreatedCorrectly(int elementsCount)
        {
            var data = GetRandomPeople(elementsCount);
            var database = new Database(data);

            Assert.AreEqual(database.Count, elementsCount);
            
            foreach (var person in data)
            {
                var checkedPerson = database.FindByUsername(person.UserName);
                Assert.IsNotNull(checkedPerson);
                Assert.AreEqual(person.UserName, checkedPerson.UserName);
                Assert.AreEqual(person.Id, checkedPerson.Id);

                var checkedPerson2 = database.FindById(person.Id);
                Assert.IsNotNull(checkedPerson2);
                Assert.AreEqual(person.UserName, checkedPerson2.UserName);
                Assert.AreEqual(person.Id, checkedPerson2.Id);
            }
        }

        [TestCase(MaxCapacity + 1)]
        public void DatabaseShouldNotBeCreatedWithMoreElementsThanCapacity(int elementsCount)
        {
            var data = GetRandomPeople(elementsCount);
            Assert.Throws<ArgumentException>(() => _ = new Database(data));
        }

        [Test]
        public void AddShouldWorkCorrectly()
        {
            var data = GetRandomPeople(MaxCapacity - 1);
            var database = new Database();

            foreach (var person in data)
            {
                database.Add(person);

                var checkedPerson = database.FindByUsername(person.UserName);
                Assert.IsNotNull(checkedPerson);
                Assert.AreEqual(person.UserName, checkedPerson.UserName);
                Assert.AreEqual(person.Id, checkedPerson.Id);

                var checkedPerson2 = database.FindById(person.Id);
                Assert.IsNotNull(checkedPerson2);
                Assert.AreEqual(person.UserName, checkedPerson2.UserName);
                Assert.AreEqual(person.Id, checkedPerson2.Id);
            }

            Assert.AreEqual(database.Count, data.Length);

            var id = Random.Shared.NextInt64();
            var name = GetRandomString();

            var personWithExistingName = new Person(id, data.Last().UserName);
            var personWithExistingId = new Person(data.Last().Id, name);

            Assert.Throws<InvalidOperationException>(() => database.Add(personWithExistingName));
            Assert.Throws<InvalidOperationException>(() => database.Add(personWithExistingId));

            var person1 = GetRandomPerson();
            database.Add(person1);

            var person2 = GetRandomPerson();
            Assert.Throws<InvalidOperationException>(() => database.Add(person2));
        }

        [Test]
        public void RemoveShouldWorkCorrectly()
        {
            var data = GetRandomPeople(MaxCapacity);
            var database = new Database(data);

            var count = data.Length;
            for (var i = 0; i < data.Length; i++)
            {
                database.Remove();
                count--;

                Assert.AreEqual(database.Count, count);
            }

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void FindByUsernameShouldWorkCorrectly()
        {
            var data = GetRandomPeople(MaxCapacity);
            var database = new Database(data);
            var randomIndex = Random.Shared.NextInt64(0, MaxCapacity);
            var randomPerson = data[randomIndex];

            Assert.AreEqual(randomPerson.UserName, database.FindByUsername(randomPerson.UserName).UserName);
            Assert.AreEqual(randomPerson.Id, database.FindByUsername(randomPerson.UserName).Id);
        }

        [Test]
        public void FindByIdShouldWorkCorrectly()
        {
            var data = GetRandomPeople(MaxCapacity);
            var database = new Database(data);
            var randomIndex = Random.Shared.NextInt64(0, MaxCapacity);
            var randomPerson = data[randomIndex];

            Assert.AreEqual(randomPerson.UserName, database.FindById(randomPerson.Id).UserName);
            Assert.AreEqual(randomPerson.Id, database.FindById(randomPerson.Id).Id);
        }

        [Test]
        public void FindByUsernameShouldThrowExceptionIfInvalidUsernameIsProvided()
        {
            var database = new Database();
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
        }

        [Test]
        public void FindByUsernameShouldThrowExceptionIfNotFound()
        {
            var database = new Database();

            var person = GetRandomPerson();
            database.Add(person);

            Assert.Throws<InvalidOperationException>(() => database.FindByUsername($"{person}-123"));
        }

        [Test]
        public void FindByIdShouldThrowExceptionIfInvalidIdIsProvided()
        {
            var database = new Database();
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
        }

        [Test]
        public void FindByIdShouldThrowExceptionIfNotFound()
        {
            var database = new Database();

            var person = GetRandomPerson();
            database.Add(person);

            Assert.Throws<InvalidOperationException>(() => database.FindById(person.Id + 1));
        }
    }
}