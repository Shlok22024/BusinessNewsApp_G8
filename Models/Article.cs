using System.Text.Json.Serialization;

namespace BusinessNewsApp.Models
{
    public class Article
    {
        [JsonPropertyName("source")]
        public Source Source { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Source
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class NewsApiResponse
    {
        [JsonPropertyName("articles")]
        public List<Article> Articles { get; set; }
    }
}