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
            var categories = await sunrayTechDbContext.ProductCategories.ToListAsync();

            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await sunrayTechDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await sunrayTechDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await sunrayTechDbContext.Products.ToListAsync();

            return products;
        }
    }
}
