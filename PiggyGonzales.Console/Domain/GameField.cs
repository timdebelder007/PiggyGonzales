using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Domain;

public class GameField
{
    public int X { get;  }
    public int Y { get;  }
    public bool Open { get; }
    public bool Hidden { get;  }
    public bool Bomb { get; }   

    public GameField(int x, int y, bool hidden, bool bomb, bool open)
    {
        X = x;
        Y = y;
        Hidden = hidden;
        Bomb = bomb;
        Open = open;
    }
}
