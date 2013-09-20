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

        public static Rule<T> Any(IEnumerable<Rule<T>> rules)
        {
            return new AnyRule<T>(rules);
        }

        public static Rule<T> All(IEnumerable<KeyValuePair<Rule<T>, Func<Rule<T>, Interpretation>>> rulesWithFailureInterpreations)
        {
            return new AllSpecification<T>(rulesWithFailureInterpreations);
        }

    }
}
