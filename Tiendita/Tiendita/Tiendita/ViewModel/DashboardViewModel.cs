using System;
using System.Collections.Generic;
using System.Text;
using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    public class DashboardViewModel : BaseViewModel<List<PedidoProducto>>
    {
        private string _correo;
        private string _token;
        private string _mensaje;
        private PedidoService _pedidoService;

        private Command _detalleCommand;
        private Command _logoutCommand;

        public DashboardViewModel(INavigation navigation, string Correo = null, string Token = null, List<PedidoProducto> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<PedidoProducto>();
            }            
            _pedidoService = new PedidoService();
            _correo = Correo;
            _token = Token;

            PedidosAction();
        }
        
        public List<PedidoProducto> Pedidos
        {
            get => Model;
            set
            {
                if (Model == value) return;
                Model = value;

                OnPropertyChanged();
            }
        }

        public string Mensaje
        {
            get => _mensaje;
            set
            {
                if (string.Equals(value, _mensaje)) return;
                _mensaje = value;

                OnPropertyChanged();
            }
        }

        public Command DetalleCommand
        {
            get => _detalleCommand ?? (_detalleCommand = new Command<int>(DetalleAction));
        }

        public Command LogoutCommand
        {
            get => _logoutCommand ?? (_logoutCommand = new Command(LogoutAction));
        }

        private async void PedidosAction()
        {
            Pedidos = await _pedidoService?.PedidosAsync( _token);
            Mensaje = Pedidos.Count < 1 ? "No hay pedidos." : "Pedidos realizados:";
        }

        private  void LogoutAction()
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void DetalleAction(int id)
        {
            await Navigation.PushAsync(new View.DetalleDashboard(_correo, _token, id));
        }
        


    }
}
