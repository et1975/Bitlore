using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public sealed class AllSpecification<T> : Rule<T>,Interpretation
    {
        public IEnumerable<KeyValuePair<Rule<T>, Func<Rule<T>, Interpretation>>> RulesWithFailureInterpretions
        {
            get;
            private set;
        }

        Interpretation Failures { get; set; }
        
        public AllSpecification(IEnumerable<KeyValuePair<Rule<T>,Func<Rule<T>,Interpretation>>> rulesWithFailureInterpretions)
        {
            if (rulesWithFailureInterpretions == null)
                throw new ArgumentNullException("rulesWithFailureInterpretions");
            RulesWithFailureInterpretions = rulesWithFailureInterpretions;
        }

        bool Rule<T>.Evaluate(T item)
        {
            var failures = Test(item).ToArray();
            Failures = new CompositeInterpretation(failures);
            return !failures.Any();
        }

        public IEnumerable<Interpretation> Test(T item)
        {
            return 
                from r in RulesWithFailureInterpretions
                where !r.Key.Evaluate(item) 
                select r.Value(r.Key);
        }

        string Interpretation.AsText()
        {
            return Failures.AsText();
        }

        string Interpretation.AsText(Func<string, string> formatter)
        {
            return Failures.AsText(formatter);
        }
    }
}
