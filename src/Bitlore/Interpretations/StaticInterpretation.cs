using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public class StaticInterpretation : Interpretation
    {
        string _text;

        public StaticInterpretation(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            _text = text;
        }

        public string AsText()
        {
            return _text;
        }

        public string AsText(Func<string,string> formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException("formatter");
            return formatter(_text) ?? _text;
        }
    }
}
