using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly ConDigestDBContext dbContext;

        public ProductItemsController(ConDigestDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ConDigestDBContext DbContext { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from database - Domain models
            var productsDomain = await dbContext.ProductItems.ToListAsync();

            //map domain models to DTOs
            var productsDto = new List<ProductItemDto>();
            foreach (var productDomain in productsDomain)
            {
                productsDto.Add(new ProductItemDto()
                {
                    Id = productDomain.Id,
                    ProductName = productDomain.ProductName,
                    ProductDescription = productDomain.ProductDescription,
                    ProductPrice = productDomain.ProductPrice,
                    Category = productDomain.Category,
                    CategoryId = productDomain.CategoryId,
                    Restaurant = productDomain.Restaurant,
                    RestaurantId = productDomain.RestaurantId,
                    Stock = productDomain.Stock
                });
            }
            return Ok(productsDto);
        }

        // GET SINGLE REGIONS (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var productsDomain = dbContext.ProductItems.FirstOrDefault(r => r.Id == id);
            if (productsDomain == null)
            {
                return NotFound();
            }

            //map/convert region domain model to Region DTO
            var productsDto = new ProductItemDto
            {
                Id = productsDomain.Id,
                ProductName = productsDomain.ProductName,
                ProductDescription = productsDomain.ProductDescription,
                ProductPrice = productsDomain.ProductPrice,
                Category = productsDomain.Category,
                CategoryId = productsDomain.CategoryId,
                Restaurant = productsDomain.Restaurant,
                RestaurantId = productsDomain.RestaurantId,
                Stock = productsDomain.Stock
            };
            return Ok(productsDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddProductItemRequestDto addProductItemRequestDto)
        {
            //map or convert DTO to Domain Model
            var productDomainModel = new ProductItem
            {
                ProductName = addProductItemRequestDto.ProductName,
                ProductDescription = addProductItemRequestDto.ProductDescription,
                ProductPrice = addProductItemRequestDto.ProductPrice,
                Category = addProductItemRequestDto.Category,
                CategoryId = addProductItemRequestDto.CategoryId,
                Restaurant = addProductItemRequestDto.Restaurant,
                RestaurantId = addProductItemRequestDto.RestaurantId,
                Stock = addProductItemRequestDto.Stock
            };

            //use Domain model to create Region
            dbContext.ProductItems.Add(productDomainModel);
            dbContext.SaveChanges();

            //map domain model back to DTO
            var productDto = new ProductItemDto
            {
                Id = productDomainModel.Id,
                ProductName = productDomainModel.ProductName,
                ProductDescription = productDomainModel.ProductDescription,
                ProductPrice = productDomainModel.ProductPrice,
                Category = productDomainModel.Category,
                CategoryId = productDomainModel.CategoryId,
                Restaurant = productDomainModel.Restaurant,
                RestaurantId = productDomainModel.RestaurantId,
                Stock = productDomainModel.Stock
            };
            return CreatedAtAction(nameof(GetById), new { id = productDomainModel.Id }, productDto);

        }

        //Update region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateProductItemRequestDto updateProductRequestDto)
        {
            var productDomainModel = dbContext.ProductItems.FirstOrDefault(x => x.Id == id);
            if (productDomainModel == null)
            {
                return NotFound();
            }

            //map dto to domain model
            productDomainModel.ProductName = updateProductRequestDto.ProductName;
            productDomainModel.ProductDescription = updateProductRequestDto.ProductDescription;
            productDomainModel.ProductPrice = updateProductRequestDto.ProductPrice;
            productDomainModel.Category = updateProductRequestDto.Category;
            productDomainModel.CategoryId = updateProductRequestDto.CategoryId;
            productDomainModel.Restaurant = updateProductRequestDto.Restaurant;
            productDomainModel.RestaurantId = updateProductRequestDto.RestaurantId;
            productDomainModel.Stock = updateProductRequestDto.Stock;
            dbContext.SaveChanges();

            //convert domain model to DTO
            var productDto = new ProductItemDto
            {
                Id = productDomainModel.Id,
                ProductName = productDomainModel.ProductName,
                ProductDescription = productDomainModel.ProductDescription,
                ProductPrice = productDomainModel.ProductPrice,
                Category = productDomainModel.Category,
                CategoryId = productDomainModel.CategoryId,
                Restaurant = productDomainModel.Restaurant,
                RestaurantId = productDomainModel.RestaurantId,
                Stock = productDomainModel.Stock
            };
            return Ok(productDto);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var productDomainModel = dbContext.ProductItems.FirstOrDefault(x => x.Id == id);
            if (productDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            dbContext.ProductItems.Remove(productDomainModel);
            dbContext.SaveChanges();

            //return deleted Region back
            //map domain Model to Dto
            var productDto = new ProductItemDto
            {
                Id = productDomainModel.Id,
                ProductName = productDomainModel.ProductName,
                ProductDescription = productDomainModel.ProductDescription,
                ProductPrice = productDomainModel.ProductPrice,
                Category = productDomainModel.Category,
                CategoryId = productDomainModel.CategoryId,
                Restaurant = productDomainModel.Restaurant,
                RestaurantId = productDomainModel.RestaurantId,
                Stock = productDomainModel.Stock
            };
            return Ok(productDto);
        }
    }
}
