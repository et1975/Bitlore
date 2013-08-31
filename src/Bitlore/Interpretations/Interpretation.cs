using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public interface Interpretation
    {
        string AsText();
        string AsText(Func<string,string> formatter);
    }
}
