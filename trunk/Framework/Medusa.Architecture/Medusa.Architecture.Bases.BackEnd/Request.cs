using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Medusa.Architecture.Bases.BackEnd;

namespace Medusa.Architecture.Bases.BackEnd
{
	public abstract class Request<T> : IXMLDataCarrier
		where T : System.Data.DataSet, new()
	{
		T _Data = new T();
		ContextInformation _Context;

		public T Param
		{
			get { return _Data; }
		}

		#region IXMLDataCarrier Members

		public ContextInformation Context
		{
			get { return _Context; }
			set { _Context = value; }
		}

		public string GetXML()
		{
			return _Data.GetXml();
		}

		public void SetXML(string pXMLData)
		{
			StringReader wReader = new StringReader(pXMLData);
			_Data.ReadXml(wReader);
		}

		#endregion

	}
}
