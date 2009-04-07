using NHibernate.Validator.Constraints;
namespace NHibernate.Validator.Winforms_Demo.Entities
{
	public class Address
	{
		private int number;
		private string street;

		public Address(string street, int number)
		{
			this.street = street;
			this.number = number;
		}

		[NotEmpty]
		public string Street
		{
			get { return street; }
		}

		[NotEmpty]
		public int Number
		{
			get { return number; }
		}
	}
}