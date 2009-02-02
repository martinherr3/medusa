using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Medusa.Architecture.Xml
{
    /// <summary>
    /// Manejo de CData.
    /// </summary>
    /// <Author>Jorge F. Lousararian</Author>
    /// <Date>28-12-2005</Date>
    public class CData
    {
        /// <summary>
        /// Crea y agrega una seccion CDATA en un nodo.
        /// </summary>
        /// <param name="pnode">El nodo al que se le agregara la seccion CDATA.</param>
        public static void CDATASectionCreateAndAdd(XmlNode pnode)
        {
            CDATASectionCreateAndAdd(pnode, "");
        }

        /// <summary>
        /// Crea y agrega una seccion CDATA en un nodo.
        /// </summary>
        /// <param name="pnode">El nodo al que se le agregara la seccion CDATA.</param>
        /// <param name="pCDATASectionValue">El contenido de la seccion CDATA.</param>
        public static void CDATASectionCreateAndAdd(XmlNode pnode, string pCDATASectionValue)
        {
            XmlCDataSection wNewCDataSection = pnode.OwnerDocument.CreateCDataSection(pCDATASectionValue);
            pnode.AppendChild(wNewCDataSection);
        }
    }
}
