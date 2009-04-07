using System;
using Xunit;

namespace Suricato.Test
{
	public class UtilsFixture
	{
		[Fact]
		public void test01()
		{
			Assert.Equal("Customer", Utils.GetPropertyName("txtCustomer"));
			Assert.Equal("Country", Utils.GetPropertyName("cboCountry"));
		}

		[Fact]
		public void test02()
		{
			Assert.Throws<InvalidOperationException>(delegate { Assert.Equal("Customer", Utils.GetPropertyName("TxtCustomer")); });
		}
	}
}