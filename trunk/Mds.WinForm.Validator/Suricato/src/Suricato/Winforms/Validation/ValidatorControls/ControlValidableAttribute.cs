using System;

namespace Suricato.Winforms.Validation.ValidatorControls
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ControlValidableAttribute : Attribute
	{
		private Type control;

		public ControlValidableAttribute(Type clazz)
		{
			control = clazz;
		}

		public Type Control
		{
			get { return control; }
		}
	}
}