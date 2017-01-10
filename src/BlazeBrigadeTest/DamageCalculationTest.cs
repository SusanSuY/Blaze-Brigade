using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Model;
using System;
using Model.UnitModule;
using Controller;

namespace ModelTest
{
    [TestClass]
    public class DamageCalculationTest
    {

        [TestMethod]
        public void getDamageDealt_ShouldReturnExpected()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();    // create mock object for enemyUnit

            // initialize unit 1 stats
            mockUnit.Setup(Unit => Unit.Str).Returns(10);
            mockUnit.Setup(Unit => Unit.Int).Returns(8);
            mockUnit.Setup(Unit => Unit.Def).Returns(6);
            mockUnit.Setup(Unit => Unit.Res).Returns(7);

            // initialize unit 2 stats
            mockEnemyUnit.Setup(Unit => Unit.Str).Returns(10);
            mockEnemyUnit.Setup(Unit => Unit.Int).Returns(8);
            mockEnemyUnit.Setup(Unit => Unit.Def).Returns(6);
            mockEnemyUnit.Setup(Unit => Unit.Res).Returns(7);

            // test physical damage
            Assert.AreEqual(4, DamageCalculations.getDamageDealt(mockUnit.Object, mockEnemyUnit.Object, false));
            Assert.AreEqual(4, DamageCalculations.getDamageDealt(mockEnemyUnit.Object, mockUnit.Object, false));

            // test magical damage
            Assert.AreEqual(1, DamageCalculations.getDamageDealt(mockUnit.Object, mockEnemyUnit.Object, true));
            Assert.AreEqual(1, DamageCalculations.getDamageDealt(mockEnemyUnit.Object, mockUnit.Object, true));
        }

        [TestMethod]
        public void getDamageDealt_NullUnits_ShouldThrowNullReferenceException()
        {
            // test both null
            try
            {
                DamageCalculations.getDamageDealt(null, null, true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getHitRate_ShouldReturnExpected()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();    // create mock object for enemyUnit

            // test for when units have same skill
            mockUnit.Setup(Unit => Unit.Skill).Returns(10);
            mockEnemyUnit.Setup(Unit => Unit.Skill).Returns(10);
            Assert.AreEqual(80, DamageCalculations.getHitRate(mockUnit.Object, mockEnemyUnit.Object));
            Assert.AreEqual(80, DamageCalculations.getHitRate(mockEnemyUnit.Object, mockUnit.Object));

            // test for when 1 unit has higher skill
            mockUnit.Setup(Unit => Unit.Skill).Returns(11);
            Assert.AreEqual(88, DamageCalculations.getHitRate(mockUnit.Object, mockEnemyUnit.Object));
            Assert.AreEqual(72, DamageCalculations.getHitRate(mockEnemyUnit.Object, mockUnit.Object));
        }

        [TestMethod]
        public void getHitRate_NullUnits_ShouldThrowNullReferenceException()
        {
            // test both null
            try
            {
                DamageCalculations.getHitRate(null, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getCritRate_ShouldReturnExpected()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();    // create mock object for enemyUnit

            // test for when units have same skill
            mockUnit.Setup(Unit => Unit.Skill).Returns(10);
            mockEnemyUnit.Setup(Unit => Unit.Skill).Returns(10);
            Assert.AreEqual(10, DamageCalculations.getCritRate(mockUnit.Object, mockEnemyUnit.Object));
            Assert.AreEqual(10, DamageCalculations.getCritRate(mockEnemyUnit.Object, mockUnit.Object));

            // test for when 1 unit has higher skill
            mockUnit.Setup(Unit => Unit.Skill).Returns(11);
            Assert.AreEqual(13, DamageCalculations.getCritRate(mockUnit.Object, mockEnemyUnit.Object));
            Assert.AreEqual(7, DamageCalculations.getCritRate(mockEnemyUnit.Object, mockUnit.Object));
        }

        [TestMethod]
        public void getCritRate_NullUnits_ShouldThrowNullReferenceException()
        {
            // test both null
            try
            {
                DamageCalculations.getCritRate(null, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getHitCount_ShouldReturn2()
        {
            Mock<Unit> attacker = new Mock<Unit>();
            Mock<Unit> defender = new Mock<Unit>();
            attacker.Setup(Unit => Unit.Speed).Returns(10);
            defender.Setup(Unit => Unit.Speed).Returns(1);
            Assert.AreEqual(2, DamageCalculations.getHitCount(attacker.Object, defender.Object));
        }

        [TestMethod]
        public void getHitCount_ShouldReturn1()
        {
            Mock<Unit> attacker = new Mock<Unit>();
            Mock<Unit> defender = new Mock<Unit>();
            attacker.Setup(Unit => Unit.Speed).Returns(10);
            defender.Setup(Unit => Unit.Speed).Returns(7);
            Assert.AreEqual(1, DamageCalculations.getHitCount(attacker.Object, defender.Object));
        }

        [TestMethod]
        public void getHitCount_NullUnits_ShouldThrowNullReferenceException()
        {
            // test both null
            try
            {
                DamageCalculations.getHitCount(null, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getFinalDamage_ShouldBeNonNull()
        {
            Mock<Unit> attacker = new Mock<Unit>();
            Mock<Unit> defender = new Mock<Unit>();
            attacker.Setup(Unit => Unit.Speed).Returns(10);
            defender.Setup(Unit => Unit.Speed).Returns(7);

            Assert.IsNotNull(DamageCalculations.finalDamage(attacker.Object, defender.Object, true));
            Assert.IsNotNull(DamageCalculations.finalDamage(attacker.Object, defender.Object, false));
        }

        [TestMethod]
        public void getFinalDamage_NullUnits_ShouldThrowNullReferenceException()
        {
            // test both null
            try
            {
                DamageCalculations.finalDamage(null, null, true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }
    }
}