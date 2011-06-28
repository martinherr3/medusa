using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using log4net;
using NHibernate;
using Mds.Architecture.Data;
using System.Collections;

namespace Mds.Architecture.Core.Context
{
    public class MdsWcfContext : IExtension<OperationContext>
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ISession nHibernateSession;
        private const string SESSION_KEY = "CONTEXT_SESSIONS";

        public MdsWcfContext()
        {
            Items = new Hashtable();
        }

        public ISession NHibernateSession
        {
            get { return nHibernateSession; }
            set { nHibernateSession = value; }
        }

        /// <summary>
        /// Coleccion para almacenar informacion en el contexto
        /// </summary>
        public IDictionary Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Retorna el contexto WCF actual, si no existe retorna null.
        /// </summary>
        public static MdsWcfContext Current
        {
            get
            {
                try
                {
                    log.Info("Getting current Mudusa WCF custom context...");
                    return OperationContext.Current.Extensions.Find<MdsWcfContext>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #region IExtension<OperationContext> Members

        public void Attach(OperationContext owner)
        {
            //Como inicializo la session?
            //La session se inicializa cuando algun dao es invocado y se almacena en el hashtable del contexto.
            //El contexto puede ser WcfContext, HttpContext o CallContext. Todo esto lo maneja la clase NHibernateSessionManager.
        }

        public void Detach(OperationContext owner)
        {
            try
            {
                //Cerrar session, actualmente no se usa
                if (NHibernateSession != null)
                {
                    log.Info("Closing NHibernate session stored in WCF Operation context...");
                    NHibernateSession.Flush();
                    NHibernateSession.Close();
                }

                //Cerrar sessiones almacenadas en el hashtable, esto se hizo para integrarlo con el 
                //manejo de sessiones de NHibernate actual en NHibernateSessionManager
                if (Items[SESSION_KEY] != null)
                {
                    Hashtable sessions = (Hashtable)Items[SESSION_KEY];

                    log.Info("Iterating through NHibernate session hashtable stored in WCF Operation context...");
                    //Close sessions
                    foreach (DictionaryEntry sessionEntry in sessions)
                    {
                        string key = (string)sessionEntry.Key;
                        ISession session = (ISession)sessionEntry.Value;
                        log.Info("Closing NHibernate session stored in WCF Operation context hashtable...");
                        log.Info("...Session factory config path: " + key);
                        if (session != null && session.IsOpen)
                        {
                            session.Flush();
                            session.Close();
                        }
                    }

                    //Clean session hashtable
                    sessions.Clear();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error closing the NHiberante sessions", ex);
            }
        }

        #endregion
    }
}
