using System.ComponentModel;
using System.Windows.Forms;
using NHibernate.Validator;
using Suricato.Winforms.Validation.ValidatorControls;
using NHibernate.Validator.Engine;
using System.Collections.Generic;

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

			IEnumerable<InvalidValue> errors = vtor.GetPotentialInvalidValues(GetPropertyName((Control) sender),controlValuable.GetValue((Control)sender));
            int i = 0;

            foreach (InvalidValue error in errors)
            {
                errorProvider.SetError((TextBox)sender, error.Message);
                i++;
            }

			if (i == 0)
				errorProvider.SetError((TextBox) sender, string.Empty);
		}

		private string GetPropertyName(Control control)
		{
			return vvtor.GetPropertyName(control);
		}
	}
}