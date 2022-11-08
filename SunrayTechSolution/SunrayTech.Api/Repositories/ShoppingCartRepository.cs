using Microsoft.EntityFrameworkCore;
using SunrayTech.Api.Data;
using SunrayTech.Api.Entities;
using SunrayTech.Api.Repositories.Contracts;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly SunrayTechDbContext sunrayTechDbContext;

        public ShoppingCartRepository(SunrayTechDbContext sunrayTechDbContext)
        {
            this.sunrayTechDbContext = sunrayTechDbContext;
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if(await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in sunrayTechDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.sunrayTechDbContext.CartItems.AddAsync(item);
                    await this.sunrayTechDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await sunrayTechDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                sunrayTechDbContext.CartItems.Remove(item);
                await sunrayTechDbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in sunrayTechDbContext.Carts
                          join CartItem in sunrayTechDbContext.CartItems
                          on cart.Id equals CartItem.Id
                          where CartItem.Id == id
                          select new CartItem
                          {
                              Id = CartItem.Id,
                              ProductId = CartItem.ProductId,
                              Qty = CartItem.Qty,
                              CartId = CartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in sunrayTechDbContext.Carts
                          join cartItem in sunrayTechDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              CartId = cartItem.CartId,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await sunrayTechDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await sunrayTechDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await sunrayTechDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }
    }
}
