using System.Text.Json.Serialization;

namespace Nowfeeds.Infrastructure.ExternalServices.Models
{
	public class WorldNewsApiResponse
	{
		[JsonPropertyName("offset")]
		public int Offset { get; set; }

		[JsonPropertyName("number")]
		public int Number { get; set; }

		[JsonPropertyName("available")]
		public int Available { get; set; }

		[JsonPropertyName("news")]
		public List<News> News { get; set; }
	}

	public class News
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("summary")]
		public string Summary { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }

		[JsonPropertyName("image")]
		public string Image { get; set; }

		[JsonPropertyName("video")]
		public object Video { get; set; }

		[JsonPropertyName("publish_date")]
		public string PublishDate { get; set; }

		[JsonPropertyName("author")]
		public string Author { get; set; }

		[JsonPropertyName("authors")]
		public List<string> Authors { get; set; }

		[JsonPropertyName("language")]
		public string Language { get; set; }

		[JsonPropertyName("category")]
		public string Category { get; set; }

		[JsonPropertyName("source_country")]
		public string SourceCountry { get; set; }

		[JsonPropertyName("sentiment")]
		public double? Sentiment { get; set; }
	}
}
