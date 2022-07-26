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
    [Range(1,10)]
    public int Budget { get; }
    public ESize Size { get; }

    public Piggy(int budget, ESize size)
    {        
        Budget = budget;
        Size = size;
    }
}
