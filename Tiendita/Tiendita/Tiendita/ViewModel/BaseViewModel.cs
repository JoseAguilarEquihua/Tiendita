using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Tiendita.ViewModel
{
    public class BaseViewModel<T> : INotifyPropertyChanged
    {
        public T Model { get; set; }

        public INavigation Navigation { get; set; }

        public BaseViewModel(INavigation navigation, T model)
        {
            Model = model;
            Navigation = navigation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}
