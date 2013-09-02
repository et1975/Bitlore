using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public sealed class AnyRule<T> : Rule<T>
    {
        public IEnumerable<Rule<T>> Rules
        {
            get;
            private set;
        }
        
        public AnyRule(IEnumerable<Rule<T>> rules)
        {
            if (rules == null)
                throw new ArgumentNullException("rules");
            Rules = rules;
        }

        public bool Evaluate(T item)
        {
            return Rules.Any( r => r.Evaluate(item));
        }
    }
}
