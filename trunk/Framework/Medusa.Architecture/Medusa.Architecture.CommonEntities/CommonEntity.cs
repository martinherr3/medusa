using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using Medusa.Architecture.HelpersFunctions;

namespace Medusa.Architecture.CommonEntities
{
    /// <summary>
    /// Clase base para las entidades.
    /// </summary>
    [Serializable]
    public class CommonEntity
    {
        /// <summary>
        /// Serializa en formato XML la entidad.
        /// </summary>
        /// <returns></returns>
        public String Serializar()
        {
            return SerializationFunctions.Serialize(this);
        }

        /// <summary>
        /// Deserializa a partir de un XML al objeto indicado mediante generics.
        /// </summary>
        /// <typeparam name="T">Objeto que se creará a partir de la Deserialización</typeparam>
        /// <param name="pXML">XML con una serialización del Objeto.</param>
        /// <returns></returns>
        public static T Deserializar<T>(String pXML)
        {
            return (T)SerializationFunctions.Deserialize(typeof(T), pXML);
        }

        /// <summary>
        /// Clona un objeto de estructura simple o compleja.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return CloneFunctions.CloneObject(this);
        }
             
    }


    /// <summary>
    /// Clase base para las listas de entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos que contendrá la lista.</typeparam>
    [Serializable]
    public class ListCommonEntity<T> : List<T>
        where T : CommonEntity, new()
    {
        /// <summary>
        /// Serializa la lista de entidades a un XML.
        /// </summary>
        /// <returns></returns>
        public String Serializar()
        {
            return SerializationFunctions.Serialize(this);
        }

        /// <summary>
        /// Deserializa a partir de un XML al objeto indicado mediante generics.
        /// </summary>
        /// <param name="pXML">XML con una serialización de la lista.</param>
        /// <returns></returns>
        public static T Deserializar(String pXML)
        {
            return CommonEntity.Deserializar<T>(pXML);
        }

        /// <summary>
        /// Clona un objeto de estructura simple o compleja.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return CloneFunctions.CloneObject(this);
        }

        /// <summary>
        /// Obtiene un DataSet a partir de la entidad.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet ExtraerDataSet()
        {

            DataSet wDts = new DataSet(this.GetType().Name);

            String strXml = SerializationFunctions.SerializeToXml(this);
            wDts.ReadXml(new StringReader(strXml));

            return wDts;

        }

        private Object _filtro;

        /// <summary>
        /// Filtro que se aplicará en método busqueda.
        /// </summary>
        public Object Filtro
        {
            get { return _filtro; }
            set { _filtro = value; }
        }


        /// <summary>
        /// Filtra la lista y devuelve una nueva lista.
        /// </summary>
        /// <typeparam name="R">Tipo de la lista que se filtrará</typeparam>
        /// <param name="pFiltro">Objeto que se usará para filtrar.</param>
        /// <returns></returns>
        public R Filtrar<R>(Object pFiltro)
            where R : ListCommonEntity<T>, new()
        {
            Filtro = pFiltro;
            R wR = new R();
            List<T> wT = this.FindAll(busqueda);
            foreach (T wTipo in wT)
            {
                wR.Add(wTipo);
            }
            return wR;
        }

        /// <summary>
        /// Método reescribible que se usará para implementar las condiciones de búsqueda.
        /// </summary>
        /// <param name="pBuscar">Elemento de la lista.</param>
        /// <returns></returns>
        public virtual bool busqueda(T pBuscar)
        {
            return true;
        }

    }
}
