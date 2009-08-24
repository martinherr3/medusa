using System.ComponentModel;
using System.Windows.Forms;
using NHibernate.Validator;
using Suricato.Winforms.Validation.ValidatorControls;
using NHibernate.Validator.Engine;

namespace Suricato.Winforms.Validation
{
	public class EventValidation
	{
		private ErrorProvider errorProvider;
		private ViewValidator vvtor;

		public EventValidation(ViewValidator vvtor)
		{
			SetValidator(vvtor);
		}

		public void SetValidator(ViewValidator viewValidator)
		{
			this.vvtor = viewValidator;
			this.errorProvider = viewValidator.ErrorProvider;
		}

		public void ValidatingHandler(object sender, CancelEventArgs e)
		{
			System.Type entityType = vvtor.GetEntityType((Control) sender);

			ClassValidator vtor = new ClassValidator(entityType);

			IControlValuable controlValuable = vvtor.Resolver.GetControlValuable(sender);

			InvalidValue[] errors =
				vtor.GetPotentialInvalidValues(GetPropertyName((Control) sender),controlValuable.GetValue((Control)sender));

			if (errors.Length > 0)
				errorProvider.SetError((TextBox) sender, errors[0].Message);
			else
				errorProvider.SetError((TextBox) sender, string.Empty);
		}

		private string GetPropertyName(Control control)
		{
			return vvtor.GetPropertyName(control);
		}
	}
}