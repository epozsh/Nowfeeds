using Microsoft.AspNetCore.Mvc;

namespace Nowfeeds.Api.Controllers
{
	public class FeedsController : Controller
	{
		public async Task<string> GetLocalFeeds([FromQuery] int location)
		{
			return string.Empty;
		}
	}
}
