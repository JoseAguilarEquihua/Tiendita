using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /*public T EncryptOrDecryptData(bool isEncrypt = true)
        {
            Type myType = Model.GetType();
            var newModel = Activator.CreateInstance(myType);

            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(Model);

                if (propValue != null && propValue.GetType() == typeof(string))
                {
                    if (isEncrypt)
                        prop.SetValue(newModel, Helpers.Base64Encrypter.Encrypt(propValue.ToString()));
                    else
                        prop.SetValue(newModel, Helpers.Base64Encrypter.Decrypt(propValue.ToString()));
                }
            }

            return (T)newModel;
        }
        public static string ModelToJson(T model)
        {
            return JsonSerializer.Serialize(model);
        }*/
    }
}
