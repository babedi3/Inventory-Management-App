using System.Linq;
using InventoryManagement.Services.Product;
using InventoryManagement.Web.ViewModels;
using InventoryManagement.Web.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Web.Controllers {

    [ApiController]
    public class ProductController : ControllerBase {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService) {
            _logger = logger;
            _productService = productService;
        }
        
        [HttpPost("/api/product")]
         public ActionResult AddProduct([FromBody] ProductModel product) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Adding product");
            var newProduct = ProductMapper.SerializeProductModel(product);
            var newProductResponse = _productService.CreateProduct(newProduct);
            return Ok(newProductResponse);
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct() {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products
                .Select(ProductMapper.SerializeProductModel);
            return Ok(productViewModels);
        }

        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation("Archiving product");
            var archiveResult = _productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }
    }
}