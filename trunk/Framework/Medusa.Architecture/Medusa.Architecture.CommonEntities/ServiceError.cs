using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Define un error de servicio
    /// </summary>
    public class ServiceError
    {
        
        private String _Mensaje = null;

        /// <summary>
        /// Mensaje de error
        /// </summary>
        public String Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value;
            _HasError = true;
            }
        }

        
        private String _Source = null;

        /// <summary>
        /// Ubicación donde se produjo el error
        /// </summary>
        public String Origen
        {
            get { return _Source; }
            set { _Source = value;
            _HasError = true;
            }
        }

        
        private String _StackTrace = null;

        /// <summary>
        /// Pila de llamadas
        /// </summary>
        public String Pila
        {
            get { return _StackTrace; }
            set { _StackTrace = value;
            _HasError = true;
            }
        }

        
        private String _NroError = null;

        /// <summary>
        /// Número de Error
        /// </summary>
        public String NroError
        {
            get { return _NroError; }
            set {
                try
                {
                    Convert.ToInt32(value);
                    _NroError = value;
                }
                catch 
                {
                    _NroError = null;
                }
            }
        }

        private Boolean _HasError = false;

        /// <summary>
        /// Define si se produjo un error en el servicio
        /// </summary>
        public Boolean HasError
        {
            get { return _HasError; }
        }

        /// <summary>
        /// Constructor para asignar propiedades
        /// </summary>
        /// <param name="pMensaje">Mensaje del Error</param>
        /// <param name="pOrigen">Origen del Error</param>
        /// <param name="pPila">Pila de Llamadas</param>
        /// <param name="pNroError">Número de Error</param>
        public ServiceError(String pMensaje,String pOrigen, String pPila)
        {
            Mensaje = pMensaje;
            Origen = pOrigen;
            Pila = pPila;
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ServiceError()
        {

        }
       
    }
}
