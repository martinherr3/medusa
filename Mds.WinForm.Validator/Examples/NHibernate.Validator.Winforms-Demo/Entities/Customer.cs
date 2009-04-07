using NHibernate.Validator.Constraints;
namespace NHibernate.Validator.Winforms_Demo.Entities
{
	public class Customer
	{
		private string email;
		private string firstName;
		private int id;
		private string lastName;
		private Address location;

		public int Id
		{
			get { return id; }
		}

		[NotEmpty, Length(30)]
		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		[NotEmpty, Length(Max = 30)]
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		[Email]
		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		[Valid]
		public Address Location
		{
			get { return location; }
			set { location = value; }
		}
	}
}