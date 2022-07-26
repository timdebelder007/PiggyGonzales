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
    }
}