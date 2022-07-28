using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiggyGonzales.Console.Application;
using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Domain.Tests
{
    [TestClass()]
    public class MoveMasterPiggyTests
    {
        [TestMethod()]
        public void ExecuteMasterShouldWinOnBudget()
        {
            
            Piggy master = new Piggy(10, ESize.small);
            Piggy enemy = new Piggy(5, ESize.small);
            GameField field = new GameField(1, 1, false, false, true); 
            field.Piggy = enemy;
            //MovePiggy moveAction = new MovePiggy(master, field);

            //moveAction.Execute();

            Assert.AreEqual(15, master.Budget);
            Assert.AreEqual(field.Piggy, master);
        }

        [TestMethod()]
        public void ExecuteMasterMustDie()
        {

            Piggy master = new Piggy(2, ESize.small);
            Piggy enemy = new Piggy(5, ESize.small);
            GameField field = new GameField(1, 1, false, false, true);
            field.Piggy = enemy;
            //MoveMasterPiggy moveAction = new MoveMasterPiggy(master, field);


            //Assert.ThrowsException<PiggyDiedException>(() => moveAction.Execute());


            Assert.AreEqual(15, master.Budget);
            Assert.AreEqual(field.Piggy, master);
        }

    }
}