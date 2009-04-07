using System;

namespace Suricato
{
	public class Utils
	{
		public static string GetPropertyName(string name)
		{
			if (!char.IsLower(name[0]))
			{
				throw new InvalidOperationException(
					"The first letter of the name must be lower case in order to extract the Property Name");
			}

			for(int i = 0; i < name.Length; i++)
			{
				if (char.IsUpper(name[i])) return name.Substring(i);
			}

			throw new ArgumentException("Could not extract the property from the parameter","name");
		}

		public static object GetControlValue(object sender)
		{
			return null; //if(sender.GetType() is Winforms)
		}
	}
}