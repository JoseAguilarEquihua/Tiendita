using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tiendita.Helpers;
using Tiendita.Model;

namespace Tiendita.Services
{
    public class ProductoService
    {
        HttpClient client;

        private readonly string API_PRODUCTOS = "Producto";

        public ProductoService()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<List<Producto>> ProductosAsync(string token)
        {
            List<Producto> productosResult = null;
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync(AppResources.APIResources.APIHOST + API_PRODUCTOS);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                productosResult = JsonConvert.DeserializeObject<List<Producto>>(result);
            }

            return productosResult;
        }

    }
}

