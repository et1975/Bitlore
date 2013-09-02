using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_an_Default_ActionSpecification
    {
        bool _failureInvoked;
        bool _successInvoked;
        
        ActionSpecification<int> _spec;
        public Given_an_Default_ActionSpecification()
        {
            _spec = Specifications.Action(new List<Binding<int>>
            {
                Rules<int>
                    .Predicate(x => x % 2 == 0)
                    .Bind(_ => Interpretations.Static("NOT_EVEN"), (_,i)=>{Assert.Equal("NOT_EVEN",i.AsText());_failureInvoked = true;},
                        _ => Interpretations.Static("EVEN"), (_,i)=>{Assert.Equal("EVEN",i.AsText());_successInvoked = true;}),
                Rules<int>
                    .Predicate(x => x > 0)
                    .Bind(_ => Interpretations.Static("NOT_POSITIVE"), (_,i)=>{Assert.Equal("NOT_POSITIVE",i.AsText());_failureInvoked = true;},
                        _ => Interpretations.Static("POSITIVE"), (_,i)=>{Assert.Equal("POSITIVE",i.AsText());_successInvoked = true;}),
            });
        }

        [Fact]
        public void Invokes_success_path()
        {
            _spec.Test(2);
            Assert.True(_successInvoked);
            Assert.False(_failureInvoked);
        }

        [Fact]
        public void Invokes_failure_path()
        {
            _spec.Test(-3);
            Assert.False(_successInvoked);
            Assert.True(_failureInvoked);
        }

        [Fact]
        public void Doesnt_stop_on_success()
        {
            _spec.Test(-4);
            Assert.True(_successInvoked);
            Assert.True(_failureInvoked);
        }

        [Fact]
        public void Doesnt_stop_on_failure()
        {
            _spec.Test(3);
            Assert.True(_successInvoked);
            Assert.True(_failureInvoked);
        }
    }
}
