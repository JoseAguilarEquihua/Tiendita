using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tiendita.Helpers;
using Tiendita.Model;
using Newtonsoft.Json;

namespace Tiendita.Services
{
    public class LoginService
    {
        HttpClient client;

        private readonly string API_USUARIOS = "Usuarios/Login";

        public LoginService()
        {
#if DEBUG
            var handler = new BypassSSLValidationClientHandler();
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<Usuario> Login(Auth usuario)
        {
            string result = string.Empty;
            Usuario usuarioResponse = null;

            if (usuario != null && !string.IsNullOrEmpty(usuario.Correo) && !string.IsNullOrEmpty(usuario.Contrasenia))
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
