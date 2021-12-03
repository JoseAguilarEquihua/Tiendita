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
        private PedidoService _pedidoService;
        private string _mensaje;
        private string _correo;
        private int _idCarrito;
        private bool _result;
        private double _total;
        private string _token;
        private Pedido _pedido;
        private bool _pedidoDetalle;
        private List<CarritoDetalle> _carritoDetalle;
        private bool _eliminado = false;
        private Command _sumaCommand;
        private Command _restaCommand;
        private Command _eliminaCommand;
        private Command _completaPedidoCommand;
        private Command _regresaCommand;

        public CarritoViewModel(INavigation navigation, string Correo=null, int IdCarrito = 0, string Token = null, List<CarritoDetalleProducto> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<CarritoDetalleProducto>();
            }
            _carritoService = new CarritoService();
            _pedidoService = new PedidoService();
            _correo = Correo;
            _idCarrito = IdCarrito;
            _token = Token;

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

        public double Total
        {
            get => _total;
            set
            {
                if (_total == value) return;
                _total = value;

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

        public Command CompletaPedidoCommand
        {
            get => _completaPedidoCommand ?? (_completaPedidoCommand = new Command(CompletaPedidoAction));
        }

        public Command RegresarCommand
        {
            get => _regresaCommand ?? (_regresaCommand = new Command(RegresaAction));
        }

        private async void CarritoAction()
        {
            Carrito = await _carritoService?.CarritoAsync(_idCarrito, _token);
            if(Carrito != null)
            {
                Total = _carritoService.CalculaTotal(Carrito);
            }
            else
            {
                Total = 0;
            }            
            Mensaje = Carrito.Count < 1 ? "No has agregado ningún producto." : "Productos agregados:";
        }

        private void RegresaAction()
        {            
             Navigation.PushAsync(new View.Productos(_correo, _idCarrito, _token));            
        }

        private async void SumaAction(int id)
        {
            _result = await _carritoService?.ModificaCantidad(id, true, _token);
            if (_result)
            {
                await Navigation.PushAsync(new View.Cart(_correo, _idCarrito, _token));
            }            
        }

        private async void RestaAction(int id)
        {
            _result = await _carritoService?.ModificaCantidad(id, false, _token);
            if (_result)
            {
                await Navigation.PushAsync(new View.Cart(_correo, _idCarrito, _token));
            }
        }

        private async void EliminaAction(int id)
        {
            _result = await _carritoService?.EliminaProducto(id, _token);
            if (_result)
            {
                await Navigation.PushAsync(new View.Cart(_correo, _idCarrito, _token));
            }
        }
        
        private async void CompletaPedidoAction()
        {
            _pedido = await _pedidoService?.AddPedidoAsync(_correo, _total, _token);
            if (_pedido != null)
            {
                _carritoDetalle = await _carritoService.CarritoDetalleAsync(_idCarrito, _token);
                if( _carritoDetalle != null)
                {
                    _pedidoDetalle = await _pedidoService.AddDetallePedidoAsync(_carritoDetalle, _pedido.IdPedido, _token);
                    if(_pedidoDetalle)
                    {                        
                        _eliminado = await _pedidoService.EliminaCarrito(_idCarrito, _token);
                        if(_eliminado)
                        {
                            await App.Current.MainPage.DisplayAlert("Alerta", "Pedido completado", "OK");
                            await Navigation.PushAsync(new View.Productos(_correo, _idCarrito, _token));
                        }                       
                    }
                }
      
            }
            

        }
        


    }

}

