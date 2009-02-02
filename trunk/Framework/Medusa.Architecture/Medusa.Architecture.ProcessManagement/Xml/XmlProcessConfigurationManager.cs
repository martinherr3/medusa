using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Medusa.Architecture.HelpersFunctions;
using Medusa.Architecture.Transaction;
using Medusa.Architecture.Xml;

namespace Medusa.Architecture.ProcessManagement.Xml
{
	/// <summary>
	/// Manejador de configuración de procesos que trabaja con un archivo XML como repositorio de configuración.
	/// </summary>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public sealed class XmlProcessConfigurationManager : IProcessConfigurationManager
	{


		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		public XmlProcessConfigurationManager()
		{
			_XmlConfigSection = (XmlConfigSection)System.Configuration.ConfigurationManager.GetSection("XmlConfig");
		}

		#region < Fields >
		XmlConfigSection _XmlConfigSection;
		#endregion

		#region < IProcessConfiguration Members >


		/// <summary>
		/// Devuelve la configuración de un proceso buscándolo en el repositorio XML.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso</param>
		/// <returns>Configuración del proceso</returns>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public ProcessConfiguration GetProcessConfiguration(string pProcessName)
		{

			ProcessConfigurationCollection wProcesses;
			ProcessConfiguration wResult;

			try
			{
				wProcesses = GetAllProcesses();

				if (!wProcesses.Contains(pProcessName))
				{
					throw new Exception("El proceso " + pProcessName + " no se encuentra configurado.");
				}

				wResult = wProcesses[pProcessName];

				return wResult;
			}
			finally
			{
				wProcesses = null;
			}
		}

		/// <summary>
		/// Recupera la configuración de todos los procesos de negocio.
		/// </summary>
		/// <returns>Colección de configuraciones de procesos de negocio.</returns>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		public ProcessConfigurationCollection GetAllProcesses()
		{
			ProcessConfigurationCollection wResult;
			XmlDocument wDocument;

			try
			{
				wDocument = GetXMLProcessConfigurationDocument();
				wResult = new ProcessConfigurationCollection();

				foreach (XmlNode wNode in wDocument.DocumentElement.SelectSingleNode("Processes").SelectNodes("Process"))
				{
					ProcessConfiguration wProcessConfiguration = new ProcessConfiguration();
					wProcessConfiguration.Name = Convert.ToString(wNode.Attributes["Name"].InnerText);
					wProcessConfiguration.Description = Convert.ToString(wNode.SelectSingleNode("Description").InnerText);
					wProcessConfiguration.Handler = Convert.ToString(wNode.SelectSingleNode("Handler").InnerText);
					wProcessConfiguration.Request = Convert.ToString(wNode.SelectSingleNode("Request").InnerText);
					wProcessConfiguration.Response = Convert.ToString(wNode.SelectSingleNode("Response").InnerText);
					wProcessConfiguration.Available = Convert.ToBoolean(wNode.SelectSingleNode("Available").InnerText);
					wProcessConfiguration.Audit = Convert.ToBoolean(wNode.SelectSingleNode("Audit").InnerText);
					wProcessConfiguration.ProcessTransactionalBehaviour = (TransactionalBehaviour)Enum.Parse(typeof(TransactionalBehaviour), wNode.SelectSingleNode("ProcessTransactionalBehaviour").InnerText);
					wProcessConfiguration.ProcessIsolationLevel = (IsolationLevel)Enum.Parse(typeof(IsolationLevel), wNode.SelectSingleNode("ProcessIsolationLevel").InnerText);
					wProcessConfiguration.Timeout = Convert.ToInt32(wNode.SelectSingleNode("Timeout").InnerText);

					wResult.Add(wProcessConfiguration);
				}


				return wResult;
			}
			finally
			{
				wDocument = null;
			}

		}

		/// <summary>
		/// Actualiza la configuración de un proceso de negocio.
		/// </summary>
		/// <param name="pProcessConfiguration">Configuración del proceso de negocio.</param>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		public void SetProcessConfiguration(ProcessConfiguration pProcessConfiguration)
		{
			XmlDocument wDocument;
			XmlElement wNode;

			try
			{
				wDocument = GetXMLProcessConfigurationDocument();

				wNode = GetXMLProcessNode(pProcessConfiguration.Name, wDocument);

				if (wNode == null)
				{
					throw new Exception("El proceso " + pProcessConfiguration.Name + " no se encuentra configurado.");
				}

				wNode.Attributes["Name"].InnerText = pProcessConfiguration.Name;
				wNode.SelectSingleNode("Description").InnerText = pProcessConfiguration.Description;
				wNode.SelectSingleNode("Handler").InnerText = pProcessConfiguration.Handler;
				wNode.SelectSingleNode("Request").InnerText = pProcessConfiguration.Request;
				wNode.SelectSingleNode("Response").InnerText = pProcessConfiguration.Response;
				wNode.SelectSingleNode("Available").InnerText = pProcessConfiguration.Available.ToString();
				wNode.SelectSingleNode("Audit").InnerText = pProcessConfiguration.Audit.ToString();
				wNode.SelectSingleNode("ProcessTransactionalBehaviour").InnerText = Enum.GetName(typeof(TransactionalBehaviour), pProcessConfiguration.ProcessTransactionalBehaviour);
				wNode.SelectSingleNode("ProcessIsolationLevel").InnerText = Enum.GetName(typeof(IsolationLevel),pProcessConfiguration.ProcessIsolationLevel);
				wNode.SelectSingleNode("Timeout").InnerText = pProcessConfiguration.Timeout.ToString();

				wDocument.Save(GetXMLRepositoryPath());
			}
			finally
			{
				wNode = null;
				wDocument = null;
			}
		}

		/// <summary>
		/// Almacena la configuración de un nuevo proceso de negocio.
		/// </summary>
		/// <param name="pProcessConfiguration">Configuración del proceso de negocio.</param>
		/// <date>2006-02-13T00:00:00</date>
		/// <author>gmedina</author>
		public void AddProcessConfiguration(ProcessConfiguration pProcessConfiguration)
		{
			XmlDocument wDocument;
			XmlElement wNode;

			try
			{
				wDocument = GetXMLProcessConfigurationDocument();

				wNode = (XmlElement) Node.NodeCreateAndAdd((XmlElement)wDocument.DocumentElement.SelectSingleNode("Processes"), "Process");

				NodeAttribute.AttributeCreateAndSet(wDocument, wNode, "Name", pProcessConfiguration.Name);
				Node.NodeCreateAndAdd(wNode, "Description", pProcessConfiguration.Description);
				Node.NodeCreateAndAdd(wNode, "Handler", pProcessConfiguration.Handler);
				Node.NodeCreateAndAdd(wNode, "Request", pProcessConfiguration.Request);
				Node.NodeCreateAndAdd(wNode, "Response", pProcessConfiguration.Response);
				Node.NodeCreateAndAdd(wNode, "Available", pProcessConfiguration.Available.ToString());
				Node.NodeCreateAndAdd(wNode, "Audit", pProcessConfiguration.Audit.ToString());
				Node.NodeCreateAndAdd(wNode, "ProcessTransactionalBehaviour", Enum.GetName(typeof(TransactionalBehaviour), pProcessConfiguration.ProcessTransactionalBehaviour));
				Node.NodeCreateAndAdd(wNode, "ProcessIsolationLevel", Enum.GetName(typeof(IsolationLevel), pProcessConfiguration.ProcessIsolationLevel));
				Node.NodeCreateAndAdd(wNode, "Timeout", pProcessConfiguration.Timeout.ToString());

				wDocument.Save(GetXMLRepositoryPath());
			}
			finally
			{
				wNode = null;
				wDocument = null;
			}
		}

		/// <summary>
		/// Elimina la configuración de un proceso de negocio.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso.</param>
		/// <date>2006-02-13T00:00:00</date>
		/// <author>gmedina</author>
		public void DeleteProcessConfiguration(string pProcessName)
		{
			XmlDocument wDocument;
			XmlElement wNode;


			try
			{
				wDocument = GetXMLProcessConfigurationDocument();

				wNode = GetXMLProcessNode(pProcessName, wDocument);

				if (wNode == null)
				{
					throw new Exception("El proceso " + pProcessName + " no se encuentra configurado.");
				}

				wNode.ParentNode.RemoveChild(wNode);

				wDocument.Save(GetXMLRepositoryPath());
			}
			finally
			{
				wNode = null;
				wDocument = null;
			}


		}

		#endregion

		#region < Private methods >

		/// <summary>
		/// Busca la configuración de un proceso.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso de negocio.</param>
		/// <param name="pDocument">Documento con el contenido del repositorio XML.</param>
		/// <returns>Nodo que contiene la configuración del proceso.</returns>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		private XmlElement GetXMLProcessNode(string pProcessName, XmlDocument pDocument)
		{
			XmlElement wNode;
			// realiza la consulta XPath
			string wXPath = "Process[@Name='" + pProcessName + "']";
			wNode = (XmlElement)pDocument.DocumentElement.SelectSingleNode("Processes").SelectSingleNode(wXPath);

			if (wNode == null)
			{
				throw new Exception("El proceso " + pProcessName + " no se encuentra configurado.");
			}

			return wNode;
		}

		/// <summary>
		/// Recupera el repositorio XML.
		/// </summary>
		/// <returns>Documento con el contenido del repositorio XML.</returns>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		private XmlDocument GetXMLProcessConfigurationDocument()
		{
			XmlDocument wDocument;
			wDocument = new XmlDocument();

			wDocument.Load(GetXMLRepositoryPath());
			return wDocument;
		}

		/// <summary>
		/// Devuelve la ruta al repositorio XML.
		/// </summary>
		/// <returns>Ruta al repositorio XML.</returns>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		private string GetXMLRepositoryPath()
		{
			string wResult = _XmlConfigSection.ConfigurationFilePath;
			return wResult;
		}

		#endregion

	}
}
