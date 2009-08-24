using System.Windows.Forms;
using Suricato.Winforms.Validation.ValidatorControls;

namespace Suricato.Winforms.Validation.ValidatorControls
{
	[ControlValidable(typeof(TextBox))]
	[ControlValidable(typeof(RichTextBox))]
	public class TextValuable : IControlValuable
	{
		#region IControlValuable Members

		public object GetValue(Control control)
		{
			return control.Text;
		}

		#endregion
	}
}