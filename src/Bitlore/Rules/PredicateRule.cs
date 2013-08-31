using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public class PredicateRule<T> : Rule<T>
    {
        Func<T, bool> _predicate;

        public PredicateRule(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            _predicate = predicate;
        }

        public bool Evaluate(T item)
        {
            return _predicate(item);
        }
    }
}
