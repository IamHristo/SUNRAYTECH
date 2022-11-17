using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SunrayTech.Models.Dtos;
using SunrayTech.Web.Services.Contracts;

namespace SunrayTech.Web.Pages
{
    public partial class Checkout : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        protected int TotalQty { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);

                if(ShoppingCartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = ShoppingCartItems.Sum(x => x.TotalPrice);
                    TotalQty = ShoppingCartItems.Sum(x => x.Qty);
                    PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";

                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            try
            {
                if(firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

    }
}