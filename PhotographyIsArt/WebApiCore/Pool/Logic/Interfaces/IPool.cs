using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Pool.Logic.Interfaces
{
	public interface IPool<T>
	{
		T Get();
		void Return(T item);
	}
}
