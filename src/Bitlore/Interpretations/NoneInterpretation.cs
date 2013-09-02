using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public class NoneInterpretation : Interpretation
    {
        internal NoneInterpretation() {}

        public string AsText()
        {
            return "none";
        }

        public string AsText(Func<string,string> formatter)
        {
            return formatter("none");
        }
    }
}
