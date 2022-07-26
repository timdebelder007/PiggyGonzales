using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiggyGonzales.Console.Application;
using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application.Tests
{
    [TestClass()]
    public class GameFieldFactoryTests
    {
        [TestMethod()]
        public void CreateClosedFieldShouldNotBeOpen()
        {
            GameField field = GameFieldFactory.CreateClosedField(1, 1); 

            Assert.IsFalse(field.Open);
        }

        [TestMethod()]
        public void CreateClosedFieldShouldNotBeHidden()
        {
            GameField field = GameFieldFactory.CreateClosedField(1, 1);

            Assert.IsFalse(field.Hidden);
        }

        [TestMethod()]
        public void CreateClosedFieldShouldNotBeABomb()
        {
            GameField field = GameFieldFactory.CreateClosedField(1, 1);

            Assert.IsFalse(field.Bomb);
        }

        [TestMethod()]
        public void CreateOpenFieldShouldBeOpen()
        {
            GameField field = GameFieldFactory.CreateOpenField(1, 1);

            Assert.IsTrue(field.Open);
        }

        [TestMethod()]
        public void CreateOpenFieldShouldNotBeHidden()
        {
            GameField field = GameFieldFactory.CreateOpenField(1, 1);

            Assert.IsFalse(field.Hidden);
        }

        [TestMethod()]
        public void CreateOpenFieldShouldNotBeABomb()
        {
            GameField field = GameFieldFactory.CreateOpenField(1, 1);

            Assert.IsFalse(field.Bomb);
        }

        [TestMethod()]
        public void CreateHiddenFieldShouldBeOpen()
        {
            GameField field = GameFieldFactory.CreateHiddenField(1, 1);

            Assert.IsTrue(field.Open);
        }

        [TestMethod()]
        public void CreateHiddenFieldShouldBeHidden()
        {
            GameField field = GameFieldFactory.CreateHiddenField(1, 1);

            Assert.IsTrue(field.Hidden);
        }

        [TestMethod()]
        public void CreateHiddenFieldShouldNotBeABomb()
        {
            GameField field = GameFieldFactory.CreateHiddenField(1, 1);

            Assert.IsFalse(field.Bomb);
        }

        [TestMethod()]
        public void CreateHiddenBombFieldShouldBeOpen()
        {
            GameField field = GameFieldFactory.CreateHiddenBombField(1, 1);

            Assert.IsTrue(field.Open);
        }

        [TestMethod()]
        public void CreateHiddenBombFieldShouldBeHidden()
        {
            GameField field = GameFieldFactory.CreateHiddenBombField(1, 1);

            Assert.IsTrue(field.Hidden);
        }

        [TestMethod()]
        public void CreateHiddenBombFieldShouldNotBeABomb()
        {
            GameField field = GameFieldFactory.CreateHiddenBombField(1, 1);

            Assert.IsTrue(field.Bomb);
        }
    }
}