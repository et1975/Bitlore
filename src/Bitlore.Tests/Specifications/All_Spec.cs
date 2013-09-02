using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_an_EvenAndPositive_AllSpecification
    {
        AllSpecification<int> _spec = Specifications.All(new Dictionary<Rule<int>,Func<Rule<int>,Interpretation>>
            {
                {Rules<int>.Predicate(x => x % 2 == 0), _ => Interpretations.Static("NOT_EVEN")},
                {Rules<int>.Predicate(x => x > 0), _ => Interpretations.Static("NOT_POSITIVE")}
            });

        [Fact]
        public void Evaluates_2_as_True()
        {
            Assert.True(((Rule<int>)_spec).Evaluate(2));
        }

        [Fact]
        public void Evaluates_minus2_as_False()
        {
            Assert.False(((Rule<int>)_spec).Evaluate(-2));
            Assert.Equal("NOT_POSITIVE",((Interpretation)_spec).AsText());
        }

        [Fact]
        public void Evaluates_3_as_False()
        {
            Assert.False(((Rule<int>)_spec).Evaluate(3));
            Assert.True(new[] { "NOT_EVEN" }.SequenceEqual(_spec.Test(3).Select(i => i.AsText())));
        }
    }
}
