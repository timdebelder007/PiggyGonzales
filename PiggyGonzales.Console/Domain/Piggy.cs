using PiggyGonzales.Console.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Domain;

public class Piggy
{
    public double Budget { get; }
    public ESize Size { get; }

    public Piggy(double budget, ESize size)
    {
        Budget = budget;
        Size = size;
    }
}
