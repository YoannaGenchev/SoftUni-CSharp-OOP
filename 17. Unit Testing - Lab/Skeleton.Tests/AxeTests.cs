using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private int attack = 2;
        private int durability = 10;
        private Dummy dummy;

        [SetUp]
        public void SetUp()
        {
            axe = new Axe(attack, durability);
            dummy = new Dummy(int.MaxValue, 10);
        }

        [Test]
        public void AxeCreation_ShouldSetPropertiesCorrectly()
        {
            Assert.AreEqual(attack, axe.AttackPoints);
            Assert.AreEqual(durability, axe.DurabilityPoints);
        }

        [Test]
        public void AxeLoosesDurabilityAfterAttack()
        {
            axe.Attack(dummy);
            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesn't change after attack.");
        }

        [Test]
        public void AxeLoosesDurabilityAfterMultipleAttacks()
        {
            var axeDurability = axe.DurabilityPoints;

            for (int i = 0; i < 5; i++)
            {
                axe.Attack(dummy);
            }

            Assert.AreEqual(axeDurability - 5, axe.DurabilityPoints);
        }

        [Test]
        public void Attack_WhenDurabilityIs0_ShouldThrow()
        {
            for (int i = 0; i < durability; i++)
            {
                axe.Attack(dummy);
            }

            Assert.Throws<InvalidOperationException>(() =>
            {
                axe.Attack(dummy);
            });
        }
    }
}