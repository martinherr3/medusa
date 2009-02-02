using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Medusa.Architecture.ProcessManagement.Xml
{

	/// <summary>
	/// Sección de configuración para el administrador de configuración de procesos con repositorio XML.
	/// </summary>
	/// <date>2006-02-14T00:00:00</date>
	/// <author>gmedina</author>
	internal class XmlConfigSection : ConfigurationSection
	{
		#region <public properties>

		/// <summary>
		/// Ruta del repositorio XML.
		/// </summary>
		/// <remarks>
		/// Si esta propiedad no se setea en la configuración de la aplicación el valor por defecto depende del tipo de aplicación:
		/// <list type="bullet">
		/// <item>Aplicaciones Windows: [raiz del proceso] + @"\BPConfig.xml"</item>
		/// <item>Aplicaciones Web: [raiz del directorio] + @"\BPConfig.xml"</item>
		/// </list>
		/// </remarks>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		[ConfigurationProperty("ConfigurationFilePath", DefaultValue = "BPConfig.xml")]
		public string ConfigurationFilePath
		{
			get
			{
				string wResult = (string)base["ConfigurationFilePath"];
				return wResult;
			}
			set
			{
				base["ConfigurationFilePath"] = value;
			}
		}
		#endregion
	}
}
