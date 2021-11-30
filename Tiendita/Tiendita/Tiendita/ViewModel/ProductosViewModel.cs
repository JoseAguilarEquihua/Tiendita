using System.Collections.Generic;
using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    class ProductosViewModel : BaseViewModel<List<Producto>>
    {
        private ProductoService _productoService;
        private string _mensaje;

        public ProductosViewModel(INavigation navigation, List<Producto> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<Producto>();
            }
            _productoService = new ProductoService();

            ProductosAction();
        }

        public List<Producto> Productos
        {
            get => Model;
            set
            {
                if(Model == value) return;
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

        private async void ProductosAction()
        {
            Productos = await _productoService?.ProductosAsync();
            Mensaje = Productos.Count < 1 ? "No hay productos disponibles." : "Productos disponibles:";
        }
    }
}
