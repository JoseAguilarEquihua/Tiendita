using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public PedidoService()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<Pedido> AddPedidoAsync(string correo, double total)
        {
            Pedido pedidoResult = new Pedido();
            HttpResponseMessage response = null;
            string result = string.Empty;

            pedidoResult.Correo = correo;
            pedidoResult.Total = total;

            result = System.Text.Json.JsonSerializer.Serialize(pedidoResult);

            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

            response = await client.PostAsync("https://192.168.100.7:45455/api/" + API_PEDIDO, content);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = await contenido.ReadAsStringAsync();
                pedidoResult = JsonConvert.DeserializeObject<Pedido>(result);
            }

            return pedidoResult;
        }

        public async Task<bool> AddDetallePedidoAsync(List<CarritoDetalle> carritoDetalle, int id)
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

        public async Task<bool> EliminaCarrito(int id)
        {
            bool result = false;
            HttpResponseMessage response = null;

            response = await client.GetAsync("https://192.168.100.7:45455/api/" + API_DETALLECARRITO + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content;
                result = true;
            }

            return result;
        }


    }
}
