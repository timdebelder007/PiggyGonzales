using PiggyGonzales.Console.Domain;
using PiggyGonzales.Console.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application;

public static class PiggyFactory
{
    public static readonly List<int> availableEnemyBudgets = new() { 1,2,5 };

    public static Piggy CreateMasterPiggy(int startBudget)
    {
        return new Piggy(startBudget, EnumFuctions<ESize>.GetRandomValue());
    }

    public static Piggy CreateEnemyPiggy()
    {        
        return new Piggy(availableEnemyBudgets.ElementAt(Random.Shared.Next(availableEnemyBudgets.Count())), EnumFuctions<ESize>.GetRandomValue());
    }

}
