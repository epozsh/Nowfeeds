using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowfeeds.Application.Interfaces
{
	public interface INewsFeedService
	{
		Task<string> GetNewsFeedsAsync(string location, string category);
	}
}
