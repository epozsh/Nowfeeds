
namespace Nowfeeds.Infrastructure.Exceptions
{
	public class TwitterException : InfrastructureException
	{
		public TwitterException()
		{
		}

		public TwitterException(string? message) : base(message)
		{
		}

		public TwitterException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
