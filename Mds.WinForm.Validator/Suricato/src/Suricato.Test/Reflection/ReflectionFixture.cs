using Suricato.Reflection;
using Suricato.Test.FooObjects;
using Xunit;

namespace Suricato.Test.Reflection
{
    public class ReflectionFixture
    {
        [Fact]
        public void GetField()
        {
            Foo f = new Foo();
            f.Id = 1;
            f.Name = "my name it's foo";

            Assert.Equal(1, (int) Reflector.FieldGet(f, "id"));
            Assert.Equal("my name it's foo", (string) Reflector.FieldGet(f, "name"));
        }
    }
}