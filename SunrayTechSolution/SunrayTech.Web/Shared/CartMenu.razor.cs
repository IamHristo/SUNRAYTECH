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
using System.Runtime.CompilerServices;

namespace SunrayTech.Web.Shared
{
    public partial class CartMenu: IDisposable
    {
        private int shoppinCartItemCount = 0;

        protected override void OnInitialized()
        {
            shoppingCartService.OnShoppinCartChanged += ShoppinCartChanged;
        }

        protected void ShoppinCartChanged(int totalQty)
        {
            shoppinCartItemCount = totalQty;
            StateHasChanged();
        }

        void IDisposable.Dispose()
        {
            shoppingCartService.OnShoppinCartChanged -= ShoppinCartChanged;
        }
    }
}