using System;
using System.IO;
using System.Collections.Generic;
using Medusa.Architecture.HelpersFunctions;

namespace Medusa.Architecture.ServiceInterfaceWrappers
{

	/// <summary>
	/// Wrapper para interfaz de servicio implementada a través de un web service.
	/// </summary>
	/// <date>2007-12-20T00:00:00</date>
	/// <author>gmedina</author>
	public class Wrapper 
	{

		#region IServiceInterfaceWrapper Members

		/// <summary>
		/// Ejecuta un servicio de negocio.
		/// </summary>
		/// <param name="pServiceName">Nombre del servicio.</param>
		/// <param name="pData">XML con datos de entrada para la ejecución del servicio.</param>
		/// <returns>XML con datos de salida del servicio.</returns>
		/// <date>2007-12-20T00:00:00</date>
        /// <author>gmedina</author>
		public string ExecuteService (string pServiceName, string pData)
		{
			string wResult = null;
           
			using (ServiceInterface.Service wService = new ServiceInterface.Service())
			{
				wResult = wService.ExecuteService(pServiceName, pData);
			}

			return wResult;
		}

		/// <summary>
		/// Ejecuta un servicio de negocio.
		/// </summary>
		/// <param name="pServiceName">Nombre del servicio.</param>
		/// <param name="pData">Cualquier objeto de datos.</param>
		/// <returns>Cualquier objeto de respuesta del servicio.</returns>
		/// <date>2007-06-28</date>
		/// <author>gmedina</author>
        public TResponse ExecuteService<TRequest, TResponse>(string pServiceName, TRequest pData)
		{
            string wResultService = ExecuteService(pServiceName, SerializationFunctions.Serialize(pData));
            TResponse wResult = (TResponse)SerializationFunctions.Deserialize(typeof(TResponse), wResultService);
            return wResult;
		}

		#endregion
	}
}
