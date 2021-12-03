using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiendita.ViewModel;
using Xamarin.Forms;

namespace Tiendita
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            OnBackButtonPressed();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new LoginViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
