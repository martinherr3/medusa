using System.Windows.Forms;
using Suricato.Winforms.Validation.ValidatorControls;

namespace Suricato.Winforms.Validation.ValidatorControls
{
	[ControlValidable(typeof(DateTimePicker))]
	public class DateTimePickerValuable : IControlValuable
	{
		#region IControlValuable Members

		public object GetValue(Control control)
		{
			return ((DateTimePicker) control).Value;
		}

		#endregion
	}
}