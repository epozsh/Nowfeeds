using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Nowfeeds.Application.Decorators
{
	public class ValidationDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IRequestHandler<TRequest, TResponse> _decorated;
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationDecorator(IRequestHandler<TRequest, TResponse> decorated, IEnumerable<IValidator<TRequest>> validators)
		{
			_decorated = decorated;
			_validators = validators;

		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

			List<ValidationFailure> failures = _validators.Select(v => v.Validate(context))
					.SelectMany(result => result.Errors)
					.Where(f => f != null).ToList();

			if (failures.Count != 0) throw new ValidationException(failures);

			return await _decorated.Handle(request, cancellationToken);
		}
	}
}
