using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application
{
    public class GameFactory
    {
        public int BudgetMasterPiggy { get; }
        public int BoardSize { get; }

        private readonly List<GameField> gameFields;
        private readonly List<Piggy> enemyPiggies;

        public GameFactory(int budgetMasterPiggy, int boardSize)
        {
            if (budgetMasterPiggy < 1 || budgetMasterPiggy > 10) throw new ArgumentOutOfRangeException(nameof(budgetMasterPiggy), $"Budget must be between 1 and 10 (given value {budgetMasterPiggy})");
            if (boardSize < 10 || boardSize > 20) throw new ArgumentOutOfRangeException(nameof(boardSize), $"Board size must be between 10 and 20 (given value {boardSize})");

            BudgetMasterPiggy = budgetMasterPiggy;
            BoardSize = boardSize;
            this.gameFields = new List<GameField>();
            this.enemyPiggies = new List<Piggy>();
        }
    }
}
