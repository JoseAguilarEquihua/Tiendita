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
    public partial class Cart : ContentPage
    {
        public Cart(string Correo, int IdCarrito)
        {
            InitializeComponent();
            BindingContext = new CarritoViewModel(Navigation, Correo, IdCarrito);
        }
    }
}