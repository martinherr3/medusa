using System.Windows.Forms;

namespace Suricato.Winforms.Validation.ValidatorControls
{
	public interface IControlValuable
	{
		object GetValue(Control control);
	}
}