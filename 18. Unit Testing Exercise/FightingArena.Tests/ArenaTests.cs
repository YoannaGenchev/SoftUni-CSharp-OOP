namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior = new Warrior("gosho", 50, 50);

        [SetUp]
        public void SetUpArena()
        {
            this.arena = new Arena();

            var warriorOne = new Warrior("W1", 100, 250);
            var warriorTwo = new Warrior("W2", 200, 250);

            this.arena.Enroll(warriorOne);
            this.arena.Enroll(warriorTwo);
        }

        [Test]
        public void TestCreatedArena()
        {
            Assert.IsNotNull(this.arena);
            Assert.True(this.arena.Warriors.Count() == 2);
            Assert.True(this.arena.Warriors.Any(w => w.Name == "W1"));
            Assert.True(this.arena.Warriors.Any(w => w.Name == "W2"));
        }

        [Test]
        public void TestArenaConstructor()
        {
            var newArena = new Arena();
            Assert.IsNotNull(newArena);
            Assert.False(newArena.Warriors.Any());
        }

        [Test]
        public void TestEnrollWorksCorrectly()
        {
            var warrior = new Warrior("WNew", 100, 250);
            this.arena.Enroll(warrior);
            Assert.True(arena.Warriors.Contains(warrior));
        }

        [Test]
        public void TestEnrollThrows()
        {
            var warrior = new Warrior("WNew", 100, 250);
            this.arena.Enroll(warrior);
            Assert.Throws<InvalidOperationException>(() => this.arena.Enroll(warrior));
        }

        [Test]
        public void TestFightWorksCorrectly()
        {
            Assert.DoesNotThrow(() => this.arena.Fight("W1", "W2"));
            Assert.DoesNotThrow(() => this.arena.Fight("W2", "W1"));
        }

        [Test]
        public void TestFightThrows()
        {
            Assert.Throws<InvalidOperationException>(() => this.arena.Fight("WNew", "W1"));
            Assert.Throws<InvalidOperationException>(() => this.arena.Fight("W1", "WNew"));
        }

        [Test]
        public void WarriorsPropTest()
        {

            Arena arena = new Arena();
            Assert.AreEqual(arena.Warriors.Count(), 0);
            Assert.AreEqual(arena.Count, 0);
            arena.Enroll(warrior);
            Assert.AreEqual(arena.Warriors.Count(), 1);
            Assert.AreEqual(arena.Count, 1);
        }

        [Test]
        public void EnrollMethodInvalidOperationThrows()
        {
            Arena arena = new Arena();
            arena.Enroll(warrior);
            Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior));
        }

        [Test]
        public void FightMethodValidWarriorsWorks()
        {
            Arena arena = new Arena();
            Warrior attacker = new Warrior("Thor", 50, 100);
            Warrior defender = new Warrior("Loki", 40, 80);

            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight("Thor", "Loki");

            Assert.AreEqual(attacker.HP, 60);
            Assert.AreEqual(defender.HP, 30);
        }

        [Test]
        public void FightMethodValidWarriorsThrows()
        {
            Arena arena = new Arena();
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(
            () => arena.Fight("gosho", "pesho"));
            Assert.Throws<InvalidOperationException>(
                () => arena.Fight("pesho", "gosho"));
        }
    }
}
