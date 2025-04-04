namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class WarriorTests
    {
        private const int MIN_ATTACK_HP = 30;

        private string name = "Gosho";
        private int damage = 60;
        private int hp = 100;

        private Warrior warrior;

        [SetUp]
        public void SetUp()
        {
            warrior = new Warrior(name, damage, hp);
        }

        [Test]
        public void TestWarriorConstructor()
        {
            var newWarrior = new Warrior(name, damage, hp);
            Assert.IsNotNull(newWarrior);
        }

        [Test]
        public void TestWarriorGetters()
        {
            Assert.AreEqual(name, warrior.Name);
            Assert.AreEqual(damage, warrior.Damage);
            Assert.AreEqual(hp, warrior.HP);
        }

        [Test]
        public void TestWarriorGettersThrows()
        {
            string emptyString = string.Empty;
            string whitespaceString = " ";
            string nullString = null;
            int zero = 0;
            int negative = -1;

            Assert.Throws<ArgumentException>(() => _ = new Warrior(emptyString, damage, hp));
            Assert.Throws<ArgumentException>(() => _ = new Warrior(whitespaceString, damage, hp));
            Assert.Throws<ArgumentException>(() => _ = new Warrior(nullString, damage, hp));
            Assert.Throws<ArgumentException>(() => _ = new Warrior(name, zero, hp));
            Assert.Throws<ArgumentException>(() => _ = new Warrior(name, negative, hp));
            Assert.Throws<ArgumentException>(() => _ = new Warrior(name, damage, negative));
        }

        [Test]
        public void Attack_ShouldReduceHP_Correctly()
        {
            var attacker = new Warrior("Attacker", 30, 100);
            var defender = new Warrior("Defender", 20, 100);

            attacker.Attack(defender);

            Assert.AreEqual(70, defender.HP);
            Assert.AreEqual(80, attacker.HP);
        }

        [Test]
        public void Attack_ShouldSetHPToZero_WhenDamageExceedsHP()
        {
            var strongWarrior = new Warrior("Strong", 150, 100);
            var weakWarrior = new Warrior("Weak", 20, 50);

            strongWarrior.Attack(weakWarrior);

            Assert.AreEqual(0, weakWarrior.HP);
        }

        [Test]
        public void Attack_ShouldThrowException_WhenHPBelowMinimum()
        {
            var lowHPWarrior = new Warrior("LowHP", 10, 25);
            var opponent = new Warrior("Opponent", 20, 100);

            Assert.Throws<InvalidOperationException>(() => lowHPWarrior.Attack(opponent));
            Assert.Throws<InvalidOperationException>(() => opponent.Attack(lowHPWarrior));
        }

        [Test]
        public void Attack_ShouldThrowException_WhenFighterNotAllowedToAttack()
        {
            var maxDamageWarrior = new Warrior("MaxDamage", int.MaxValue, 100);
            var normalWarrior = new Warrior("Normal", 50, 100);

            Assert.Throws<InvalidOperationException>(() => normalWarrior.Attack(maxDamageWarrior));
        }

        [Test]
        public void CtorValidDataCreateInstance()
        {
            Warrior warrior = new Warrior(this.name, this.damage, this.hp);
            Assert.AreEqual(name, warrior.Name);
            Assert.AreEqual(damage, warrior.Damage);
            Assert.AreEqual(hp, warrior.HP);
        }

        [Test]
        public void CtorInvalidDataThrows()
        {
            Assert.Throws<ArgumentException>(
                () => new Warrior("", 23, 32));
            Assert.Throws<ArgumentException>(
                () => new Warrior(" ", 23, 32));
            Assert.Throws<ArgumentException>(
                () => new Warrior(null, 23, 32));
            Assert.Throws<ArgumentException>(
                () => new Warrior("asd", -2, 32));
            Assert.Throws<ArgumentException>(
                () => new Warrior("asd", 0, 32));
            Assert.Throws<ArgumentException>(
                () => new Warrior("asd", 23, -32));
        }

        [Test]
        public void AttackMethodInvalidOperation_Throws()
        {

            Warrior lowHpWarrior = new Warrior(name, damage, 20);
            Warrior warrior = new Warrior(name, damage, 50);
            Assert.Throws<InvalidOperationException>(() => lowHpWarrior.Attack(warrior));
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(lowHpWarrior));

            Warrior newWarrior = new Warrior(name, damage, 40);
            Assert.Throws<InvalidOperationException>(() => newWarrior.Attack(warrior));
        }

        [Test]
        public void AttackMethodValidWarriors_WorksFine()
        {
            Warrior warrior1 = new Warrior(name, damage, hp);
            Warrior warrior2 = new Warrior(name, damage, hp);

            warrior1.Attack(warrior2);
            Assert.AreEqual(warrior1.HP, 40);
            Assert.AreEqual(warrior2.HP, 40);

            Warrior warrior3 = new Warrior(name, damage, hp);
            Warrior warrior4 = new Warrior(name, damage, 50);

            warrior3.Attack(warrior4);
            Assert.AreEqual(warrior4.HP, 0);
            Assert.AreEqual(warrior3.HP, 40);
        }
    }
}