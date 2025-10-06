using Nowfeeds.Application.Common.Enums;

namespace Nowfeeds.Application.Common
{
	public abstract class Result
	{
		public ErrorCodeStatus? ErrorCodeStatus { get; set; }
	}
}
