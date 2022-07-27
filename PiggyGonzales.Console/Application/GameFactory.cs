using PiggyGonzales.Console.Domain;
using System.Timers;
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
        private GameAction? lastAction;
        public int Lives { get; }

        private int currentSecond;

        private int IntervalMoveMasterPiggy;
        private int IntervalSpawnEnemyPiggy; 

        public GameFactory(int budgetMasterPiggy, int boardSize, int lives = 3)
        {
            if (budgetMasterPiggy < 1 || budgetMasterPiggy > 10) throw new ArgumentOutOfRangeException(nameof(budgetMasterPiggy), $"Budget must be between 1 and 10 (given value {budgetMasterPiggy})");
            if (boardSize < 10 || boardSize > 20) throw new ArgumentOutOfRangeException(nameof(boardSize), $"Board size must be between 10 and 20 (given value {boardSize})");

            BudgetMasterPiggy = budgetMasterPiggy;
            BoardSize = boardSize;

            this.IntervalMoveMasterPiggy = 2;
            this.IntervalSpawnEnemyPiggy = 5;

            this.gameFields = new List<GameField>();
            this.enemyPiggies = new List<Piggy>();

            this.Lives = lives;
        }

        private void GenerateGame()
        {                       
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
        }

        System.Timers.Timer GameTimer = new System.Timers.Timer(1000);

        public void Play()
        {
            GenerateGame();
            masterPiggy = PiggyFactory.CreateMasterPiggy(BudgetMasterPiggy);
            currentSecond = 0;
           
            GameTimer.Elapsed += (sender, e) => Handler();
            GameTimer.Start();
            System.Console.ReadLine();
            
            
        }

        public void RePlay()
        {

        }

        void Handler()
        {
            try
            {
                //spawnMasterPiggy
                if (currentSecond == 0)
                {                    
                    GameField gameFieldToSpawn = GetRandomFreeSpot();
                    lastAction = new GameAction(EGameActionType.SpawnEnemyPiggy, masterPiggy, gameFieldToSpawn, lastAction);
                    System.Console.WriteLine($"master piggy spawned: {masterPiggy.Size} & {masterPiggy.Budget}");
                }

                currentSecond++;

                if (currentSecond % IntervalSpawnEnemyPiggy == 0)
                {
                    Piggy newEnemyPiggy = PiggyFactory.CreateEnemyPiggy();
                    enemyPiggies.Add(newEnemyPiggy);
                    GameField gameFieldToSpawn = GetRandomFreeSpot();
                    lastAction = new GameAction(EGameActionType.SpawnEnemyPiggy, newEnemyPiggy, gameFieldToSpawn, lastAction);
                    System.Console.WriteLine($"enemy piggy spawned: {newEnemyPiggy.Size} & {newEnemyPiggy.Budget}");                    
                }

                if (currentSecond % IntervalMoveMasterPiggy == 0)
                {

                }
            }
            catch (Exception)
            {
                GameTimer.Stop();                
            }         
        }


        private void MoveMasterPiggy(GameAction action)
        {

        }

        private void SpawnEnemyPigy(GameAction action)
        {     
            
        }

        private GameField GetRandomFreeSpot()
        {
            List<GameField> freeSpots = gameFields.Where(gf => gf.Open && gf.Piggy == null).ToList();

            if (freeSpots.Count == 0) throw new ArgumentOutOfRangeException(nameof(freeSpots));

            return freeSpots.ElementAt(Random.Shared.Next(freeSpots.Count));
        }

        private List<GameField> GetCloseSpots(GameField gf)
        {
            List<GameField> result = new List<GameField>();

            var LeftField = gameFields.FirstOrDefault(left => left.X == gf.X && left.Y == gf.Y - 1 && left.Open);
            var RightField = gameFields.FirstOrDefault(right => right.X == gf.X && right.Y == gf.Y + 1 && right.Open);
            var UpField = gameFields.FirstOrDefault(up => up.X == gf.X - 1 && up.Y == gf.Y && up.Open);
            var BottomField = gameFields.FirstOrDefault(bottom => bottom.X == gf.X + 1 && bottom.Y == gf.Y && bottom.Open);

            if (LeftField != null) result.Add(LeftField);
            if (RightField != null) result.Add(RightField);
            if (UpField != null) result.Add(UpField);
            if (BottomField != null) result.Add(BottomField);

            return result;





        }
    }
}
