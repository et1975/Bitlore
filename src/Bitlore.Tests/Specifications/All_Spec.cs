using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_an_All_EvenAndPositive_Specification
    {
        AllSpecification<int> spec = new AllSpecification<int>(new Dictionary<Rule<int>,Func<Rule<int>,Interpretation>>
            {
                {Rules<int>.Predicate(x => x % 2 == 0), _ => Interpretations.Static("NOT_EVEN")},
                {Rules<int>.Predicate(x => x > 0), _ => Interpretations.Static("NOT_POSITIVE")}
            });

        [Fact]
        public void Evaluates_2_as_True()
        {
            Assert.True(((Rule<int>)spec).Evaluate(2));
        }

        [Fact]
        public void Evaluates_minus2_as_False()
        {
            Assert.False(((Rule<int>)spec).Evaluate(-2));
            Assert.True(new[] { "NOT_POSITIVE" }.SequenceEqual(spec.Test(-2).Select(i => i.AsText())));
        }

        [Fact]
        public void Evaluates_3_as_False()
        {
            Assert.False(((Rule<int>)spec).Evaluate(3));
            Assert.True(new[] { "NOT_EVEN" }.SequenceEqual(spec.Test(3).Select(i => i.AsText())));
        }
    }
}
