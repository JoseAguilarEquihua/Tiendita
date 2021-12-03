using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    public class LoginViewModel : BaseViewModel<Auth>
    {
        private Command _loginCommand;
        private Command _registerCommand;
        private LoginService _loginService;
        private CarritoService _carritoService;
        private Sesion _sesion;
        private string _jsonResult;
        private Carrito _cartResponse = new Carrito();
        private Carrito _cart = new Carrito();

        public LoginViewModel(INavigation navigation,  Auth model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new Auth();
            }
            _loginService = new LoginService();
            _carritoService = new CarritoService();
        }

        public string Correo
        {
            get => Model.Correo;

            set
            {
                if (string.Equals(value, Model.Correo)) return;
                Model.Correo = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => Model.Contrasenia;

            set
            {
                if (string.Equals(value, Model.Contrasenia)) return;
                Model.Contrasenia = value;
                OnPropertyChanged();
            }
        }

        public string JsonResult
        {
            get => _jsonResult;
            set
            {
                if (string.Equals(_jsonResult, value)) return;
                _jsonResult = value;

                OnPropertyChanged();
            }
        }


        public Command LoginCommand
        {
            get => _loginCommand ?? (_loginCommand = new Command(LoginAction));
        }

        public Command RegisterCommand
        {
            get => _registerCommand ?? (_registerCommand = new Command(RegisterAction));
        }

        private void RegisterAction()
        {
            Navigation.PushAsync(new View.Register());
        }

        private async void LoginAction()
        {
            Model.Contrasenia = Helpers.Base64Encrypter.Encriptar(Model.Contrasenia);
            _sesion = await _loginService?.Login(Model);

            if (_sesion != null)
            {                
                _cart.Correo = Correo;
                _cart.IdCarrito = 0;
                _cartResponse = await _carritoService?.AddCarritoAsync(_cart, _sesion.Token);
                if (_cartResponse != null)
                {
                    if (_sesion.usuario.TipoUsuario)
                    {
                        await Navigation.PushAsync(new View.Productos(_cartResponse.Correo, _cartResponse.IdCarrito, _sesion.Token));
                    } else
                    {
                        await Navigation.PushAsync(new View.Dashboard(_cartResponse.Correo, _sesion.Token));
                    }
                     
                } else
                {
                    JsonResult = _cartResponse.IdCarrito.ToString();
                }
              
            }
            else
            {
                JsonResult = "Correo o contraseña incorrectos";
            }
        }
    }
}
