using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCarfts.Website.Models

{
    public class Product
    {
        public string id { get; set; }
        public string maker { get; set; }
        [JsonPropertyName("img")]
        public string image { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int[] Rating { get; set; }


        public string toString() => JsonSerializer.Serialize<Product>(this);
    }
}
