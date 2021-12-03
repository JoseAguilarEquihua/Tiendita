using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiendita.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tiendita.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDashboard : ContentPage
    {
        public DetalleDashboard(string Correo, string Token, int IdPedido)
        {
            InitializeComponent();
            BindingContext = new DetallePedidoViewModel(Navigation, Correo, Token, IdPedido);
        }
    }
}