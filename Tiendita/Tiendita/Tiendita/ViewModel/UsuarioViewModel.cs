using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    class UsuarioViewModel : BaseViewModel<Usuario>
    {
        private Command _registerCommand;
        private UsuariosServices _registerService;
        private Usuario _usuario;
        private string _result;

        public UsuarioViewModel(INavigation navigation, Usuario model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new Usuario();
            }
            _registerService = new UsuariosServices();
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

        public string Nombre
        {
            get => Model.Nombre;

            set
            {
                if (string.Equals(value, Model.Nombre)) return;
                Model.Nombre = value;
                OnPropertyChanged();
            }
        }

        public string Apellidos
        {
            get => Model.Apellidos;

            set
            {
                if (string.Equals(value, Model.Apellidos)) return;
                Model.Apellidos = value;
                OnPropertyChanged();
            }
        }

        public string Direccion
        {
            get => Model.Direccion;

            set
            {
                if (string.Equals(value, Model.Direccion)) return;
                Model.Direccion = value;
                OnPropertyChanged();
            }
        }

        public string Telefono
        {
            get => Model.Telefono;

            set
            {
                if (string.Equals(value, Model.Telefono)) return;
                Model.Telefono = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                if (string.Equals(_result, value)) return;
                _result = value;

                OnPropertyChanged();
            }
        }

        public Command RegisterCommand
        {
            get => _registerCommand ?? (_registerCommand = new Command(RegisterAction));
        }

        private async void RegisterAction()
        {
            Model.TipoUsuario = true;
            _usuario = await _registerService?.RegisterAsync(Model);

            if (_usuario != null)
            {
                Navigation.PushAsync(new View.Register());
            }
            else
            {
                Result = "No se pudo registrar al usuario.";
            }
        }
    }


}