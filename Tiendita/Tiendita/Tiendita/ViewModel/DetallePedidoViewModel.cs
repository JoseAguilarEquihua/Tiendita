﻿using System;
using System.Collections.Generic;
using System.Text;
using Tiendita.Model;
using Tiendita.Services;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    class DetallePedidoViewModel : BaseViewModel<List<DetallePedidoUsuario>>
    {
        private string _correo;
        private string _token;
        private int _idPedido;
        private Pedido _pedido;
        private PedidoProducto _pedidoProducto;
        private PedidoService _pedidoService;

        private Command _regresaCommand;

        public DetallePedidoViewModel(INavigation navigation, string Correo = null, string Token = null, int IdPedido = 0, List<DetallePedidoUsuario> model = null) : base(navigation, model)
        {
            if (model == null)
            {
                Model = new List<DetallePedidoUsuario>();
            }
            _pedidoService = new PedidoService();
            _correo = Correo;
            _token = Token;
            _idPedido = IdPedido;

            PedidoAction();
            DetallePedidoAction();
        }

        public List<DetallePedidoUsuario> DetallePedido
        {
            get => Model;
            set
            {
                if (Model == value) return;
                Model = value;

                OnPropertyChanged();
            }
        }

        public Pedido Pedido
        {
            get => _pedido;
            set
            {
                if (_pedido == value) return;
                _pedido = value;
                
                OnPropertyChanged();
            }
        }

        public PedidoProducto PedidoProducto
        {
            get => _pedidoProducto;
            set
            {
                if (_pedidoProducto == value) return;
                _pedidoProducto = value;

                OnPropertyChanged();
            }
        }
        
        public Command RegresarCommand
        {
            get => _regresaCommand ?? (_regresaCommand = new Command(RegresaAction));
        }

        private void RegresaAction()
        {
            Navigation.PushAsync(new View.Dashboard(_correo, _token));
        }

        private async void DetallePedidoAction()
        {
            DetallePedido = await _pedidoService?.DetallePedidoAsync(_idPedido, _token);            
        }

        private async void PedidoAction()
        {
            Pedido = await _pedidoService?.PedidoAsync(_idPedido, _token);
            PedidoProducto = await _pedidoService?.PedidosProductoAsync(_idPedido, _token);
        }

    }
}
