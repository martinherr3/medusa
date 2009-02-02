using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Define la respuesta de un servicio  
    /// </summary>
    /// <typeparam name="T">Tipo del dato devuelto por el servicio</typeparam>
    public class ResponseService<T>
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

        private ServiceError _ServiceError = new ServiceError();

        /// <summary>
        /// Errores devueltos por el servicio
        /// </summary>
        public ServiceError ServiceError
        {
            get { return _ServiceError; }
            set { _ServiceError = value;}
        }


        
        private ServiceContext _ServiceContext = null;

        /// <summary>   
        /// Información de contexto del servicio
        /// </summary>
        public ServiceContext ServiceContext
        {
            get { return _ServiceContext; }
            set { _ServiceContext = value; }
        }
 
       
       
       
    }
}
