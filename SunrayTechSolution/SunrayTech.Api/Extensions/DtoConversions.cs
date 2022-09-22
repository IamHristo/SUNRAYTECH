using SunrayTech.Api.Entities;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products,
                                                            IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Descriptin = product.Description,
                        ImagaURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = productCategory.Id,
                        CategoryName = productCategory.Name
                    }).ToList();
        }

        public static ProductDto ConvertToDto(this Product product,
                                                            ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Descriptin = product.Description,
                ImagaURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = productCategory.Id,
                CategoryName = productCategory.Name
            };
        }
    }
}
