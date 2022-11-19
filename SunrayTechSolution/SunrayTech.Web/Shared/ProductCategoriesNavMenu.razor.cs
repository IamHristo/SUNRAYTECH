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

namespace SunrayTech.Web.Shared
{
    public partial class ProductCategoriesNavMenu 
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ProductCategories = await ProductService.GetProductCategories();
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}