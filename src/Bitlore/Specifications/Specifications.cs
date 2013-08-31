using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public static class Specifications<T>
    {
        public static Rule<T> All(IEnumerable<KeyValuePair<Rule<T>, Func<Rule<T>, Interpretation>>> rulesWithFailureInterpreations)
        {
            return new AllSpecification<T>(rulesWithFailureInterpreations);
        }
    }
}
