using System;
using System.Collections.Generic;
using System.Text;
using Tiendita.Model;
using Tiendita.Views;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    public class LoginViewModel : BaseViewModel<Usuario>
    {
        private Command _loginCommand;
        public LoginViewModel(INavigation navigation, Usuario model = null ) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new Usuario();

            }

            //usuariosService = new UsuariosService();
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
            get => Model.Password;

            set
            {
                if (string.Equals(value, Model.Password)) return;
                Model.Password = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand
        {
            get => _loginCommand ?? (_loginCommand = new Command(LoginAction));
        }

        private void LoginAction()
        {
            Navigation.PushAsync(new Register());
        }
    }
}
