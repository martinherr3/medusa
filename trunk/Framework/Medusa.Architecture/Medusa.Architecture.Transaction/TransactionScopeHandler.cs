using System;
using System.Collections.Generic;
using System.Text;
using SysTrans = System.Transactions;


namespace Medusa.Architecture.Transaction
{


	/// <summary>
	/// Clase para encapsular manejo de transacciones.
	/// </summary>
	/// <remarks>
	/// Encapsula la instanciación de los tipos necesarios para llevar a cabo una transacción, independizando el resto de la aplicación de la tecnología utilizada a su efecto.
	/// </remarks>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public sealed class TransactionScopeHandler: IDisposable
	{

		/// <summary>
		/// Comportamiento del ámbito transaccional .
		/// </summary>
		TransactionalBehaviour _TransactionalBehaviour;

		/// <summary>
		/// Nivel de aislamiento.
		/// </summary>
		IsolationLevel _IsolationLevel;

		/// <summary>
		/// Intervalo de tiempo para la ejecución de la transacción.
		/// </summary>
		TimeSpan _Timeout;

		/// <summary>
		/// Ámbito transaccional.
		/// </summary>
		SysTrans.TransactionScope _TransactionScope;

		/// <summary>
		/// Flag que indica si la instancia ya fue pasada a disponibilidad o no.
		/// </summary>
		bool _Disposed = false;


		/// <summary>
		/// Traduce el valor que define el comportamiento a un valor propio de la tecnología con que se implementa el soporte transaccional.
		/// </summary>
		/// <remarks></remarks>
		/// <returns>Opción del ambiente transaccional.</returns>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		private SysTrans.TransactionScopeOption GetTransactionScopeOption()
		{
			switch (_TransactionalBehaviour)
			{
				case TransactionalBehaviour.Support:
					{
					    throw new Exception("El comportamiento no está soportado por la actual implementación");
					}
				case TransactionalBehaviour.Required:
					{
						return SysTrans.TransactionScopeOption.Required;
					}
				case TransactionalBehaviour.RequiresNew:
					{
						return SysTrans.TransactionScopeOption.RequiresNew;
					}
				case TransactionalBehaviour.Suppres:
					{
						return SysTrans.TransactionScopeOption.Suppress;
					}
				default:
					{
                        throw new Exception("El comportamiento no está soportado por la actual implementación");
					}
			}
		}


		/// <summary>
		/// Traduce los valors que definen las opciones del ámbito de la transacción a un valor propio de la tecnología con que se implementa el soporte transaccional.
		/// </summary>
		/// <remarks></remarks>
		/// <returns>Opción del ambiente transaccional.</returns>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		private SysTrans.TransactionOptions GetTransactionOptions()
		{
			SysTrans.TransactionOptions wResult = new SysTrans.TransactionOptions();

			switch (_IsolationLevel)
			{
				case IsolationLevel.Chaos:
					{
						wResult.IsolationLevel = SysTrans.IsolationLevel.Chaos;
						break;
					}
				case IsolationLevel.ReadCommitted:
					{
						wResult.IsolationLevel = SysTrans.IsolationLevel.ReadCommitted;
						break;
					}
				case IsolationLevel.ReadUncommitted:
					{
						wResult.IsolationLevel = SysTrans.IsolationLevel.ReadUncommitted;
						break;
					}
				case IsolationLevel.RepeatableRead:
					{
						wResult.IsolationLevel = SysTrans.IsolationLevel.RepeatableRead;
						break;
					}
				case IsolationLevel.Serializable:
					{
						wResult.IsolationLevel = SysTrans.IsolationLevel.Serializable;
						break;
					}
				case IsolationLevel.Snapshot:
					{
						wResult.IsolationLevel = SysTrans.IsolationLevel.Snapshot;
						break;
					}
				default:
					{
                        throw new Exception("El comportamiento no está soportado por la actual implementación");
					}
			}

			wResult.Timeout = _Timeout;			

			return wResult;
		}
		
		/// <summary>
		/// Constructor por defecto.
		/// </summary>
		/// <remarks>
		/// Levanta configuración por defecto de los settings.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public TransactionScopeHandler()
		{
			ConfigureScope((TransactionalBehaviour)Enum.Parse(typeof(TransactionalBehaviour), Properties.Settings.Default.TransactionalBehaviour), (IsolationLevel)Enum.Parse(typeof(IsolationLevel), Properties.Settings.Default.IsolationLevel), Properties.Settings.Default.Timeout);
		}


		/// <summary>
		/// Constructor que recibe parámetros de inicialización para el ámbito de la transacción.
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <param name="pTransactionalBehaviour">Comportamiento del ámbito transaccional.</param>
		/// <param name="pIsolationLevel">Nivel de aislamiento de la transacción.</param>
		/// <param name="pTimeOut">Intervalo de tiempo que puede durar la transacción.</param>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public TransactionScopeHandler(TransactionalBehaviour pTransactionalBehaviour, IsolationLevel pIsolationLevel, TimeSpan pTimeOut)
		{
			ConfigureScope(pTransactionalBehaviour, pIsolationLevel, pTimeOut);
		}

		/// <summary>
		/// Configura los parámetros de inicialización para el alcance de la transacción.
		/// </summary>
		/// <remarks>
		/// No inicializa el ámbito transaccional, solamente setea valores de configuración que serán utilizados por <see cref="InitScope"/>.
		/// </remarks>
		/// <param name="pTransactionalBehaviour">Comportamiento del ámbito transaccional.</param>
		/// <param name="pIsolationLevel">Nivel de aislamiento de la transacción.</param>
		/// <param name="pTimeOut">Intervalo de tiempo que puede durar la transacción.</param>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public void ConfigureScope(TransactionalBehaviour pTransactionalBehaviour, IsolationLevel pIsolationLevel, TimeSpan pTimeOut)
		{
			_TransactionalBehaviour = pTransactionalBehaviour ;
			_IsolationLevel = pIsolationLevel;
			_Timeout = pTimeOut;
		}
		
		/// <summary>
		/// Inicializa el ámbito de transacción.
		/// </summary>
		/// <remarks>
		/// Hace uso de la configuración especificada por <see cref="ConfigureScope"/>.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public void InitScope()
		{
			CheckDisposed();

			if (_TransactionScope != null)
			{
                throw new Exception("El alcance de transacción ya ha sido inicializado");
			}

			_TransactionScope = CreateScope();
		}

		private SysTrans.TransactionScope CreateScope()
		{
			SysTrans.TransactionScope wResult = new SysTrans.TransactionScope(GetTransactionScopeOption(), GetTransactionOptions());
			return wResult;
		}

		/// <summary>
		/// Indica que se debe completar la ejecución de la transacción.
		/// </summary>
		/// <remarks>
		/// Completa la ejecución de la transacción y pasa a disposición el ámbito creado en <see cref="InitScope"/>.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public void Complete()
		{
			CheckDisposed();
			CheckInitialized();
			_TransactionScope.Complete();
			DisposeScope();
		}


		/// <summary>
		/// Aborta las actualizaciones realizadas dentro del alcance de la transacción.
		/// </summary>
		/// <remarks>
		/// Al abortar la ejecución, se pasa a disposición el ámbito creado en <b>InitScope</b>.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public void Abort()
		{
			CheckDisposed();
			CheckInitialized();
			DisposeScope();
		}

		/// <summary>
		/// Verifica si ya se ha inicializado el alcance de la transacción.
		/// </summary>
		/// <remarks>
		/// Si no se inicializó el ámbito, se dispara una excepción. Para inicializar el ámbito transaccional ejecutar <see cref="InitScope"/>.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		private void CheckInitialized()
		{
			if (_TransactionScope == null)
			{
			    throw new Exception("No hay un alcance de transacción inicializado.");
			}
		}

		/// <summary>
		/// Verifica si ya se ha pasado a disponibilidad el alcance de la transacción.
		/// </summary>
		/// <remarks>
		/// Si la instancia ya fue pasada a disponibilidad, dispara una excepción.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		private void CheckDisposed()
		{
			if (_Disposed)
			{
				throw new System.ObjectDisposedException(this.GetType().FullName);
			}
		}

		/// <summary>
		/// Pasa el alcance de transacción a disponibilidad y libera la referencia a este.
		/// </summary>
		/// <remarks>
		/// Al pasar a disponibilidad el ámbito, se aborta implícitamente la transacción. Para aceptar los cambios realizados, se debe ejecutar previamente <see cref="Complete"/>.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		private void DisposeScope()
		{
			_TransactionScope.Dispose();
			_TransactionScope = null;
		}

		#region IDisposable Members

		/// <summary>
		/// Implementacion de IDisposable.Dispose.
		/// </summary>
		/// <remarks>
		/// En caso de que haya un ambiente transaccional activo, se lo pasa a disposición abortando la transacción.
		/// </remarks>
		/// <date>2006-02-07T00:00:00</date>
		/// <author>gmedina</author>
		public void Dispose()
		{
			if (!_Disposed)
			{
				_Disposed = true;

				if (_TransactionScope != null)
				{
					DisposeScope();
				}
			}
		}

		#endregion
	}
}
