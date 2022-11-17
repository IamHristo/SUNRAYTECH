using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using SunrayTech.Web;
using SunrayTech.Web.Shared;
using SunrayTech.Web.Services.Contracts;
using SunrayTech.Models.Dtos;

namespace SunrayTech.Web.Pages
{
    public partial class Products : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }


        public IEnumerable<ProductDto> ProductsDto { get; set; }

        public string ErrorMessage { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ProductsDto = await ProductService.GetItems();

                var shoppinCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                var totalQty = shoppinCartItems.Sum(x => x.Qty);

                ShoppingCartService.RaiseEventOnShoppinCartChanged(totalQty);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in ProductsDto
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup; ;
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }
    }
}