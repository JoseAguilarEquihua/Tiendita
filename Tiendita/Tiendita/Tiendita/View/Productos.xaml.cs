using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Tiendita.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tiendita.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Productos : ContentPage
    {
        public Productos(string Correo, int IdCarrito, string Token = null)
        {
            InitializeComponent();
            OnBackButtonPressed();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new ProductosViewModel(Navigation, Correo, IdCarrito, Token);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}