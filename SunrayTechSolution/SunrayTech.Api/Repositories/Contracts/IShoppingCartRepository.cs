using SunrayTech.Api.Entities;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);

        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);

        Task<CartItem> DeleteItem(int id);

        Task<CartItem> GetItem(int id);

        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
