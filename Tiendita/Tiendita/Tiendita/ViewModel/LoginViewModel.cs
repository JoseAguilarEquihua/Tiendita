using System;
using System.Collections.Generic;
using System.Text;
using Tiendita.Model;
using Tiendita.Services;
using Tiendita.Views;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    public class LoginViewModel : BaseViewModel<Auth>
    {
        private Command _loginCommand;
        private LoginService _loginService;
        private Usuario _usuario;
        private string _jsonResult;

        public LoginViewModel(INavigation navigation, Auth model = null ) : base(navigation, model)
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

        private async void LoginAction()
        {
            Usuario usuario = await _loginService?.Login(Model);
            if (usuario != null)
            {
                Navigation.PushAsync(new View.Productos());
            } else
            {
                JsonResult = "Correo o contraseña incorrectos";
            }
        }
    }
}
