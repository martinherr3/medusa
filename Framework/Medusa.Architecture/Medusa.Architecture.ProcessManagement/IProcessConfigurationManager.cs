using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.ProcessManagement
{
	/// <summary>
	/// Interfaz para clases que leer y escribir configuración de procesos de negocio.
	/// </summary>
	/// <remarks>
	/// Esta interfaz deberá ser implementada por todas las clases que interactúan con las fachadas de negocio, siendo utilizadas para realizar lecturas y escrituras de configuración de procesos de negocio.
	/// Cada implementación deberá ser específica para un tipo de repositorio determinado: archivo xml, base de datos, múltiples archivos de recursos, etc.
	/// </remarks>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public interface IProcessConfigurationManager
	{
		/// <summary>
		/// Recupera la configuración de un proceso de negocio.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso.</param>
		/// <returns>Configuración del proceso de negocio.</returns>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		ProcessConfiguration GetProcessConfiguration(string pProcessName);

		/// <summary>
		/// Recupera la configuración de todos los procesos de negocio.
		/// </summary>
		/// <returns>Lista de configuraciones de procesos de negocio.</returns>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		ProcessConfigurationCollection GetAllProcesses();


		/// <summary>
		/// Actualiza la configuración de un proceso de negocio.
		/// </summary>
		/// <param name="pProcessConfiguration">Configuración del proceso de negocio.</param>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		void SetProcessConfiguration(ProcessConfiguration pProcessConfiguration);

		/// <summary>
		/// Almacena la configuración de un nuevo proceso de negocio.
		/// </summary>
		/// <param name="pProcessConfiguration">Configuración del proceso de negocio.</param>
		/// <date>2006-02-13T00:00:00</date>
		/// <author>gmedina</author>
		void AddProcessConfiguration(ProcessConfiguration pProcessConfiguration);

		/// <summary>
		/// Elimina la configuración de un proceso de negocio.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso.</param>
		/// <date>2006-02-13T00:00:00</date>
		/// <author>gmedina</author>
		void DeleteProcessConfiguration(string pProcessName);
	}
}
