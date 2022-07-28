using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application.GameActions
{
    public class MovePiggy : GameAction
    {        
        protected GameField ActionFromGameField { get; set; }
        protected GameField ActionToGameField { get; set; }

        public MovePiggy(GameField actionFromGameField, GameField actionToGameField) : base()
        {
            ActionFromGameField = actionFromGameField;
            ActionToGameField = actionToGameField;
        }

        public override GameAction Execute()
        {            
            System.Console.WriteLine($"Field move to {ActionToGameField.X} & {ActionToGameField.Y}");

            if (ActionToGameField.Piggy != null && 
                ActionFromGameField.Piggy != null && 
                !ActionToGameField.Piggy.Equals(ActionFromGameField.Piggy))
            {
                System.Console.WriteLine($"Fight: ");
                System.Console.WriteLine($"  Master: {ActionFromGameField.Piggy.Size} - {ActionFromGameField.Piggy.Size}");
                System.Console.WriteLine($"  Enemy: {ActionToGameField.Piggy.Size} - {ActionToGameField.Piggy.Budget}");

                if (ActionFromGameField.Piggy.Size < ActionToGameField.Piggy.Size)
                    throw new PiggyDiedException("Master Piggy died because he was smaller!");
                if (ActionFromGameField.Piggy.Size == ActionToGameField.Piggy.Size && 
                    ActionFromGameField.Piggy.Budget < ActionToGameField.Piggy.Budget)
                    throw new PiggyDiedException("Master Piggy died because he was equal in size but had a smaller budget!");

                ActionFromGameField.Piggy.AddToBudget(ActionToGameField.Piggy.Budget);                
            }
            
            ActionToGameField.Piggy = ActionFromGameField.Piggy;
            ActionFromGameField.Piggy = null;

            return this;
        }


    }
}
