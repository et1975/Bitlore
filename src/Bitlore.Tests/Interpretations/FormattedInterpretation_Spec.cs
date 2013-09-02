using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bitlore.Tests
{
    public class Given_a_FormattedInterpretation
    {
        Interpretation _i = new FormattedInterpretation("value:{0}",1);

        [Fact]
        public void Produces_Text()
        {
            Assert.Equal("value:1",_i.AsText());
        }

        [Fact]
        public void Uses_Formatter()
        {
            Assert.Equal("_value:1_",_i.AsText(t => string.Format("_{0}_",t)));
        }
    }
}
