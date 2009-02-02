using System;
using System.Collections.Generic;
using System.Text;
using Medusa.Architecture.Transaction;
using Datablock = Microsoft.Practices.EnterpriseLibrary.Data;

namespace Medusa.Architecture.ProcessManagement.Database
{
	/// <summary>
	/// Manejador de configuración de procesos que trabaja con una base de datos relacional como repositorio de configuración.
	/// </summary>
	/// <remarks>
	/// La base de datos que sirve de repositorio puede estar creada en cualquier producto que sea accesible por los componentes de acceso a datos de Microsoft Enterprise Library.
	/// </remarks>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public sealed class DatabaseProcessConfigurationManager: IProcessConfigurationManager
	{

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		public DatabaseProcessConfigurationManager()
		{
			_DatabaseConfigSection = (DatabaseConfigSection)System.Configuration.ConfigurationManager.GetSection("DatabaseConfig");
		}

		#region < Fields >
		DatabaseConfigSection _DatabaseConfigSection;
		#endregion

		#region < IProcessConfigurationManager Members >

		/// <summary>
		/// Devuelve la configuración de un proceso buscándolo en la base de datos de configuración.
		/// </summary>
		/// <param name="pProcessName">Nombre del proceso</param>
		/// <returns>Configuración del proceso</returns>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public ProcessConfiguration GetProcessConfiguration(string pProcessName)
		{
			Datablock.Database wBPConfig = Datablock.DatabaseFactory.CreateDatabase(_DatabaseConfigSection.ConnectionName);
			ProcessConfiguration wProcessConfiguration;

			using (System.Data.Common.DbCommand wCmd = wBPConfig.GetStoredProcCommand("Process_g_Name"))
			{
				wBPConfig.AddInParameter(wCmd, "Name", System.Data.DbType.String, pProcessName);

				using (System.Data.DataSet wProcessData = wBPConfig.ExecuteDataSet(wCmd))
				{
					if (wProcessData.Tables[0].Rows.Count == 0)
					{
						throw new Exception("El proceso " + pProcessName + " no se encuentra configurado.");
					}

					wProcessConfiguration = GetProcessConfigurationFromRow(wProcessData.Tables[0].Rows[0]);
				}

			}


			return wProcessConfiguration;
		}

		/// <summary>
		/// Recupera la configuración de todos los procesos de negocio.
		/// </summary>
		/// <returns>Lista de configuraciones de procesos de negocio.</returns>
		/// <date>2006-02-13T00:00:00</date>
		/// <author>gmedina</author>
		public ProcessConfigurationCollection GetAllProcesses()
		{
			Datablock.Database wBPConfig = Datablock.DatabaseFactory.CreateDatabase(_DatabaseConfigSection.ConnectionName);
			ProcessConfigurationCollection wProcessConfigurationCollection = new ProcessConfigurationCollection();

			using (System.Data.DataSet wProcessData = wBPConfig.ExecuteDataSet("Process_s_All"))
			{
				foreach (System.Data.DataRow wProcessRow in wProcessData.Tables[0].Rows)
				{
					ProcessConfiguration wProcessConfiguration = GetProcessConfigurationFromRow(wProcessRow);
					wProcessConfigurationCollection.Add(wProcessConfiguration);
				}

			}

			return wProcessConfigurationCollection;
		}

		/// <summary>
		/// Almacena la configuración de un proceso de negocio.
		/// </summary>
		/// <param name="pProcessConfiguration">Configuración del proceso de negocio.</param>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		public void SetProcessConfiguration(ProcessConfiguration pProcessConfiguration)
		{
			Datablock.Database wBPConfig = Datablock.DatabaseFactory.CreateDatabase(_DatabaseConfigSection.ConnectionName);

			using (System.Data.Common.DbCommand wCmd = wBPConfig.GetStoredProcCommand("Process_u"))
			{
				wBPConfig.AddInParameter(wCmd, "Name", System.Data.DbType.String, pProcessConfiguration.Name);
				wBPConfig.AddInParameter(wCmd, "Description", System.Data.DbType.String, pProcessConfiguration.Description);
				wBPConfig.AddInParameter(wCmd, "Handler", System.Data.DbType.String, pProcessConfiguration.Handler);
				wBPConfig.AddInParameter(wCmd, "Request", System.Data.DbType.String, pProcessConfiguration.Request);
				wBPConfig.AddInParameter(wCmd, "Response", System.Data.DbType.String, pProcessConfiguration.Response);
				wBPConfig.AddInParameter(wCmd, "Available", System.Data.DbType.String, pProcessConfiguration.Available);
				wBPConfig.AddInParameter(wCmd, "Audit", System.Data.DbType.String, pProcessConfiguration.Audit);
				wBPConfig.AddInParameter(wCmd, "ProcessTransactionalBehaviour", System.Data.DbType.String, Enum.GetName(typeof(TransactionalBehaviour), pProcessConfiguration.ProcessTransactionalBehaviour));
				wBPConfig.AddInParameter(wCmd, "ProcessIsolationLevel", System.Data.DbType.String, Enum.GetName(typeof(IsolationLevel), pProcessConfiguration.ProcessIsolationLevel));
				wBPConfig.AddInParameter(wCmd, "Timeout", System.Data.DbType.Int32, pProcessConfiguration.Timeout);
                wBPConfig.AddInParameter(wCmd, "Cacheable", System.Data.DbType.Int32, pProcessConfiguration.Cacheable);

				int wAffected = wBPConfig.ExecuteNonQuery(wCmd);

				if (wAffected == 0)
				{
					throw new Exception("El proceso " + pProcessConfiguration.Name + " no se actualizó por no encontrarse configurado.");
				}

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
			Datablock.Database wBPConfig = Datablock.DatabaseFactory.CreateDatabase(_DatabaseConfigSection.ConnectionName);

			using (System.Data.Common.DbCommand wCmd = wBPConfig.GetStoredProcCommand("Process_i"))
			{
				wBPConfig.AddInParameter(wCmd, "Name", System.Data.DbType.String, pProcessConfiguration.Name);
				wBPConfig.AddInParameter(wCmd, "Description", System.Data.DbType.String, pProcessConfiguration.Description);
				wBPConfig.AddInParameter(wCmd, "Handler", System.Data.DbType.String, pProcessConfiguration.Handler);
				wBPConfig.AddInParameter(wCmd, "Request", System.Data.DbType.String, pProcessConfiguration.Request);
				wBPConfig.AddInParameter(wCmd, "Response", System.Data.DbType.String, pProcessConfiguration.Response);
				wBPConfig.AddInParameter(wCmd, "Available", System.Data.DbType.String, pProcessConfiguration.Available);
				wBPConfig.AddInParameter(wCmd, "Audit", System.Data.DbType.String, pProcessConfiguration.Audit);
				wBPConfig.AddInParameter(wCmd, "ProcessTransactionalBehaviour", System.Data.DbType.String, Enum.GetName(typeof(TransactionalBehaviour), pProcessConfiguration.ProcessTransactionalBehaviour));
				wBPConfig.AddInParameter(wCmd, "ProcessIsolationLevel", System.Data.DbType.String, Enum.GetName(typeof(IsolationLevel), pProcessConfiguration.ProcessIsolationLevel));
				wBPConfig.AddInParameter(wCmd, "Timeout", System.Data.DbType.Int32, pProcessConfiguration.Timeout);
                wBPConfig.AddInParameter(wCmd, "Cacheable", System.Data.DbType.Int32, pProcessConfiguration.Cacheable);

				wBPConfig.ExecuteNonQuery(wCmd);
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
			Datablock.Database wBPConfig = Datablock.DatabaseFactory.CreateDatabase(_DatabaseConfigSection.ConnectionName);

			using (System.Data.Common.DbCommand wCmd = wBPConfig.GetStoredProcCommand("Process_d"))
			{
				wBPConfig.AddInParameter(wCmd, "Name", System.Data.DbType.String, pProcessName);

				int wAffected = wBPConfig.ExecuteNonQuery(wCmd);

				if (wAffected == 0)
				{
					throw new Exception("El proceso " + pProcessName + " no se eliminó por no encontrarse configurado.");
				}
			}
		}

		#endregion

		#region < Private methods >

		/// <summary>
		/// Mapea los campos de una datarow a propiedades de una configuración de proceso.
		/// </summary>
		/// <returns>Configuracion de proceso de negocio.</returns>
		/// <date>2006-02-13T00:00:00</date>
		/// <author>gmedina</author>
		private ProcessConfiguration GetProcessConfigurationFromRow(System.Data.DataRow pProcessRow)
		{
			ProcessConfiguration wProcessConfiguration = new ProcessConfiguration();

			wProcessConfiguration.Name = Convert.ToString(pProcessRow["Name"]);
			wProcessConfiguration.Description = Convert.ToString(pProcessRow["Description"]);
			wProcessConfiguration.Handler = Convert.ToString(pProcessRow["Handler"]);
			wProcessConfiguration.Request = Convert.ToString(pProcessRow["Request"]);
			wProcessConfiguration.Response = Convert.ToString(pProcessRow["Response"]);
			wProcessConfiguration.Available = Convert.ToBoolean(pProcessRow["Available"]);
			wProcessConfiguration.Audit = Convert.ToBoolean(pProcessRow["Audit"]);
			wProcessConfiguration.ProcessTransactionalBehaviour = (TransactionalBehaviour)Enum.Parse(typeof(TransactionalBehaviour), pProcessRow["ProcessTransactionalBehaviour"].ToString());
			wProcessConfiguration.ProcessIsolationLevel = (IsolationLevel)Enum.Parse(typeof(IsolationLevel), pProcessRow["ProcessIsolationLevel"].ToString());
			wProcessConfiguration.Timeout = Convert.ToInt32(pProcessRow["Timeout"]);
            wProcessConfiguration.Cacheable = Convert.ToBoolean(pProcessRow["Cacheable"]);

			return wProcessConfiguration;
		}
		#endregion

	}
}
