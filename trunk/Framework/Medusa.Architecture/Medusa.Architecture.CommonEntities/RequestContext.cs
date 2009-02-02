using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Define el contexto de la solicitud
    /// </summary>
    public class RequestContext:Context 
    {
        
        private String _Usuario = null;

        /// <summary>
        /// Informacion identificatoria del Usuario
        /// </summary>
        public String Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        
        private String _Empresa = null;

        /// <summary>
        /// Informacion Identificatoria de la Empresa
        /// </summary>
        public String Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        
        private String _UO = null;

        /// <summary>
        /// Información Identificatoria de la Unidad Operativa
        /// </summary>
        public String UO
        {
            get { return _UO; }
            set { _UO = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUsuario">Usuario asociado</param>
        public RequestContext(String pUsuario)
        {
            Usuario = pUsuario;
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RequestContext()
        {

        }

        
       
    }
}
