using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowfeeds.Application.Interfaces
{
	public interface IWeatherService
	{
		Task<string> GetWeatherAsync(string location);
	}
}
