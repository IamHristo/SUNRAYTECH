using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunrayTech.Api.Entities;
using SunrayTech.Api.Extensions;
using SunrayTech.Api.Repositories.Contracts;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await shoppingCartRepository.GetItems(userId);

                if(cartItems == null)
                {
                    return NoContent();
                }
                
                var products = await productRepository.GetItems();

                if(products == null)
                {
                    throw new Exception("No products exist in the system");
                }

                var cartItemsDto = cartItems.ConvertToDto(products);

                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItem(id);

                if (cartItem == null)
                {
                    return NoContent();
                }

                var product = await productRepository.GetItem(id);

                if (product == null)
                {
                    throw new Exception("No products exist in the system");
                }
                var cartItemDto = cartItem.ConvertToDto(product);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await shoppingCartRepository.AddItem(cartItemToAddDto);

                if(newCartItem == null)
                {
                    return NoContent();
                }

                var product = await productRepository.GetItem(newCartItem.ProductId);

                if(product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:{cartItemToAddDto.ProductId}");
                }

                var newCartitemDto = newCartItem.ConvertToDto(product);

                return CreatedAtAction(nameof(GetItem), new { id = newCartitemDto.Id }, newCartitemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
