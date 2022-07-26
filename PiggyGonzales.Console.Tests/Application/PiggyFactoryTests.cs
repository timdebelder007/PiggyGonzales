using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiggyGonzales.Console.Application;
using PiggyGonzales.Console.Domain;
using PiggyGonzales.Console.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application.Tests
{
    [TestClass()]
    public class PiggyFactoryTests
    {
        [TestMethod()]
        public void CreateMasterPiggyShouldHaveGivenBudget()
        {
            Piggy master = PiggyFactory.CreateMasterPiggy(10);

            Assert.AreEqual(10, master.Budget);
        }

        [TestMethod()]
        public void CreateMasterPiggyShouldInvalidateBudget()
        {           
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => PiggyFactory.CreateMasterPiggy(11));            
        }

        [TestMethod()]
        public void CreateEnemyPiggyShouldHaveBudgetInRange()
        {
            Piggy enemyPiggy = PiggyFactory.CreateEnemyPiggy();

            Assert.IsTrue(PiggyFactory.availableEnemyBudgets.Contains(enemyPiggy.Budget));
        }

        [TestMethod()]
        [DataRow(10, 10)]
        [DataRow(11, 12)]
        [DataRow(12, 14)]
        [DataRow(13, 16)]
        [DataRow(14, 19)]
        [DataRow(15, 22)]
        [DataRow(16, 25)]
        [DataRow(17, 28)]
        [DataRow(18, 32)]
        [DataRow(19, 36)]
        [DataRow(20, 40)]
        public void MyTests(int input, int expectedResult)
        {
            decimal value = (decimal)(input * input) / 100 * 10;

            Assert.AreEqual(expectedResult, (int)Math.Round(value, 0, MidpointRounding.ToZero));
        }


    }
}