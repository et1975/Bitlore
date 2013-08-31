using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_a_PredicateRule_Even
    {
        Rule<int> _rule = new PredicateRule<int>(x => x % 2 == 0);

        [Fact]
        public void Evaluates_2_as_True()
        {
            Assert.True(_rule.Evaluate(2));
        }

        [Fact]
        public void Evaluates_3_as_False()
        {
            Assert.False(_rule.Evaluate(3));
        }
    }
}
