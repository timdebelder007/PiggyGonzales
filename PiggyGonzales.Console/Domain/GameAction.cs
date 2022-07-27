using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Domain
{
    public class GameAction
    {
        public EGameActionType ActionType { get; }
        public Piggy Piggy { get; }
        public GameField GameField { get; }
        public GameAction? PreviousAction { get; }

        public GameAction(EGameActionType actionType, Piggy piggy, GameField gameField, GameAction? previousAction = null)
        {
            PreviousAction = previousAction;
            Piggy = piggy;
            GameField = gameField;
            ActionType = ActionType;
        }        
    }
}
