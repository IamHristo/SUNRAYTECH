using SunrayTech.Models.Dtos;

namespace SunrayTech.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();

    }
}
