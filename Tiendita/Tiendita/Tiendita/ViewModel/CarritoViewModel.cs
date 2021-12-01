using System;
using System.Collections.Generic;
using System.Text;
using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    class CarritoViewModel : BaseViewModel<List<CarritoDetalle>>
    {
        private CarritoService _carritoService;
        private string _mensaje;
        public CarritoViewModel(INavigation navigation, List<CarritoDetalle> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<CarritoDetalle>();
            }
            _carritoService = new CarritoService();

            CarritoAction();
        }

        public List<CarritoDetalle> Carrito
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

        private async void CarritoAction()
        {
            Carrito = await _carritoService?.CarritoAsync();
            Mensaje = Carrito.Count < 1 ? "No has agregado ningún producto." : "Productos agregados:";
        }
    }

}

