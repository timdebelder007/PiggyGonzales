using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console
{
    public class PiggyDiedException : Exception
    {
        public PiggyDiedException(string message) : base(message)
        { }

    }
}
