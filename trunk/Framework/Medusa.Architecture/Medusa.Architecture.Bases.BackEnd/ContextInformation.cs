using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.Bases.BackEnd
{
	public class ContextInformation
	{
		string _UserName;

		public string UserName
		{
			get { return _UserName; }
			set { _UserName = value; }
		}
	}
}
