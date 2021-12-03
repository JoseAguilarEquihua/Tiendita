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
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
            OnBackButtonPressed();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new UsuarioViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}