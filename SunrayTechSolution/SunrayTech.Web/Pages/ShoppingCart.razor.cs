using Microsoft.AspNetCore.Components;
using SunrayTech.Models.Dtos;
using SunrayTech.Web.Services.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

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

        public string TotalPrice { get; set; }

        public int TotalQty { get; set; }

        public bool CartSummaryLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);

                CartChange();
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
                //var cartItemDto = await ShoppingCartService.DeleteItem(id);

                RemoveCartItem(id);
                CartChange();
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

                    UpdateItemTotalPrice(returntedUpdatedItemDto);

                    CartChange();
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

        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQty();
        }

        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C");
        }

        private void SetTotalQty()
        {
            TotalQty = ShoppingCartItems.Select(x => x.Qty).Sum();
        }

        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if(item != null)
            {
                item.TotalPrice = item.Price * item.Qty;
            }
        }

        private void CartChange()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppinCartChanged(TotalQty);
        }
    }
}