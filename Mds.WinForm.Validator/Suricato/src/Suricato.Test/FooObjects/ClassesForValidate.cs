using Castle.Components.Validator;

namespace Suricato.Test.FooObjects
{
    public class Customer
    {
        private int id;
        private string lastName;
        private string name;

        public string Email {
            get { return email; }
            set { email = value; }
        }

        private string email;

        public Customer() {
        }

        public Customer(int id, string name, string lastName) {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
        }

        public int Id {
            get { return id; }
            set { id = value; }
        }

        [ValidateNonEmpty]
        public string Name {
            get { return name; }
            set { name = value; }
        }

        [ValidateNonEmpty]
        public string LastName {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}