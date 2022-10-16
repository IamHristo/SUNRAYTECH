using SunrayTech.Models.Dtos;

namespace SunrayTech.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetItems(int userId);

        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
    }
}
