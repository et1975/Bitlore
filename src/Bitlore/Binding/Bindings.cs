using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public static class Bindings
    {
        public static Binding<T> Bind<T>(this Rule<T> rule,
            Func<Rule<T>, Interpretation> failureInterpretation,
            Action<Rule<T>, Interpretation> onFailure,
            Func<Rule<T>, Interpretation> successInterpretation,
            Action<Rule<T>, Interpretation> onSuccess)
        {
            return new Binding<T>(rule, failureInterpretation, onFailure, successInterpretation, onSuccess);
        }

        public static Binding<T> Bind<T>(this Rule<T> rule,
            Func<Rule<T>, Interpretation> failureInterpretation,
            Action<Rule<T>, Interpretation> onFailure)
        {
            return new Binding<T>(rule, failureInterpretation, onFailure, _ => Interpretations.None, (_, __) => { });
        }

        public static Binding<T> Bind<T>(this Rule<T> rule,
            Action<Rule<T>, Interpretation> onFailure)
        {
            return new Binding<T>(rule, _ => Interpretations.None, onFailure, _ => Interpretations.None, (_, __) => { });
        }
    }
}
