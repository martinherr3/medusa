using System;
using System.Windows.Forms;

namespace Suricato.Winforms.Validation
{
	public class BinderItem
	{
		private Type clazz;
		private Control control;
		private string propertyName;

		public BinderItem(Control control, Type clazz, string propertyName)
		{
			this.control = control;
			this.clazz = clazz;
			this.propertyName = propertyName;
		}

		public Control Control
		{
			get { return control; }
			set { control = value; }
		}

		public Type Clazz
		{
			get { return clazz; }
			set { clazz = value; }
		}

		public string PropertyName
		{
			get { return propertyName; }
			set { propertyName = value; }
		}
	}
}