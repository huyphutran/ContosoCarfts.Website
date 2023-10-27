using ContosoCarfts.Website.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCarfts.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {

            this.ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }


        [HttpGet]

        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        [Route("Rate")]
        [HttpGet]

        public ActionResult Get(
            [FromQuery] string ProductId,
            [FromQuery] int rating)
        {
            ProductService.GetRatings(ProductId, rating);
            return Ok();
        }
    }
}