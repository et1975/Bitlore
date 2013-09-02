using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public sealed class CompositeInterpretation : Interpretation
    {
        IEnumerable<Interpretation> _interpretations;
        string _newLine;

        public CompositeInterpretation(IEnumerable<Interpretation> interpretations):this(interpretations,Environment.NewLine)
        {
        }

        public CompositeInterpretation(IEnumerable<Interpretation> interpretations, string newLine)
        {
            if (interpretations == null)
                throw new ArgumentNullException("interpretations");
            if (newLine == null)
                throw new ArgumentNullException("newLine");
            _interpretations = interpretations;
            _newLine = newLine;
        }

        public string AsText()
        {
            return _interpretations
                .Aggregate(new StringBuilder(), (sb,i)=>sb.Length == 0 ? sb.Append(i.AsText()) : sb.AppendFormat("{0}{1}",_newLine,i.AsText()))
                .ToString();
        }

        public string AsText(Func<string,string> formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException("formatter");
            return _interpretations
                .Aggregate(new StringBuilder(), (sb, i) => sb.Length == 0 ? sb.Append(i.AsText(formatter)) : sb.AppendFormat("{0}{1}", _newLine, i.AsText(formatter)))
                .ToString();
        }
    }
}
