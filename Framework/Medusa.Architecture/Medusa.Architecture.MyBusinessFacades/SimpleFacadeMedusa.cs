using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Medusa.Architecture.HelpersFunctions;

namespace Medusa.Architecture.MyBusinessFacades
{
    public class SimpleFacadeMedusa
    {
        public SimpleFacadeMedusa()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        /// <summary>
        /// Ejecuta un servicio de negocio.
        /// </summary>
        /// <param name="pServiceName">Nombre del servicio de negocio.</param>
        /// <param name="pData">XML con datos de entrada para la ejecución del servicio.</param>
        /// <param name="pContextInformation">XML con información de contexto para la ejecución del servicio.</param>
        /// <returns>XML con el resultado de la ejecución del servicio.</returns>
        /// <date>2006-02-07T00:00:00</date>
        /// <author>gmedina</author>
        public string ExecuteService(string pServiceName, string pData)
        {
            //try
            //{
            //    FacadeHelper wHelper = new FacadeHelper();
            //    string wResult;

            //    //ServiceConfiguration wServiceConfiguration = wHelper.GetServiceConfiguration(pServiceName);
            //    ProcessConfiguration wServiceConfiguration = wHelper.GetServiceConfiguration(pServiceName);

            //    // Validación de disponibilidad del servicio.
            //    wHelper.ValidateAvailability(wServiceConfiguration);

            //    if (wServiceConfiguration.Cacheable == true)
            //    {
            //        SetPrimitiveCache();
            //        wResult = GetDataById(pData, wServiceConfiguration);
            //    }
            //    else
            //    {
            //        // Ejecución del servicio.
            //        wResult = wHelper.RunTransactionalProcess(pData, wServiceConfiguration);
            //    }

            //    // Caching del servicio.
            //    return wResult;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}



            //try
            //{
            //    //XML
            //    //Enviando solo el proceso el XML (o una BD) me indican que instanciar y por donde ir
            //    XmlDocument oDom;
            //    oDom = new XmlDocument();
            //    oDom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "AppConfigWS.xml");
            //    string NameSpaceBC = oDom.SelectSingleNode("/AppConfig/Procesos/Proceso[Descripcion='" + Proceso + "']/NameSpaceBC").InnerText.Trim();
            //    string ClaseBC = oDom.SelectSingleNode("/AppConfig/Procesos/Proceso[Descripcion='" + Proceso + "']/ClaseBC").InnerText.Trim();
            //    string Metodo = oDom.SelectSingleNode("/AppConfig/Procesos/Proceso[Descripcion='" + Proceso + "']/Metodo").InnerText.Trim();
            //    string NameSpaceBE = oDom.SelectSingleNode("/AppConfig/Procesos/Proceso[Descripcion='" + Proceso + "']/NameSpaceBE").InnerText.Trim();
            //    string ClaseBE = oDom.SelectSingleNode("/AppConfig/Procesos/Proceso[Descripcion='" + Proceso + "']/ClaseBE").InnerText.Trim();
            //    //XML

            //    //Cargo el assembly de la BC
            //    Assembly AssProceso = Assembly.Load(NameSpaceBC);
            //    //Obtengo el tipo de la BC
            //    object MiObjeto = AssProceso.CreateInstance(NameSpaceBC + "." + ClaseBC);
            //    //Cargo el assembly de la BE
            //    Assembly miAssTipo = Assembly.Load(NameSpaceBE);
            //    //obtego el tipo de la entidad (de la BE)
            //    Type tipoEntidad = miAssTipo.GetType(NameSpaceBE + "." + ClaseBE);
            //    //Creo un array de Objetos para pasar como parametro
            //    Object[] o = new Object[1];
            //    //Deserializo el XML en un objeto entidad
            //    o[0] = DeserializeFromXml(tipoEntidad, objetoXML);
            //    //Hago la llamada al proceso.
            //    return (DataSet)MiObjeto.GetType().GetMethod(Metodo).Invoke(MiObjeto, o);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}


            //XML
            //Enviando solo el proceso el XML (o una BD) me indican que instanciar y por donde ir
            XmlDocument oDom;
            oDom = new XmlDocument();
            oDom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "AppConfigWS.xml");
            string AReq = oDom.SelectSingleNode("/AppConfig/Servicios/Servicio[Descripcion='" + pServiceName + "']/AssemblyRequest").InnerText.Trim();
            string CReq = oDom.SelectSingleNode("/AppConfig/Servicios/Servicio[Descripcion='" + pServiceName + "']/ClaseRequest").InnerText.Trim();
            //string ARes = oDom.SelectSingleNode("/AppConfig/Servicios/Servicio[Descripcion='" + pServiceName + "']/AssemblyResponse").InnerText.Trim();
            //string CRes = oDom.SelectSingleNode("/AppConfig/Servicios/Servicio[Descripcion='" + pServiceName + "']/ClaseResponse").InnerText.Trim();
            string ASer = oDom.SelectSingleNode("/AppConfig/Servicios/Servicio[Descripcion='" + pServiceName + "']/AssemblyHandler").InnerText.Trim();
            string CSer = oDom.SelectSingleNode("/AppConfig/Servicios/Servicio[Descripcion='" + pServiceName + "']/ClaseHandler").InnerText.Trim();
            //XML

            //por desgracia no transacciona porque no mandamos una excepcion. deberan ver como solucionarlo.
            //no es grave, pero no llegué a ver como hacerlo. saludos. gerardo.
            //obtengo el tipo de request
            Type wTipoEntidad = ReflectionFunctions.CreateType(CReq + ", " + AReq);
            //Creo un array de Objetos para pasar como parametro.
            Object[] o = new Object[1];
            o[0] = SerializationFunctions.Deserialize(wTipoEntidad, pData);

            // Obtención del Response.
            Type wProcessType = ReflectionFunctions.CreateType(CSer + ", " + ASer);
            Object wProcessInstance = Activator.CreateInstance(wProcessType);
            Object wResponse = wProcessType.GetMethod("Execute").Invoke(wProcessInstance, o);

            return SerializationFunctions.Serialize(wResponse);



        }
    }
}
