using Newtonsoft.Json;
using SunrayTech.Models.Dtos;
using SunrayTech.Web.Services.Contracts;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace SunrayTech.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public event Action<int> OnShoppinCartChanged;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var respone = await httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItemToAddDto);

                if(respone.IsSuccessStatusCode)
                {
                    if(respone.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);
                    }

                    return await respone.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    var message = await respone.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{respone.StatusCode} Message -{message}");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CartItemDto>> GetItems(int userId)
        {
            try
            {
                
                var response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return default;
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }

        public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var respone = await httpClient.PatchAsync($"api/ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

                if (respone.IsSuccessStatusCode)
                {
                    return await respone.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return null;

            }
            catch (Exception ex)
            {
                //Log exception
                throw;
            }
        }

        public void RaiseEventOnShoppinCartChanged(int totalQty)
        {
            if(OnShoppinCartChanged != null)
            {
                OnShoppinCartChanged.Invoke(totalQty);
            }
        }
    }
}
