using Microsoft.AspNetCore.Components;
using SunrayTech.Models.Dtos;
using SunrayTech.Web.Services.Contracts;

namespace SunrayTech.Web.Pages
{
    public partial class ShoppingCart : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task RemoveFromCart_Click(int id)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.DeleteItem(id);

                RemoveCartItem(id);
            }
            catch (Exception)
            {
                //Log exception

            }
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if(qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };

                    var returntedUpdatedItemDto = await ShoppingCartService.UpdateQty(updateItemDto);

                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if(item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }    
                }
            }
            catch (Exception)
            {
                //Log exception
            }
        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);
        }

    }
}