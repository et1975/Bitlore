using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public class Binding<T>
    {
        public Binding(Rule<T> rule,
            Func<Rule<T>, Interpretation> failureInterpretation,
            Action<Rule<T>, Interpretation> onFailure,
            Func<Rule<T>, Interpretation> successInterpretation,
            Action<Rule<T>, Interpretation> onSuccess)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");
            if (failureInterpretation == null)
                throw new ArgumentNullException("failureInterpretation");
            if (onFailure == null)
                throw new ArgumentNullException("onFailure");
            if (successInterpretation == null)
                throw new ArgumentNullException("successInterpretation");
            if (onSuccess == null)
                throw new ArgumentNullException("onSuccess");

            Rule = rule;
            SuccessInterpretation = successInterpretation;
            FailureInterpretation = failureInterpretation;
            OnFailure = onFailure;
            OnSuccess = onSuccess;
        }

        public Rule<T> Rule { get; private set; }
        public Func<Rule<T>, Interpretation> SuccessInterpretation { get; private set; }
        public Func<Rule<T>, Interpretation> FailureInterpretation { get; private set; }
        public Action<Rule<T>, Interpretation> OnSuccess { get; private set; }
        public Action<Rule<T>, Interpretation> OnFailure { get; private set; }
    }
}
