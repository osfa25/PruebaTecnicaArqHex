using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaArqHex.Data;
using PruebaTecnicaShared;

namespace PruebaTecnicaArqHex.Models.Products.infrastructure
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var allProducts = dbContext.Products.ToList();
            return Ok(allProducts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = dbContext.Products.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDTO productsAddDTO)
        {
            var productEntity = new domain.entities.Products()
            {
                name = productsAddDTO.name,
                price = productsAddDTO.price,
                stock = productsAddDTO.stock
            };

            dbContext.Products.Add(productEntity);
            dbContext.SaveChanges();
            return Ok(productEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateProduct(Guid id, ProductDTO productsUpdateDTO)
        {
            var product = dbContext.Products.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            product.name = productsUpdateDTO.name;
            product.price = productsUpdateDTO.price;
            product.stock = productsUpdateDTO.stock;

            dbContext.SaveChanges();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = dbContext.Products.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}