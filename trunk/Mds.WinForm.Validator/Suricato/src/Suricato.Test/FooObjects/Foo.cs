using System;

namespace Suricato.Test.FooObjects
{
	

	[Serializable]
	public class Foo : IValidatable
	{
		private int id;
		private string name;
		[NonSerialized] public decimal? nonSerializablefield;
		private decimal price;

		public Foo(int id, string name, decimal price)
		{
			this.price = price;
			this.name = name;
			this.id = id;
		}

		public Foo()
		{
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}

		public bool IsValid()
		{
			throw new NotImplementedException();
		}

		public string[] ValidationErrorMessages
		{
			get { throw new NotImplementedException(); }
		}
	}
}