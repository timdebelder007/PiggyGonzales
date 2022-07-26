using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application;

public static class PiggyFactory
{
    private static readonly List<double> availableEnemyBudgets = new() { 1,2,5 };

    public static Piggy CreateMasterPiggy(double startBudget)
    {
        throw new NotImplementedException();
    }

    public static Piggy CreateEnemyPiggy()
    {
        double budget = availableEnemyBudgets.ElementAt(Random.Shared.Next(availableEnemyBudgets.Count()));

        throw new NotImplementedException();
    }

}
