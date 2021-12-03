using System;
using System.Collections.Generic;
using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    class ProductosViewModel : BaseViewModel<List<Producto>>
    {
        private ProductoService _productoService;
        private CarritoService _carritoService;
        private string _mensaje;
        private string _correo;
        private int _idCarrito;
        private CarritoDetalle carritoDetalle = new CarritoDetalle();        
        private int _idProducto;
        private bool result;
        private string _token;

        private Command _carritoCommand;
        private Command _agregarCommand;
        private Command _logoutCommand;

        public ProductosViewModel(INavigation navigation, string Correo = null, int IdCarrito = 0,  string Token = null, List<Producto> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<Producto>();
            }
            _productoService = new ProductoService();
            _carritoService = new CarritoService();

            _correo = Correo;
            _idCarrito = IdCarrito;
            _token = Token;
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

        public string Correo
        {
            get => _correo;
            set
            {
                if (_correo == value) return;
                _correo = value;

                OnPropertyChanged();
            }
        }

        public int IdCarrito
        {
            get => _idCarrito;
            set
            {
                if (_idCarrito == value) return;
                _idCarrito = value;

                OnPropertyChanged();
            }
        }

        public int IdProducto
        {
            get => _idProducto;
            set
            {
                if (_idProducto == value) return;
                _idProducto = value;

                OnPropertyChanged();
            }
        }

        public Command CarritoCommand
        {
            get => _carritoCommand ?? (_carritoCommand = new Command(CarritoAction));
        }

        public Command AddProductCommand
        {
            get => _agregarCommand ?? (_agregarCommand = new Command<int>(AgregarAction));
        }

        public Command LogoutCommand
        {
            get => _logoutCommand ?? (_logoutCommand = new Command(LogoutAction));
        }

        private void CarritoAction()
        {
            Navigation.PushAsync(new View.Cart(_correo, _idCarrito, _token));
        }

        private void LogoutAction()
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void ProductosAction()
        {
            Productos = await _productoService?.ProductosAsync(_token);
            Mensaje = Productos.Count < 1 ? "No hay productos disponibles." : "Productos disponibles:";
        }

        private async void AgregarAction(int id)
        {
            carritoDetalle.IdCarrito = _idCarrito;
            carritoDetalle.IdProducto = id;
            carritoDetalle.Cantidad = 1;
            result = await _carritoService?.AddProductAsync(carritoDetalle, _token);
            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Producto agregado", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "No se pudo insertar el producto", "OK");
            }
        }
            
           

        }

       
            

    
}
