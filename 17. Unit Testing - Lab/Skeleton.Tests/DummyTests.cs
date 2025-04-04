using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private int health = 10;
        private int experience = 5;
        private Dummy dummy;

        [SetUp]
        public void SetUp()
        {
            dummy = new Dummy(health, experience);
        }

        [Test]
        public void CreateDummy_InitializesCorrectly()
        {
            Assert.AreEqual(health, dummy.Health);
        }

        [Test]
        public void TakeAttack_ShouldLowerHealth()
        {
            dummy.TakeAttack(5);

            Assert.AreEqual(health - 5, dummy.Health);
        }

        [Test]
        public void MultipleTakeAttack_ShouldLowerHealth()
        {
            for (int i = 0; i < 5; i++)
            {
                dummy.TakeAttack(1);
            }

            Assert.AreEqual(health - 5, dummy.Health);
        }

        [Test]
        public void TakeAttack_WhenDeadShouldThrow()
        {
            dummy.TakeAttack(health);

            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.TakeAttack(1);
            });
        }

        [Test]
        public void DummyLosesHealthAfterAttacked()
        {
            var axe = new Axe(1, 10);
            var dummy = new Dummy(10, 10);

            axe.Attack(dummy);

            Assert.That(dummy.Health, Is.EqualTo(9), "Axe Durability doesn't change after attack.");
        }

        [Test]
        public void GiveExperience_ShouldWorkCorrectly()
        {
            dummy.TakeAttack(int.MaxValue);
            Assert.AreEqual(experience, dummy.GiveExperience());
        }

        [Test]
        public void GiveExperience_WhenDummyNotDead()
        {
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
        }

        [Test]
        public void IsDead_ShouldWork()
        {
            dummy.TakeAttack(int.MaxValue);
            Assert.AreEqual(true, dummy.IsDead());
        }
    }
}