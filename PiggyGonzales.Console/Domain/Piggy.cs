using PiggyGonzales.Console.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Domain;

public class Piggy
{
    public int InitialBudget { get; }

    [Range(1,10)]
    public int Budget { get; private set; }
    public ESize Size { get; }

    public Piggy(int budget, ESize size)
    {
        InitialBudget = budget;
        Budget = budget;
        Size = size;
    }

    public void ResetBudget()
    {
        Budget = InitialBudget;
    }

    public void AddToBudget(int money)
    {
        Budget += money;
    }
}
