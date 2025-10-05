using FluentValidation;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsQueryValidation : AbstractValidator<GetLocalFeedsQuery>
	{
		public GetLocalFeedsQueryValidation()
		{
			RuleFor(b => b.City).NotEmpty().WithMessage("City is required");
		}
	}
}
