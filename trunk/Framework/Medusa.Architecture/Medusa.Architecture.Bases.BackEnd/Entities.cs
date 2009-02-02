using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Medusa.Architecture.Bases.BackEnd
{
	/// <summary>
	/// Clase base que representa una lista genérica de entidades.
	/// </summary>
	public abstract class Entities<T> : List<T>
		where T : Entity
	{
	}
}
