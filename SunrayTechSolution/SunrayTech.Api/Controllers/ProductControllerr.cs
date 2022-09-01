﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunrayTech.Api.Extensions;
using SunrayTech.Api.Repositories.Contracts;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductControllerr : ControllerBase
    {
        public readonly IProductRepository productRepository;

        public ProductControllerr(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productCategories = await this.productRepository.GetCategories();
                

                if (products == null || productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto(productCategories);

                    return Ok(productDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing data from the database");
            }
        }

    }
}
