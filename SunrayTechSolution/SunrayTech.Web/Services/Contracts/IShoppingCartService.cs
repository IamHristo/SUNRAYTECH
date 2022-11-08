using SunrayTech.Models.Dtos;

namespace SunrayTech.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);

        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);

        Task<CartItemDto> DeleteItem(int id);

        Task<CartItemDto> UpdateQty (CartItemQtyUpdateDto cartItemQtyUpdateDto);
    }
}
