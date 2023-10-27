using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCarfts.Website.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true  
                    });
            }
        }

        public void GetRatings(string ProductId, int rating)
        {
            var products = GetProducts();

            var query = products.First(x => x.id == ProductId);

            if (query.Rating == null)
            {

                query.Rating = new int[] { rating };
            }
            else
            {
                var ratings = query.Rating.ToList();
                ratings.Add(rating);
                query.Rating = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true,
                    }), products
                   );
            }

        }


    }
}