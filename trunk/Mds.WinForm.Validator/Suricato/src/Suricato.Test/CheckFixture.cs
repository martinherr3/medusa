using System;
using Xunit;
namespace Suricato.Test
{
    public class CheckFixture
    {
        [Fact]
        public void NotNull() {
            object Null = null;
            Assert.Throws<ArgumentNullException>(delegate { Check.NotNull(Null); });

            Assert.Equal("parametter",
            Assert.Throws<ArgumentNullException>(delegate 
            { 
                Check.NotNull(Null,"parametter","message"); 
            }).ParamName);

            object @object = new object();
            Assert.NotNull(@object);
        }


    }
}