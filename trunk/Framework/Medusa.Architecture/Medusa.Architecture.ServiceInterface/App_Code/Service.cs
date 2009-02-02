using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
//using Medusa.Architecture.BusinessFacades;
using Medusa.Architecture.MyBusinessFacades;


/// <summary>
/// Provee acceso a los services de negocio.
/// </summary>
/// <remarks>
/// Esta intefaz de servicio es capaz de ejecutar todos los servicios de negocio. No se requiere la instanciacion de otras interfaces de servicio.
/// </remarks>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{

    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Ejecuta un service de negocio.
    /// </summary>
    /// <param name="pServiceName">Nombre del service.</param>
    /// <param name="pData">XML con datos de entrada para la ejecución del servicio.</param>
    /// <returns>XML con datos de salida del servicio.</returns>
    [WebMethod]
    public string ExecuteService(string pServiceName, string pData)
    {
        //SimpleFacade wSimpleFacade = new SimpleFacade();
        //string wResult = wSimpleFacade.ExecuteService(pServiceName, pData, string.Empty);
        //return wResult;

        SimpleFacadeMedusa wSimpleFacade= new SimpleFacadeMedusa();
        string wResult = wSimpleFacade.ExecuteService(pServiceName, pData);
        return wResult; 
    }
}