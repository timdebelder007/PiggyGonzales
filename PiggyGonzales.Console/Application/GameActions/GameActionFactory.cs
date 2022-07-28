using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application.GameActions
{
    public class GameActionFactory
    {        

        public GameActionFactory()
        {            
        }       

        public GameAction CreateSpawnMasterPiggyGameAction(Game currentGame)
        {
            //remove master form current position (in case of respawn)
            GameField currentPosition = GetCurrentPositionOfMasterPiggy(currentGame);
            if (currentPosition != null)
                currentPosition.Piggy = null;           

            System.Console.WriteLine($"Master piggy spawned: {currentGame.masterPiggy.Size} & {currentGame.masterPiggy.Budget}");

            return new SpawnPiggy(currentGame.masterPiggy, GetRandomFreeSpot(currentGame));
        }

        public GameAction CreateSpawnEnemyPiggyGameAction(Game currentGame)
        {
            Piggy newEnemyPiggy = PiggyFactory.CreateEnemyPiggy();
            currentGame.enemyPiggies.Add(newEnemyPiggy);

            System.Console.WriteLine($"enemy piggy spawned: {newEnemyPiggy.Size} & {newEnemyPiggy.Budget}");

            return new SpawnPiggy(newEnemyPiggy, GetRandomFreeSpot(currentGame));                        
        }

        public GameAction CreateMoveMasterPiggyGameAction(Game currentGame)
        {
            GameField currentPosition = GetCurrentPositionOfMasterPiggy(currentGame) ?? throw new ArgumentNullException("no master piggy to move");
            GameField goToPosition = GetRandomMoveSpot(currentGame, currentPosition);
            return new MovePiggy(currentPosition, goToPosition);
        }

        private GameField GetRandomFreeSpot(Game currentGame)
        {
            List<GameField> freeSpots = currentGame.gameFields.Where(gf => gf.Open && gf.Piggy == null).ToList();

            if (freeSpots.Count == 0) throw new ArgumentOutOfRangeException(nameof(freeSpots));

            return freeSpots.ElementAt(Random.Shared.Next(freeSpots.Count));
        }

        public GameField GetRandomMoveSpot(Game currentGame, GameField gf)
        {
            List<GameField> result = new List<GameField>();

            var LeftField = currentGame.gameFields.FirstOrDefault(left => left.X == gf.X && left.Y == gf.Y - 1 && left.Open);
            var RightField = currentGame.gameFields.FirstOrDefault(right => right.X == gf.X && right.Y == gf.Y + 1 && right.Open);
            var UpField = currentGame.gameFields.FirstOrDefault(up => up.X == gf.X - 1 && up.Y == gf.Y && up.Open);
            var BottomField = currentGame.gameFields.FirstOrDefault(bottom => bottom.X == gf.X + 1 && bottom.Y == gf.Y && bottom.Open);

            if (LeftField != null) result.Add(LeftField);
            if (RightField != null) result.Add(RightField);
            if (UpField != null) result.Add(UpField);
            if (BottomField != null) result.Add(BottomField);

            if (result.Count == 0) throw new ArgumentOutOfRangeException(nameof(result));

            return result.ElementAt(Random.Shared.Next(result.Count));
        }

        public GameField? GetCurrentPositionOfMasterPiggy(Game currentGame)
        {
            return currentGame.gameFields.FirstOrDefault(gf => gf.Piggy != null && gf.Piggy.Equals(currentGame.masterPiggy));
        }

    }
}
