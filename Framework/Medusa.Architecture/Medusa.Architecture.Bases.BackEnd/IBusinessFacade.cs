 using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.Bases.BackEnd
{
	/// <summary>
	/// Interfaz para clases que ejecutan un proceso de negocio.
	/// </summary>
	/// <remarks>
	/// Esta interfaz deberá ser implementada por todas las clases que ejecuten un proceso de negocio. 
	/// Estas clases deberán implementar el comportamiento no funcional que no es provisto por el servicio que expone los procesos de negocio (seguridad, transaccionalidad, logueo, etc).
	/// </remarks>
	public interface IBusinessFacade
	{
		/// <summary>
		/// Ejecuta un proceso de negocio.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso.</param>
		/// <param name="pData">XML con datos de entrada para el proceso.</param>
		/// <param name="pContextInformation">XML con información de contexto para la ejecución del proceso.</param>
		/// <returns>XML resultado del proceso.</returns>
		string ExecuteService(string pProcessName, string pData, string pContextInformation);
	}
}
