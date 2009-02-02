using System;
using System.IO;
using System.Data;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Collections;
using Medusa.Architecture.Bases.BackEnd;
using Medusa.Architecture.BusinessFacades.Utils;
using Medusa.Architecture.ProcessManagement;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Medusa.Architecture.BusinessFacades
{

	/// <summary>
	/// Fachada de servicio por defecto a utilizar por las aplicaciones.
	/// </summary>
	/// <remarks>Esta fachada realiza las siguientes tareas: 
	/// 
	/// <list type="number">
	/// <item>Audita la ejecución del servicio.</item>
	/// <item>Provee seguridad de acceso al servicio.</item>
	/// <item>Verifica que el servicio esté disponible para ser ejecutado.</item>
	/// <item>Ejecuta el servicio a través de MSDTC en caso de ser necesario.</item>
	/// </list>
	/// </remarks>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
    public sealed class SimpleFacade : IBusinessFacade
    {
        #region < IBusinessFacade members >

        /// <summary>
		/// Ejecuta un servicio de negocio.
		/// </summary>
		/// <param name="pServiceName">Nombre del servicio de negocio.</param>
		/// <param name="pData">XML con datos de entrada para la ejecución del servicio.</param>
		/// <param name="pContextInformation">XML con información de contexto para la ejecución del servicio.</param>
		/// <returns>XML con el resultado de la ejecución del servicio.</returns>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public string  ExecuteService(string pServiceName, string pData, string pContextInformation)
        {
            try
            {
				FacadeHelper wHelper = new FacadeHelper();
				string wResult;

				//ServiceConfiguration wServiceConfiguration = wHelper.GetServiceConfiguration(pServiceName);
                ProcessConfiguration wServiceConfiguration = wHelper.GetServiceConfiguration(pServiceName);
                
				// Validación de disponibilidad del servicio.
				wHelper.ValidateAvailability(wServiceConfiguration);

                if (wServiceConfiguration.Cacheable == true)
                {
                    SetPrimitiveCache();
                    wResult = GetDataById(pData, wServiceConfiguration);
                }
                else
                {
                    // Ejecución del servicio.
                    wResult = wHelper.RunTransactionalProcess(pData, wServiceConfiguration);
                }

                // Caching del servicio.
                return wResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region --[Caching]--
        private CacheManager _CacheManager;

        private void SetPrimitiveCache()
        {
            try
            {
                if (_CacheManager == null)
                {
                    _CacheManager = CacheFactory.GetCacheManager();
                }
            }
            catch
            {
                _CacheManager = null;
            }

        }

        private string GetDataById(string pData, ProcessConfiguration wServiceConfiguration)
        {
            string wResult;
            object wAux = _CacheManager.GetData(wServiceConfiguration.Name + pData);

            if (wAux == null)
            {
                wResult = String.Empty;
            }
            else
            {
                wResult = wAux.ToString();
            }
            

            // Si no esta chacheado el servicio, ejecutarlo.
            if (wResult == String.Empty)
            {
                FacadeHelper wHelper = new FacadeHelper();
                wResult = wHelper.RunTransactionalProcess(pData, wServiceConfiguration);

                if (wResult != String.Empty)
                {
                    if (Convert.ToString(wResult) != String.Empty)
                    {
                        _CacheManager.Add(wServiceConfiguration.Name + pData, wResult);
                    }
                }
            }
            return wResult;
        }
        #endregion

	}
}

