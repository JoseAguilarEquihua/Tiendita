using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tiendita.Helpers;
using Tiendita.Model;

namespace Tiendita.Services
{
    public class UsuariosServices
    {
        HttpClient client;

        private readonly string API_USUARIOS = "Usuario";

        public UsuariosServices()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<Usuario> RegisterAsync(Usuario usuario)
        {
            string result = string.Empty;
            Usuario usuarioResponse = null;

            if (usuario != null && !string.IsNullOrEmpty(usuario.Correo) && !string.IsNullOrEmpty(usuario.Contrasenia) && !string.IsNullOrEmpty(usuario.Nombre) && !string.IsNullOrEmpty(usuario.Apellidos) && !string.IsNullOrEmpty(usuario.Direccion) && !string.IsNullOrEmpty(usuario.Telefono))
            {

                result = System.Text.Json.JsonSerializer.Serialize(usuario);

                StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;


                response = await client.PostAsync("https://192.168.100.7:45455/api/" + API_USUARIOS, content);

                if (response.IsSuccessStatusCode)
                {
                    var contenido = response.Content;
                    result = await contenido.ReadAsStringAsync();
                    usuarioResponse = JsonConvert.DeserializeObject<Usuario>(result);
                }
            }

            return usuarioResponse;
        }

    }
}
