using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public static class Specifications
    {
        public static AllSpecification<T> All<T>(IEnumerable<KeyValuePair<Rule<T>, Func<Rule<T>, Interpretation>>> rulesWithFailureInterpreations)
        {
            return new AllSpecification<T>(rulesWithFailureInterpreations);
        }

        public static ActionSpecification<T> Action<T>(IEnumerable<Binding<T>> bindings, bool stopOnFailure = false, bool stopOnSuccess = false)
        {
            return new ActionSpecification<T>(bindings, stopOnFailure, stopOnSuccess);
        }
    }
}
