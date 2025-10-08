//using Microsoft.AspNetCore.Mvc;
//using Quick_Application2.Core.Services.Shop;

//namespace Quick_Application2.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProductController : ControllerBase
//    {
//        private readonly IProductService _productService;

//        public ProductController(IProductService productService)
//        {
//            _productService = productService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllProducts()
//        {
//            var products = await _productService.GetAllProductsAsync();
//            return Ok(products);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetProductById(int id)
//        {
//            var product = await _productService.GetProductByIdAsync(id);
//            if (product == null)
//                return NotFound();
//            return Ok(product);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
//        {
//            var created = await _productService.CreateProductAsync(productDto);
//            return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
//        }
//    }

//}
