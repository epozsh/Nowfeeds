using System.Text.Json.Serialization;

namespace Nowfeeds.Infrastructure.ExternalServices.Models
{
	public class RecentTweetsApiResponse
	{
		[JsonPropertyName("data")]
		public List<Datum> Data { get; set; }

		[JsonPropertyName("meta")]
		public Meta Meta { get; set; }
	}

	public class Datum
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("edit_history_tweet_ids")]
		public List<string> EditHistoryTweetIds { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }
	}

	public class Meta
	{
		[JsonPropertyName("newest_id")]
		public string NewestId { get; set; }

		[JsonPropertyName("oldest_id")]
		public string OldestId { get; set; }

		[JsonPropertyName("result_count")]
		public int ResultCount { get; set; }
	}
}
