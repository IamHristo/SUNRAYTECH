using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunrayTech.Api.Extensions;
using SunrayTech.Api.Repositories.Contracts;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();

                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto();

                    return Ok(productDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await productRepository.GetItem(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var productDto = product.ConvertToDto();

                    return Ok(productDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing data from the database");
            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await this.productRepository.GetCategories();

                var productCategoriesDtos = productCategories.ConvertToDto();
                return Ok(productCategoriesDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing data from the database");
            }
        }

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var products = await productRepository.GetItemsByCategory(categoryId);
                var productDtos = products.ConvertToDto();

                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing data from the database");
            }    
        }

    }
}
