using Microsoft.EntityFrameworkCore;
using SunrayTech.Api.Data;
using SunrayTech.Api.Entities;
using SunrayTech.Api.Repositories.Contracts;

namespace SunrayTech.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly SunrayTechDbContext sunrayTechDbContext;

        public ProductRepository(SunrayTechDbContext sunrayTechDbContext)
        {
            this.sunrayTechDbContext = sunrayTechDbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.sunrayTechDbContext.ProductCategories.ToListAsync();

            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.sunrayTechDbContext.Products.ToListAsync();

            return products;
        }
    }
}
