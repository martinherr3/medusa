using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Medusa.Architecture.Transaction;

namespace Medusa.Architecture.ProcessManagement
{
	/// <summary>
	/// Collección de configuraciones de procesos de negocio.
	/// </summary>
	/// <remarks>
	/// Permite búsquedas indexadas por nombre de proceso (<see cref="ProcessConfiguration.Name"/>).
	/// </remarks>
	/// <date>2006-02-10T00:00:00</date>
	/// <author>gmedina</author>
	public class ProcessConfigurationCollection : KeyedCollection<string, ProcessConfiguration>
	{
		/// <summary>
		/// Devuelve la propiedad a usar como clave.
		/// </summary>
		/// <param name="pItem">La configuración de proceso de la que se quiere extraer el valor clave.</param>
		/// <date>2006-02-10T00:00:00</date>
		/// <author>gmedina</author>
		protected override string GetKeyForItem(ProcessConfiguration pItem)
		{
			return pItem.Name;
		}
	}

	/// <summary>
	/// Clase que contiene la configuración de un proceso de negocio.
	/// </summary>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public class ProcessConfiguration
	{
		/// <summary>
		/// Nombre del proceso de negocio.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		/// <summary>
		/// Descripción del proceso de negocio.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		/// <summary>
		/// Nombre completo de la clase del proceso y nombre del assembly.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public string Handler
		{
			get { return _Handler; }
			set { _Handler = value; }
		}

		/// <summary>
		/// Nombre completo del request del proceso y nombre del assembly.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public string Request
		{
			get { return _Request; }
			set { _Request = value; }
		}

		/// <summary>
		/// Nombre completo del response del proceso y nombre del assembly.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public string Response
		{
			get { return _Response; }
			set { _Response = value; }
		}

		/// <summary>
		/// Indica si el proceso está disponible para ser ejecutedo.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public bool Available
		{
			get { return _Available; }
			set { _Available = value; }
		}

		/// <summary>
		/// Indica si la ejecución del proceso debe ser auditadao.el proceso está disponible para ser ejecutedo.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public bool Audit
		{
			get { return _Audit; }
			set { _Audit = value; }
		}

		/// <summary>
		/// Opciones de comportamiento del ámbito transaccional.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public TransactionalBehaviour ProcessTransactionalBehaviour
		{
			get { return _ProcessTransactionalBehaviour; }
			set { _ProcessTransactionalBehaviour = value; }
		}


		/// <summary>
		/// Nivel de aislamiento de la transacción.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public IsolationLevel ProcessIsolationLevel
		{
			get { return _ProcessIsolationLevel; }
			set { _ProcessIsolationLevel = value; }
		}


		/// <summary>
		/// Tiempo máximo de espera para transacción en segundos.
		/// </summary>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public int Timeout
		{
			get { return _Timeout; }
			set { _Timeout = value; }
		}

        /// <summary>
        /// Indica si el resultado del proceso de almacena en caché.
        /// </summary>
        /// <date>2006-10-27T00:00:00</date>
        /// <author>jlousararian</author>
        public bool Cacheable
        {
            get { return _Cacheable; }
            set { _Cacheable = value; }
        }

		string _Name;
		string _Description;
		string _Handler;
		string _Request;
		string _Response;
		bool _Available;
		bool _Audit;
		TransactionalBehaviour _ProcessTransactionalBehaviour;
		IsolationLevel _ProcessIsolationLevel;
		int _Timeout;
        bool _Cacheable;

	}
}
