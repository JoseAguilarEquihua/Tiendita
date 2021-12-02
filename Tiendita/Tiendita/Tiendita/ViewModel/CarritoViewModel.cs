using System;
using System.Collections.Generic;
using System.Text;
using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    class CarritoViewModel : BaseViewModel<List<CarritoDetalleProducto>>
    {
        private CarritoService _carritoService;
        private string _mensaje;
        private string _correo;
        private int _idCarrito;
        private bool _result;

        private Command _sumaCommand;
        private Command _restaCommand;
        private Command _eliminaCommand;

        public CarritoViewModel(INavigation navigation, string Correo=null, int IdCarrito = 0, List<CarritoDetalleProducto> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<CarritoDetalleProducto>();
            }
            _carritoService = new CarritoService();
            _correo = Correo;
            _idCarrito = IdCarrito;

            CarritoAction();
        }

        public List<CarritoDetalleProducto> Carrito
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
        
        public Command SumaCommand
        {
            get => _sumaCommand ?? (_sumaCommand = new Command<int>(SumaAction));
        }

        public Command RestaCommand
        {
            get => _restaCommand ?? (_restaCommand = new Command<int>(RestaAction));
        }

        public Command EliminaCommand
        {
            get => _eliminaCommand ?? (_eliminaCommand = new Command<int>(EliminaAction));
        }


        private async void CarritoAction()
        {
            Carrito = await _carritoService?.CarritoAsync(_idCarrito);
            Mensaje = Carrito.Count < 1 ? "No has agregado ningún producto." : "Productos agregados:";
        }
        
        private async void SumaAction(int id)
        {
            _result = await _carritoService?.ModificaCantidad(id, true);
            if (_result)
            {
                await Navigation.PushAsync(new View.Cart(_correo, _idCarrito));
            }            
        }

        private async void RestaAction(int id)
        {
            _result = await _carritoService?.ModificaCantidad(id, false);
            if (_result)
            {
                await Navigation.PushAsync(new View.Cart(_correo, _idCarrito));
            }
        }

        private async void EliminaAction(int id)
        {
            _result = await _carritoService?.EliminaProducto(id);
            if (_result)
            {
                await Navigation.PushAsync(new View.Cart(_correo, _idCarrito));
            }
        }

    }

}

