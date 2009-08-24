using System;
using System.Windows.Forms;
using Iesi.Collections.Generic;
using Suricato.Winforms.Validation;

namespace Suricato.Winforms.Validation
{
	/// <summary>
	/// 
	/// </summary>
	public class ViewValidator
	{
		protected ISet<BinderItem> binders = new HashedSet<BinderItem>();
		private ErrorProvider errorProvider;
		private ValidatorControlResolver resolver = new ValidatorControlResolver();

		public ViewValidator()
		{
		}

		public ViewValidator(ErrorProvider errorProvider) : base()
		{
			Check.NotNull(
				errorProvider,
				"errorProvider",
				"The ErrorProvider is null, make sure of construct the ViewValidator after the winforms method InitializeComponent();");
			ErrorProvider = errorProvider;
		}

		public ErrorProvider ErrorProvider
		{
			get { return errorProvider; }
			set { errorProvider = value; }
		}

		public ValidatorControlResolver Resolver
		{
			get { return resolver; }
		}


		public void Bind(Control control, Type entity, string propertyName)
		{
			binders.Add(new BinderItem(control, entity, propertyName));
		}

		public Type GetEntityType(Control control)
		{
			foreach(BinderItem item in binders)
			{
				if (control.Equals(item.Control))
					return item.Clazz;
			}
			throw new InvalidOperationException("Could not find the Entity Type for this control");
		}


		public string GetPropertyName(Control control)
		{
			foreach(BinderItem item in binders)
			{
				if (control.Equals(item.Control))
					return item.PropertyName;
			}
			throw new InvalidOperationException("Could not find the Entity Type for this control");
		}
	}
}