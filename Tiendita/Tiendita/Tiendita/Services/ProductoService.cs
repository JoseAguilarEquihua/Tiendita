using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<List<Producto>> ProductosAsync()
        {
            List<Producto> productosResult = null;
            HttpResponseMessage response = null;

            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_PRODUCTOS);

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

