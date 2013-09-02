using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_an_EvenOrPositive_AnyRule
    {
        Rule<int> _rule = new AnyRule<int>(new []
            {
                Rules<int>.Predicate(x => x % 2 == 0),
                Rules<int>.Predicate(x => x > 0)
            });

        [Fact]
        public void Evaluates_2_as_True()
        {
            Assert.True(_rule.Evaluate(2));
        }

        [Fact]
        public void Evaluates_minus2_as_True()
        {
            Assert.True(_rule.Evaluate(-2));
        }

        [Fact]
        public void Evaluates_3_as_True()
        {
            Assert.True(_rule.Evaluate(3));
        }

        [Fact]
        public void Evaluates_minus3_as_False()
        {
            Assert.False(_rule.Evaluate(-3));
        }
    }
}
