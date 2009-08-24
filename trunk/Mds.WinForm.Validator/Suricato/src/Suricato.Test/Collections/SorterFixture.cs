using System.Collections.Generic;
using Suricato.Collections;
using Suricato.Test.FooObjects;
using Xunit;

namespace Suricato.Test.Collections
{
    public class SorterFixture
    {
        public IList<Foo> GetList() {
            IList<Foo> l = new List<Foo>();
            l.Add(new Foo(2, "xavier", 4.0m));
            l.Add(new Foo(1, "carlos", 3.0m));
            l.Add(new Foo(3, "maria", 2.0m));
            return l;
        }

        [Fact]
        public void Sorter()
        {
            IList<Foo> l = GetList();

            IList<Foo> orderList = new Sorter<Foo>().By("Id").Sort(l);

            Assert.Equal(1, orderList[0].Id);
            Assert.Equal(2, orderList[1].Id);
            Assert.Equal(3, orderList[2].Id);

            IList<Foo> orderList2 = new Sorter<Foo>().By("Name").Sort(l);

            Assert.Equal("carlos", orderList2[0].Name);
            Assert.Equal("maria", orderList2[1].Name);
            Assert.Equal("xavier", orderList2[2].Name);


            IList<Foo> orderList3 = new Sorter<Foo>().By("Price").Sort(l);

            Assert.Equal(2.0m, orderList3[0].Price);
            Assert.Equal(3.0m, orderList3[1].Price);
            Assert.Equal(4.0m, orderList3[2].Price);
        }

        [Fact]
        public void SorterIComparer()
        {
            IList<Foo> l = GetList();

            IList<Foo> orderList = new Sorter<Foo>().With(new OrderByIdComparer()) .Sort(l);

            Assert.Equal(1, orderList[0].Id);
            Assert.Equal(2, orderList[1].Id);
            Assert.Equal(3, orderList[2].Id);

            IList<Foo> orderList2 = new Sorter<Foo>().With(new OrderByNameComparer()).Sort(l);

            Assert.Equal("carlos", orderList2[0].Name);
            Assert.Equal("maria", orderList2[1].Name);
            Assert.Equal("xavier", orderList2[2].Name);

            IList<Foo> orderList3 = new Sorter<Foo>().With(new OrderByPriceComparer()) .Sort(l);

            Assert.Equal(2.0m, orderList3[0].Price);
            Assert.Equal(3.0m, orderList3[1].Price);
            Assert.Equal(4.0m, orderList3[2].Price);
        }
    }

    #region Comparers

    internal class OrderByPriceComparer : IComparer<Foo>
    {
        public int Compare(Foo x, Foo y) {
            return x.Price.CompareTo(y.Price);
        }
    }

    public class OrderByIdComparer : IComparer<Foo>
    {
        public int Compare(Foo x, Foo y) {
            return x.Id.CompareTo(y.Id);
        }
    }

    public class OrderByNameComparer : IComparer<Foo>
    {
        public int Compare(Foo x, Foo y) {
            return x.Name.CompareTo(y.Name);
        }
    }

    #endregion 

}