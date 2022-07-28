using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application.GameActions
{
    public class SpawnPiggy : GameAction
    {
        protected Piggy ActionPiggy { get; }
        protected GameField ActionGameField { get; }

        public SpawnPiggy(Piggy actionPiggy, GameField actionGameField) : base()
        {
            ActionPiggy = actionPiggy;
            ActionGameField = actionGameField;
        }

        public override GameAction Execute()
        {            
            ActionGameField.Piggy = ActionPiggy;

            System.Console.WriteLine($"Piggy spawned: {ActionPiggy.Size} & {ActionPiggy.Budget}");

            return this;
        }
    }
}
