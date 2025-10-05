using System.ComponentModel;

namespace Nowfeeds.Application.Common.Enums
{
	public enum ErrorCodeStatus
	{
		[Description("City Not Found")]
		CityNotFound = 1,
		[Description("General Error")]
		GeneralError = 2
	}
}
