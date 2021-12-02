using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        private readonly string API_CARRITODETALLEPRODUCTO = "DetalleCarrito/GetCarrito";
        private readonly string API_MODIFICACANTIDAD = "DetalleCarrito/ModifyDetalleCarrito";
        

        public CarritoService()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<List<CarritoDetalleProducto>> CarritoAsync(int idCarrito)
        {
            List<CarritoDetalleProducto> carritoResult = null;
            HttpResponseMessage response = null;

            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_CARRITODETALLEPRODUCTO + "/" + idCarrito );

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                carritoResult = JsonConvert.DeserializeObject<List<CarritoDetalleProducto>>(result);
            }

            return carritoResult;
        }

        public async Task<Carrito> AddCarritoAsync(Carrito carrito)
        {
            Carrito carritoResult = null;
            HttpResponseMessage response = null;
            string result = string.Empty;            

            result = System.Text.Json.JsonSerializer.Serialize(carrito);

            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

            response = await client.PostAsync("https://192.168.100.7:45455/api/" + API_CARRITO, content);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = await contenido.ReadAsStringAsync();
                carritoResult = JsonConvert.DeserializeObject<Carrito>(result);
            }

            return carritoResult;
        }

        public async Task<bool> AddProductAsync(CarritoDetalle carritoDetalle)
        {
            CarritoDetalle carritoResult = null;
            HttpResponseMessage response = null;
            string result = string.Empty;
            bool agregado = false;

            result = System.Text.Json.JsonSerializer.Serialize(carritoDetalle);

            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

            response = await client.PostAsync("https://192.168.100.7:45455/api/" + API_DETALLECARRITO, content);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = await contenido.ReadAsStringAsync();
                
                carritoResult = JsonConvert.DeserializeObject<CarritoDetalle>(result);
                if(carritoResult != null)
                {
                    agregado = true;
                }
            }

            return agregado;
        }

        public async Task<bool> ModificaCantidad(int id, bool accion)
        {
            bool result = false;
            HttpResponseMessage response = null;

            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_MODIFICACANTIDAD + "/" + id + "/" + accion);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = true;
                
            }

            return result;
        }

        public async Task<bool> EliminaProducto(int id)
        {
            bool result = false;
            HttpResponseMessage response = null;

            response = await client.DeleteAsync("https://192.168.100.7:45455/api/" + API_DETALLECARRITO + "/" + id );

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = true;

            }

            return result;
        }
    }
}


