namespace Nowfeeds.Domain.Values
{
	public class NewsFeed
	{
		public Article[] Articles { get; set; }
	}

	public class Article
	{
		public string Title { get; set; }
		public string Summary { get; set; }
	}
}
