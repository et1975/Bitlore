using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitlore
{
    public sealed class ActionSpecification<T>
    {
        public IEnumerable<Binding<T>> Bindings { get; private set; }
        public bool StopOnFailure { get; private set; }
        public bool StopOnSuccess { get; private set; }
        
        public ActionSpecification(IEnumerable<Binding<T>> bindings, bool stopOnFailure = false, bool stopOnSuccess = false)
        {
            if (bindings == null)
                throw new ArgumentNullException("bindings");
            Bindings = bindings;
            StopOnFailure = stopOnFailure;
            StopOnSuccess = stopOnSuccess;
        }

        public void Test(T item)
        {
            foreach(var x in Bindings
                             .Select( binding => new { res = binding.Rule.Evaluate(item), binding })
                             .TakeWhile(x => !((x.res && StopOnSuccess) || (!x.res && StopOnFailure)))
                             .Select(x => x.res
                                 ? new { a = x.binding.OnSuccess, r = x.binding.Rule, i = x.binding.SuccessInterpretation(x.binding.Rule) }
                                 : new { a = x.binding.OnFailure, r = x.binding.Rule, i = x.binding.FailureInterpretation(x.binding.Rule) }))
                x.a(x.r,x.i);
        }
    }
}
