using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_a_CompositeInterpretation
    {
        Interpretation _i = new CompositeInterpretation(new [] 
        { 
            Interpretations.Static("value:1"),
            Interpretations.Static("value:2")
        });

        [Fact]
        public void Produces_Text()
        {
            Assert.Equal("value:1" + Environment.NewLine + "value:2", 
                _i.AsText());
        }

        [Fact]
        public void Uses_Formatter()
        {
            Assert.Equal("_value:1_" + Environment.NewLine + "_value:2_", 
                _i.AsText(t => string.Format("_{0}_", t)));
        }
    }
}
