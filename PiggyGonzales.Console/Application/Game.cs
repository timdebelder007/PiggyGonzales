using PiggyGonzales.Console.Domain;
using PiggyGonzales.Console.Application.GameActions;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application
{
    public class Game
    {
        public int BudgetMasterPiggy { get; }
        public int BoardSize { get; }

        internal List<GameField> gameFields { get; }
        internal List<Piggy> enemyPiggies { get; }
        internal Piggy masterPiggy { get; set; }
        internal GameAction? lastAction { get; private set; }
        public int Lives { get; private set; }

        private readonly GameActionFactory gameActionFactory;

        private int currentSecond;
        private bool boardGenerated;

        private int IntervalMoveMasterPiggy;
        private int IntervalSpawnEnemyPiggy; 

        public Game(int budgetMasterPiggy, int boardSize, int lives = 3)
        {
            if (budgetMasterPiggy < 1 || budgetMasterPiggy > 10) throw new ArgumentOutOfRangeException(nameof(budgetMasterPiggy), $"Budget must be between 1 and 10 (given value {budgetMasterPiggy})");
            if (boardSize < 10 || boardSize > 20) throw new ArgumentOutOfRangeException(nameof(boardSize), $"Board size must be between 10 and 20 (given value {boardSize})");

            BudgetMasterPiggy = budgetMasterPiggy;
            BoardSize = boardSize;
            masterPiggy = PiggyFactory.CreateMasterPiggy(budgetMasterPiggy);

            this.IntervalMoveMasterPiggy = 2;
            this.IntervalSpawnEnemyPiggy = 5;

            this.gameFields = new List<GameField>();
            this.enemyPiggies = new List<Piggy>();

            this.Lives = lives;
            this.boardGenerated = false;

            this.gameActionFactory = new GameActionFactory();            
        }


        //must be seperated
        public void GenerateGame()
        {
            if (boardGenerated) return;

            decimal TotalFieldCount = (decimal)(BoardSize * BoardSize);

            int closedFieldCount = (int)Math.Round(TotalFieldCount / 100 *10, 0, MidpointRounding.ToZero);
            int HiddenFieldCount = (int)Math.Round(TotalFieldCount/100*10, 0, MidpointRounding.ToZero);
            int BombFieldCount = (int)Math.Round((decimal)HiddenFieldCount / 100 * 50, 0, MidpointRounding.ToZero);
            HiddenFieldCount -= BombFieldCount;            

            while(gameFields.Where(gf => !gf.Open).Count() < closedFieldCount)
            {
                GameField newField = GameFieldFactory.CreateClosedField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X && gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }            

            while (gameFields.Where(gf => gf.Open && gf.Hidden && !gf.Bomb).Count() < HiddenFieldCount)
            {
                GameField newField = GameFieldFactory.CreateHiddenField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X && gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }

            while (gameFields.Where(gf => gf.Open && gf.Hidden && gf.Bomb).Count() < BombFieldCount)
            {
                GameField newField = GameFieldFactory.CreateHiddenBombField(Random.Shared.Next(1, BoardSize), Random.Shared.Next(1, BoardSize));
                if (!gameFields.Where(gf => gf.X == newField.X && gf.Y == newField.Y).Any())
                    gameFields.Add(newField);
            }

            while (gameFields.Count() < TotalFieldCount)
            {
                for(int x = 1; x <= BoardSize; x++)
                {
                    for(int y = 1; y <= BoardSize; y++)
                    {
                        if (!gameFields.Where(gf => gf.X == x && gf.Y == y).Any())
                            gameFields.Add(GameFieldFactory.CreateOpenField(x, y));
                    }
                }
            }

            boardGenerated = true;
        }
        

        public void Play()
        {
            GenerateGame();

            currentSecond = 0;

            var query = this.gameFields.Where(gf => gf.Open && !gf.Hidden && gf.Piggy == null);
            try
            {
                while (Lives > 0 && query.Any())
                {
                    try
                    {
                        //spawnMasterPiggy
                        if (currentSecond == 0)
                        {
                            lastAction = gameActionFactory.CreateSpawnMasterPiggyGameAction(this).Execute();
                        }

                        currentSecond++;

                        //spawn new enemy
                        if (currentSecond % IntervalSpawnEnemyPiggy == 0)
                        {
                            lastAction = gameActionFactory.CreateSpawnEnemyPiggyGameAction(this).Execute();
                        }

                        if (currentSecond % IntervalMoveMasterPiggy == 0)
                        {
                            lastAction = gameActionFactory.CreateMoveMasterPiggyGameAction(this).Execute();
                        }
                    }
                    catch (PiggyDiedException)
                    {
                        Lives -= 1;
                        if (Lives > 0)
                            lastAction = gameActionFactory.CreateSpawnMasterPiggyGameAction(this).Execute();

                        System.Console.WriteLine($"died remaining lives: {Lives}");
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Game ended because: {ex.Message}");
            }
            
           
            System.Console.ReadLine();

        }

        public void RePlay()
        {
            lastAction?.ExecuteRecursive();
        }        
    }
}
