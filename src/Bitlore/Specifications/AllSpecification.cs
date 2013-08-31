using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public class AllSpecification<T> : Rule<T>
    {
        IEnumerable<KeyValuePair<Rule<T>, Func<Rule<T>, Interpretation>>> _rulesWithFailureInterpretions;
        
        public AllSpecification(IEnumerable<KeyValuePair<Rule<T>,Func<Rule<T>,Interpretation>>> rulesWithFailureInterpretions)
        {
            if (rulesWithFailureInterpretions == null)
                throw new ArgumentNullException("rulesWithFailureInterpretions");
            _rulesWithFailureInterpretions = rulesWithFailureInterpretions;
        }

        bool Rule<T>.Evaluate(T item)
        {
            return !Test(item).ToArray().Any();
        }

        public IEnumerable<Interpretation> Test(T item)
        {
            return 
                from r in _rulesWithFailureInterpretions
                where !r.Key.Evaluate(item) 
                select r.Value(r.Key);
        }
    }
}
