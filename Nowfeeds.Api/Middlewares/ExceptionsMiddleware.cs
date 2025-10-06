using FluentValidation;
using Nowfeeds.Api.Models;
using System.Net;

namespace Nowfeeds.Api.Middlewares
{
	public class ExceptionsMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionsMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ILogger<ExceptionsMiddleware> logger)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				var errorMessages = ex.Errors.Select(e => e.ErrorMessage);

				ApiResponseModel apiResponseModel = new ApiResponseModel
				{
					Success = false,
					Message = string.Join("; ", errorMessages),
				};

				await context.Response.WriteAsJsonAsync(apiResponseModel);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An unhandled exception occurred.");
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
		}
	}
}
