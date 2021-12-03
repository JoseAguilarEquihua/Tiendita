using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tiendita.Helpers;
using Tiendita.Model;

namespace Tiendita.Services
{
    class PedidoService
    {
        HttpClient client;

        private readonly string API_PEDIDO = "Pedido";
        private readonly string API_DETALLEPEDIDO = "DetallePedido";
        private readonly string API_DETALLECARRITO = "DetalleCarrito/DeleteDetalleCarrito";
        private readonly string API_PEDIDOPRODUCTO = "PedidoController/GetPedidosProducto";
        private readonly string API_DETALLEPEDIDOUSUARIO = "PedidoController/GetDetallePedido";

        public PedidoService()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<List<PedidoProducto>> PedidosAsync(string token)
        {
            List<PedidoProducto> pedidosResult = null;
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_PEDIDOPRODUCTO);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                pedidosResult = JsonConvert.DeserializeObject<List<PedidoProducto>>(result);
            }
            return pedidosResult;
        }

        public async Task<PedidoProducto> PedidosProductoAsync(int id, string token)
        {
            PedidoProducto pedidosResult = null;
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_PEDIDOPRODUCTO + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                pedidosResult = JsonConvert.DeserializeObject<PedidoProducto>(result);
            }
            return pedidosResult;
        }

        public async Task<Pedido> PedidoAsync(int id, string token)
        {
            Pedido pedidoResult = null;
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_PEDIDO + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                pedidoResult = JsonConvert.DeserializeObject<Pedido>(result);
            }
            return pedidoResult;
        }

        public async Task<Pedido> AddPedidoAsync(string correo, double total, string token)
        {
            Pedido pedidoResult = new Pedido();
            HttpResponseMessage response = null;
            string result = string.Empty;

            pedidoResult.Correo = correo;
            pedidoResult.Total = total;

            result = System.Text.Json.JsonSerializer.Serialize(pedidoResult);

            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.PostAsync("https://192.168.100.7:45455/api/" + API_PEDIDO, content);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = await contenido.ReadAsStringAsync();
                pedidoResult = JsonConvert.DeserializeObject<Pedido>(result);
            }

            return pedidoResult;
        }

        public async Task<bool> AddDetallePedidoAsync(List<CarritoDetalle> carritoDetalle, int id, string token)
        {
            List<PedidoDetalle> listaPedidoResult = new List<PedidoDetalle>();
            PedidoDetalle pedidoDetalle = new PedidoDetalle();
            HttpResponseMessage response = null;
            string result;
            StringContent content;
            bool resultado = false;

            foreach (CarritoDetalle carrito in carritoDetalle)
            {
                resultado = false;
                pedidoDetalle.IdPedido = id;
                pedidoDetalle.IdProducto = carrito.IdProducto;
                pedidoDetalle.Cantidad = carrito.Cantidad;
                pedidoDetalle.Detalle = carrito.Detalle;                

                result = string.Empty;

                result = System.Text.Json.JsonSerializer.Serialize(pedidoDetalle);

                content = new StringContent(result, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await client.PostAsync("https://192.168.100.7:45455/api/" + API_DETALLEPEDIDO, content);

                if (response.IsSuccessStatusCode)
                {
                    var contenido = response.Content;
                    result = await contenido.ReadAsStringAsync();
                    pedidoDetalle = JsonConvert.DeserializeObject<PedidoDetalle>(result);
                    if(pedidoDetalle != null)
                    {
                        resultado = true;
                    }                    
                }

            }         
            return resultado;
        }

        public async Task<bool> EliminaCarrito(int id, string token)
        {
            bool result = false;
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_DETALLECARRITO + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = true;
            }

            return result;
        }

        public async Task<List<DetallePedidoUsuario>> DetallePedidoAsync(int idPedido, string token)
        {
            List<DetallePedidoUsuario> detalleResult = null;
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_DETALLEPEDIDOUSUARIO + "/" + idPedido);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                var result = await contenido.ReadAsStringAsync();
                detalleResult = JsonConvert.DeserializeObject<List<DetallePedidoUsuario>>(result);
            }

            return detalleResult;
        }


    }
}
