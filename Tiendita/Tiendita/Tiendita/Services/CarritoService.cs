using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tiendita.Helpers;
using Tiendita.Model;

namespace Tiendita.Services
{
    public class CarritoService
    {
        HttpClient client;

        private readonly string API_DETALLECARRITO = "DetalleCarrito";
        private readonly string API_CARRITO = "Carrito";

        public CarritoService()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<List<CarritoDetalle>> CarritoAsync()
        {
            List<CarritoDetalle> carritoResult = null;
            HttpResponseMessage response = null;

            response = await client.GetAsync("https://192.168.100.3:45455/api/" + API_DETALLECARRITO);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                carritoResult = JsonConvert.DeserializeObject<List<CarritoDetalle>>(result);
            }

            return carritoResult;
        }

        public async Task<List<CarritoDetalle>> AddCarritoAsync()
        {
            List<CarritoDetalle> carritoResult = null;
            HttpResponseMessage response = null;

            response = await client.GetAsync("https://192.168.100.3:45455/api/" + API_CARRITO);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                carritoResult = JsonConvert.DeserializeObject<List<CarritoDetalle>>(result);
            }

            return carritoResult;
        }

    }
}


