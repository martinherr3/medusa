using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.Bases.BackEnd
{
	public interface IXMLDataCarrier
	{
		ContextInformation Context
		{
			get;
			set;
		}
		string GetXML();
		void SetXML(string pXMLData);
	}
}
