using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Medusa.Base.Utils
{
    public class ContainerHelper : IContainerAccessor, IDisposable
    {
        /// <summary>
        /// Gets instantiated on <see cref="Container" />.
        /// </summary>
        private static IWindsorContainer windsorContainer;

        public static IWindsorContainer WindsorContainer()
        {
            if (windsorContainer != null)
            {
                return windsorContainer;
            }
            else
            {
                // Create the Windsor Container for IoC.
                // Supplying "XmlInterpreter" as the parameter tells Windsor 
                // to look at web.config for any necessary configuration.
                windsorContainer = new WindsorContainer(new XmlInterpreter());
                return windsorContainer;
            }
        }

        #region IContainerAccessor Members

        public IWindsorContainer Container
        {
            get { return windsorContainer; }
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            windsorContainer.Dispose();
        }

        #endregion
    }
}
