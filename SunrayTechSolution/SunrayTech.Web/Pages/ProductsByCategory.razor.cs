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
using SunrayTech.Models.Dtos;
using SunrayTech.Web.Services.Contracts;
using SunrayTech.Web.Pages;

namespace SunrayTech.Web.Pages
{
    public partial class ProductsByCategory
    {
        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductDto> ProductsDtos { get; set; }

        public string CategoryName { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                ProductsDtos = await ProductService.GetItemsByCategory(CategoryId);
                if(ProductsDtos != null && ProductsDtos.Count() > 0)
                {
                    var productDto = ProductsDtos.FirstOrDefault(p => p.CategoryId == CategoryId);
                    if(productDto != null)
                    {
                        CategoryName = productDto.CategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}