using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tiendita.ViewModel;

namespace Tiendita.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard(string Correo, string Token)
        {
            InitializeComponent();
            BindingContext = new DashboardViewModel(Navigation, Correo, Token);
        }
    }

}