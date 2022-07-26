using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Domain
{
    public class GameAction
    {
        private readonly Piggy masterPiggy;
        private readonly GameField gameField;
        private readonly GameAction PreviousAction;       

        public GameAction(GameAction previousAction, Piggy masterPiggy, GameField gameField)
        {
            PreviousAction = previousAction;
            this.masterPiggy = masterPiggy;
            this.gameField = gameField;
        }        
    }
}
