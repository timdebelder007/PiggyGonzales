using PiggyGonzales.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Application;

public static class GameFieldFactory
{
    public static GameField CreateClosedField(int x, int y)
    {
        return new GameField(x, y, false, false, false);
    }

    public static GameField CreateOpenField(int x, int y)
    {
        return new GameField(x, y, false, false, true);
    }

    public static GameField CreateMaskedField(int x, int y)
    {
        return new GameField(x, y, true, false, true);
    }

    public static GameField CreateMaskedFieldWithBomb(int x, int y)
    {
        return new GameField(x, y, true, true, true);
    }
}
