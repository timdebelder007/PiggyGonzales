using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application.GameActions
{
    public abstract class GameAction
    {
        protected GameAction? PreviousAction { get; set; }       
       
        public GameAction()
        {            
        }

        public abstract GameAction Execute();

        public void ExecuteRecursive()
        {
            if (PreviousAction != null)
                PreviousAction.ExecuteRecursive();
            Execute();
        }



    }
}
