using System;
using System.Collections.Generic;
using Suricato.Winforms.Validation.ValidatorControls;

namespace Suricato.Winforms.Validation
{
	public class ValidatorControlResolver
	{
		private List<Type> resolver;

		public ValidatorControlResolver(List<Type> resolver)
		{
			this.resolver = resolver;
		}

		public ValidatorControlResolver()
		{
			//Register default resolvers
			resolver = new List<Type>();
			resolver.Add(typeof(TextValuable));
			resolver.Add(typeof(DateTimePickerValuable));
		}

		public IControlValuable GetControlValuable(object control)
		{
			foreach(Type type in resolver)
			{
				foreach(object o in type.GetCustomAttributes(typeof(ControlValidableAttribute), false))
				{
					ControlValidableAttribute attribute = (ControlValidableAttribute) o;
					if (attribute.Control.IsInstanceOfType(control))
						return (IControlValuable) Activator.CreateInstance(type);
				}
			}
			throw new ArgumentException("Could not find the IControlValuable for this control");
		}

		public void Add(Type controlValuable)
		{
			resolver.Add(controlValuable);
		}
	}
}