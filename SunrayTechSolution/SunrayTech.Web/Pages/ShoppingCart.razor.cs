using Microsoft.AspNetCore.Components;
using SunrayTech.Models.Dtos;
using SunrayTech.Web.Services.Contracts;

namespace SunrayTech.Web.Pages
{
    public partial class ShoppingCart:ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService{ get; set; }

        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

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
    }
}