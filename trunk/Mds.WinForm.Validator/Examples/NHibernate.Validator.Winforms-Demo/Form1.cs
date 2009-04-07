using System;
using System.Windows.Forms;
using NHibernate.Validator.Winforms_Demo.Entities;
using Suricato.Winforms.Validation;

namespace NHibernate.Validator.Winforms_Demo
{
	public partial class Form1 : Form
	{
		private SmartViewValidator vvtor;
		//private ViewValidator vvtor;

		public Form1()
		{
			InitializeComponent();
			
			//Smart Form Setup 
			vvtor = new SmartViewValidator(errorProvider1);
			//First Name
			vvtor.Bind(tFirstName, typeof(Customer));
			//Last Name
			vvtor.Bind(tLastName, typeof(Customer));
			//Email
			vvtor.Bind(tEmail, typeof(Customer));
			//Street
			vvtor.Bind(tStreet, typeof(Address));
			//Street Number
			vvtor.Bind(tNumber, typeof(Address));

			//Setup Validation Validation
			//vvtor = new ViewValidator(errorProvider1);
			////First Name
			//vvtor.Bind(tFirstName, typeof(Customer), "FirstName");
			//tFirstName.Validating += new EventValidation(vvtor).ValidatingHandler;
			////Last Name
			//vvtor.Bind(tLastName, typeof(Customer), "LastName");
			//tLastName.Validating += new EventValidation(vvtor).ValidatingHandler;
			////Email 
			//vvtor.Bind(tEmail, typeof(Customer), "Email");
			//tEmail.Validating += new EventValidation(vvtor).ValidatingHandler;
			////Street
			//vvtor.Bind(tStreet, typeof(Address), "Street");
			//tStreet.Validating += new EventValidation(vvtor).ValidatingHandler;
			////Street Number
			//vvtor.Bind(tNumber, typeof(Address), "Number");
			//tNumber.Validating += new EventValidation(vvtor).ValidatingHandler;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
		}
	}
}