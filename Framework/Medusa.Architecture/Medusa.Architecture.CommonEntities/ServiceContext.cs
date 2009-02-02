using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Información de contexto de un servicio
    /// </summary>
    public class ServiceContext: Context 
    {
        
        private String _Servidor = null;

        /// <summary>
        /// Servidor que se ejecutó el servicio
        /// </summary>
        public String Servidor
        {
            get { return _Servidor; }
            set { _Servidor = value; }
        }

        
        private Int32? _TiempoDeRespuesta = null;

        /// <summary>
        /// Tipo que consumió el servicio en ejecutarse en segundos
        /// </summary>  
        public Int32? TiempoDeRespuesta
        {
            get { return _TiempoDeRespuesta; }
            set { _TiempoDeRespuesta = value; }
        }

        
        private String _NombreServicio = null;

        /// <summary>
        /// Nombre del servicio ejecutado
        /// </summary>
        public String NombreServicio
        {
            get { return _NombreServicio; }
            set { _NombreServicio = value; }
        }


        
        
 
       

       
       
    }
}
