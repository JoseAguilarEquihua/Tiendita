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
        private Usuario _usuario;
        private string _jsonResult;

        public LoginViewModel(INavigation navigation, Auth model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new Auth();
            }
            _loginService = new LoginService();
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
            _usuario = await _loginService?.Login(Model);

            if (_usuario != null)
            {
                Navigation.PushAsync(new View.Productos());
            }
            else
            {
                JsonResult = "Correo o contraseña incorrectos";
            }
        }
    }
}
