using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Dal.Base.Models
{
	public record BaseModel<T>
	{
		public T Id { get; set; }
	}
}
