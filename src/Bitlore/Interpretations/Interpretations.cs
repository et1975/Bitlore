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
    }
}
