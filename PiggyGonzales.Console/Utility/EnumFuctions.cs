using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyGonzales.Console.Utility
{
    public class EnumFuctions<T> where T : struct, IConvertible
    {
        public static int Count 
        { 
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetNames(typeof(T)).Length;                
            } 
        }

    }
}
