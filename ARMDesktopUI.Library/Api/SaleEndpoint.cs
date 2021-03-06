using ARMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ARMDesktopUI.Library.Api
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private IAPIHelper _apiHelper;

        public SaleEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostSale(SaleModel saleModel)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", saleModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                    //return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
