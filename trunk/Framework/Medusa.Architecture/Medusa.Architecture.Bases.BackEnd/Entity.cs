using System;
using System.Collections.Generic;
using System.Text;
using Medusa.Architecture.HelpersFunctions;
using System.Data;
using System.Reflection;
using System.IO;

namespace Medusa.Architecture.Bases.BackEnd
{
	/// <summary>
	/// Clase base que representa una entidad.
	/// </summary>
	public abstract class Entity
	{
		/// <summary>
		/// Devuelve la representación en xml de los datos mantenidos en la entidad.
		/// </summary>
		public string GetXml()
		{
			return SerializationFunctions.Serialize(this);
		}

        /// <summary>
        /// Obtiene un System.DataSet .-
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet()
        {

            DataSet wDts = new DataSet(this.GetType().Name);

            String strXml = SerializationFunctions.SerializeToXml(this);
            wDts.ReadXml(new StringReader(strXml));

            return wDts;
        }

        /// <summary>
        /// Metodo estatico que deserializa on objeto y retorna el xmls correspondiente
        /// </summary>
        /// <param name="pType">Tipo de objeto a deserializar</param>
        /// <param name="pXMLData">Xml con el que se crea el objeto</param>
        /// <returns>object que hereda de Entity</returns>
        public static object GetObjectFromXml(Type pType, string pXMLData)
        {
            return SerializationFunctions.DeserializeFromXml(pType, pXMLData);
        }

	}
}
