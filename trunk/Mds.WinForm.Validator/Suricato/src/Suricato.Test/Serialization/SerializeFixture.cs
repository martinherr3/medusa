using System.Diagnostics;
using Suricato.Serialization;
using Suricato.Test.FooObjects;
using Xunit;

namespace Suricato.Test.Serialization
{
    public class SerializeFixture
    {
        [Fact]
        public void Clone()
        {
            Foo f = new Foo();
            f.Id = 1;
            f.Name = "f";
            f.Price = 2.0m;
            f.nonSerializablefield = 1000.5m;

            Foo clone = (Foo)Serialize.Clone(f);

            Assert.Equal(1,clone.Id);
            Assert.Equal("f",clone.Name);
            Assert.Equal(2.0m, clone.Price);
            Assert.Null(clone.nonSerializablefield);
            
        }
    }
}