using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Medusa.Architecture.HelpersFunctions
{
	public class ReflectionFunctions
	{
		static private string[] GetStringsFromAssemblyString(string pAssemblyString)
		{
			int wIndex = pAssemblyString.IndexOf(',');
			string[] wResult = new string[] { pAssemblyString.Substring(0, wIndex).Trim(), pAssemblyString.Remove(0, wIndex + 1).Trim() };
			return wResult;
		}

		static public string GetAssemblyNameFromAssemblyString(string pAssemblyString)
		{
			string wResult = GetStringsFromAssemblyString(pAssemblyString)[1];
			return wResult;
		}

		static public object CreateInstance(string pAssemblyString)
		{

			string wClassName;
			string wAssembly;
			ExtractTypeInformation(pAssemblyString, out wClassName, out wAssembly);

			// crea el objeto dinámicamente
			object wResult = CreateInstance(wClassName, wAssembly);

			return wResult;
		}

		private static void ExtractTypeInformation(string pAssemblyString, out string pClassName, out string pAssembly)
		{
			// Divide el assemblyString según la coma y guarda el resultado
			// en un array
			string[] wParts = GetStringsFromAssemblyString(pAssemblyString);

			// Verifica que el array tenga 2 partes
			if (wParts.Length < 2)
			{
				throw new Exception("Assembly mal configurado.");
			}

			// Toma las partes del assemblyArray en dos strings separados
			pClassName = wParts[0].Trim();
			pAssembly = wParts[1].Trim();

			// Verifica que el string strNamespaceClass tenga al menos un punto
			if (pClassName.IndexOf(".") < 0)
			{
				throw new Exception("Assembly mal configurado.");
			}
		}

		static public object CreateInstance(string pClassName, string pAssemblyName)
		{
			Assembly wAssembly = Assembly.Load(pAssemblyName);
			object wResult = wAssembly.CreateInstance(pClassName, true);

			return wResult;
		}

		static public Type CreateType(string pClassName, string pAssemblyName)
		{
			Assembly wAssembly = Assembly.Load(pAssemblyName);
			Type wResult = wAssembly.GetType(pClassName, true);

			return wResult;
		}

		static public Type CreateType(string pAssemblyString)
		{

			string wClassName;
			string wAssembly;
			ExtractTypeInformation(pAssemblyString, out wClassName, out wAssembly);

			// crea el tipo dinámicamente
			Type wResult = CreateType(wClassName, wAssembly);

			return wResult;
		}

	}
}
