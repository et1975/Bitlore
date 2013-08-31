using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public interface Rule<T>
    {
        bool Evaluate(T item);
    }
}
