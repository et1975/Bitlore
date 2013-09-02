using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public static class Interpretations
    {
        public static Interpretation Static(string text)
        {
            return new StaticInterpretation(text);
        }

        public static Interpretation Formatted(string text, params object[] args)
        {
            return new FormattedInterpretation(text,args);
        }

        public static Interpretation Composite(IEnumerable<Interpretation> interpretations)
        {
            return new CompositeInterpretation(interpretations);
        }

        public static Interpretation Composite(IEnumerable<Interpretation> interpretations, string newLine)
        {
            return new CompositeInterpretation(interpretations, newLine);
        }


        static readonly Interpretation _none = new NoneInterpretation();
        public static Interpretation None { get { return _none; } }
    }
}
