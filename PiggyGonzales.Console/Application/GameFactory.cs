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
        private Piggy? masterPiggy;
        private bool generated;

        private int IntervalMoveMasterPiggy;
        private int IntervalSpawnEnemyPiggy; 

        public GameFactory(int budgetMasterPiggy, int boardSize)
        {
            if (budgetMasterPiggy < 1 || budgetMasterPiggy > 10) throw new ArgumentOutOfRangeException(nameof(budgetMasterPiggy), $"Budget must be between 1 and 10 (given value {budgetMasterPiggy})");
            if (boardSize < 10 || boardSize > 20) throw new ArgumentOutOfRangeException(nameof(boardSize), $"Board size must be between 10 and 20 (given value {boardSize})");

            BudgetMasterPiggy = budgetMasterPiggy;
            BoardSize = boardSize;

            this.IntervalMoveMasterPiggy = 2;
            this.IntervalSpawnEnemyPiggy = 5;

            this.gameFields = new List<GameField>();
            this.enemyPiggies = new List<Piggy>();            
        }

        public void GenerateGame()
        {
            if (generated) return;

            decimal TotalFieldCount = (decimal)(BoardSize * BoardSize);

            int closedFieldCount = (int)Math.Round(TotalFieldCount / 100 *10, 0, MidpointRounding.ToZero);
            int HiddenFieldCount = (int)Math.Round(TotalFieldCount/100*10, 0, MidpointRounding.ToZero);
            int BombFieldCount = (int)Math.Round((decimal)HiddenFieldCount / 100 * 50, 0, MidpointRounding.ToZero);
            HiddenFieldCount -= BombFieldCount;            

            while(gameFields.Where(gf => !gf.Open).Count() < closedFieldCount)
            {
                GameField newField = GameFieldFactory.CreateClosedField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X || gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }

            while (gameFields.Where(gf => gf.Open && gf.Hidden && !gf.Bomb).Count() < HiddenFieldCount)
            {
                GameField newField = GameFieldFactory.CreateHiddenField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X || gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }

            while (gameFields.Where(gf => gf.Open && gf.Hidden && gf.Bomb).Count() < BombFieldCount)
            {
                GameField newField = GameFieldFactory.CreateHiddenBombField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X || gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }

            while (gameFields.Count() < TotalFieldCount)
            {
                GameField newField = GameFieldFactory.CreateOpenField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X || gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }           

            generated = true;
        }

        public void Play()
        {
            GenerateGame();
            masterPiggy = PiggyFactory.CreateMasterPiggy(BudgetMasterPiggy);
        }

        public void RePlay()
        {

        }
    }
}
