using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public static class Rules<T>
    {
        public static Rule<T> Predicate(Func<T, bool> predicate)
        {
            return new PredicateRule<T>(predicate);
        }
    }
}
