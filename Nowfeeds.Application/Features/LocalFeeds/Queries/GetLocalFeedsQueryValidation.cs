using FluentValidation;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsQueryValidation : AbstractValidator<GetLocalFeedsQuery>
	{
		public GetLocalFeedsQueryValidation()
		{
			RuleFor(b => b.City.Length).GreaterThan(2).WithMessage("City name should have at least 3 characters");
		}
	}
}
