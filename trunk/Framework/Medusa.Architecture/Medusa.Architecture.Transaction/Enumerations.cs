using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Architecture.Transaction
{
	/// <summary>
	/// Define el comportamiento del ámbito transaccional.
	/// </summary>
	/// <remarks>Los valores de esta enumeración se traducen en los valores propios de la tecnología con que se implementa el soporte transaccional.</remarks>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public enum TransactionalBehaviour
	{
		/// <summary>
		/// Si hay una transacción en curso, se hace uso de dicho ámbito. En caso contrario no transacciona.
		/// </summary>
		Support,
		/// <summary>
		/// El proceso transacciona. Si hay un ámbito transaccional ya abierto, utiliza el existente, en caso contrario crea una nueva transaccion.
		/// </summary>
		Required,
		/// <summary>
		/// Siempre se crea una nueva transacción.
		/// </summary>
		RequiresNew,
		/// <summary>
		/// No transacciona, todas las operaciones se hacen sin estar en un ámbito transaccional. 
		/// </summary>
		Suppres
	}

	/// <summary>
	/// Especifica el nivel de aislamiento de la transacción.
	/// </summary>
	/// <remarks>Los valores de esta enumeración se traducen en los valores propios de la tecnología con que se implementa el soporte transaccional.</remarks>
	/// <date>2006-02-07T00:00:00</date>
	/// <author>gmedina</author>
	public enum IsolationLevel
	{
		/// <summary>
		/// Los cambios pendientes de transacciones más aisladas no puede ser sobreescritos.
		/// </summary>
		Chaos,
		/// <summary>
		/// Los datos volátites no puede ser leidos durante la transacción, pero pueden ser modificados.
		/// </summary>
		ReadCommitted,
		/// <summary>
		/// Los datos volátines pueden ser leidos y modificados durante la transacción.
		/// </summary>
		ReadUncommitted,
		/// <summary>
		/// Los datos volátiles pueden ser leidos pero no modificados durante la transación. Nuevos datos pueden ser creados.
		/// </summary>
		RepeatableRead,
		/// <summary>
		/// Los datos volátiles pueden ser leidos pero no modificados, y no es posible crear nuevos datos durante la transacción.
		/// </summary>
		Serializable,
		/// <summary>
		/// Los datos volátiles pueden ser leidos. Antes de modificarse datos,  se verifica que otra transacción los haya cambiado luego de haber sido leidos. Si los datos se actualizaron, se levanta una excepción.
		/// </summary>
		Snapshot
	}
}
