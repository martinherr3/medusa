using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Medusa.Architecture.Bases.BackEnd;

namespace Medusa.Architecture.Bases.BackEnd
{
	public abstract class Response<T> : IXMLDataCarrier
		where T : System.Data.DataSet, new()
	{
		ContextInformation _Context;
		T _Data = new T();

		public T Result
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
