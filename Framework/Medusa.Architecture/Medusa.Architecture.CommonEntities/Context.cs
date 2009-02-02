using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Define un contexto
    /// </summary>
    public class Context
    {
        private DateTime? _Hora = DateTime.Now;

        /// <summary>
        /// Fecha y Hora
        /// </summary>  
        public DateTime? FechaYHora
        {
            get { return _Hora; }
            set { _Hora = value; }
        }
       

        private Object _InformacionAdicional = null;

        /// <summary>
        /// Información Complementaria
        /// </summary>  
        public Object InformacionAdicional
        {
            get { return _InformacionAdicional; }
            set { _InformacionAdicional = value; }
        }
    }
}
