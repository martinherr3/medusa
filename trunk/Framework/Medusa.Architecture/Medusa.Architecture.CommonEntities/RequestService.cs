using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Define la solicitud a un servicio
    /// </summary>
    /// <typeparam name="T">Tipo de datos enviado al servicio</typeparam>
    public class RequestService<T>
    {
        private T _ServiceData;

        /// <summary>
        /// Datos devueltos por el Servicio
        /// </summary>
        public T ServiceData
        {
            get { return _ServiceData; }
            set { _ServiceData = value; }
        }

        
        private RequestContext _RequestContext = null;

        /// <summary>
        /// Información del contexto de la solicitud
        /// </summary>
        public RequestContext RequestContext
        {
            get { return _RequestContext; }
            set { _RequestContext = value; }
        }

        /// <summary>
        /// Crea un RequestService.
        /// </summary>
        /// <param name="pServiceData">Datos de la Solicitud</param>
        public RequestService(T pServiceData):this()
        {
            ServiceData = pServiceData;
        }

        /// <summary>
        /// Crea un RequestService
        /// </summary>
        public RequestService()
        {

        }

        /// <summary>
        /// Crea un RequestService
        /// </summary>
        /// <param name="pServiceData">Datos de la Solicitud</param>
        public RequestService(T pServiceData, RequestContext pContexto):this(pServiceData)
        {
            RequestContext = pContexto;
        }
    }
}
