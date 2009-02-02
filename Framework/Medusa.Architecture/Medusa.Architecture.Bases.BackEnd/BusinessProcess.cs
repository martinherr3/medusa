using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.Bases.BackEnd
{
	/// <summary>
	/// Clase base que representa un proceso de negocio abstracto. Es la clase de la que deben heredar todas aquellas clases que sean implementaciones de procesos de negocio.
	/// </summary>
	/// <remarks>Las clases que heredan de esta clase abstracta son las encargadas de orquestrar las llamadas a los distintos componentes de negocio y de la integración con otros sistemas.</remarks>
	public abstract class BusinessProcess<TRequest, TResponse> 
		where TRequest : IXMLDataCarrier
		where TResponse : IXMLDataCarrier
	{
        /// <summary>
        /// Ejecuta el proceso de negocio.
        /// </summary>
		/// <param name="pProcessRequest"></param>
        /// <returns></returns>
		abstract public TResponse Execute(TRequest pProcessRequest);

	}
}
