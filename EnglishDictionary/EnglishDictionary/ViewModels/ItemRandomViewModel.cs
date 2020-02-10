using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EnglishDictionary.Models;
using Xamarin.Forms;

namespace EnglishDictionary.ViewModels
{
    public class ItemRandomViewModel : INotifyPropertyChanged
    {
        Words item = null;
        public Words Item {
            get { return item; }
            set { SetProperty(ref item, value); }
        }

        string respuesta = string.Empty;
        public string Respuesta
        {
            get { return respuesta; }
            set { SetProperty(ref respuesta, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        public ItemRandomViewModel()
        {
            Title = "Game";
            Item = Constants.getItemRandomly();
            Respuesta = "";
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
