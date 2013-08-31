using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public class FormattedInterpretation : Interpretation
    {
        string _format;
        object[] _args;

        public FormattedInterpretation(string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            _format = format;
            _args = args;
        }

        public string AsText()
        {
            return string.Format(_format,_args);
        }

        public string AsText(Func<string,string> formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException("formatter");
            var text = AsText();
            return formatter(text) ?? text;
        }
    }
}
