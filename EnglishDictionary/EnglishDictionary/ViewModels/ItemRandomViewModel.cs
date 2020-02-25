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
        int block = 0;
        public List<Words> listItemsRange = new List<Words>();
        public int numberOfWords = App.Database.GetCountAsync().Result;
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

        public int itemNumber = 0;
        string itemNumberString = string.Empty;
        public string ItemNumberString
        { 
            get { return itemNumberString; }
            set { SetProperty(ref itemNumberString, value); }
        }

public ItemRandomViewModel(int blockpass = 0)
        {
            block = blockpass;
            Title = "Game";
            if (block == 0)
            {
                Item = Constants.getItemRandomly();
            }
            else
            {
                listItemsRange = Constants.getItemsBlock(blockpass);
                Item = listItemsRange[itemNumber];
                itemNumber++;
                ItemNumberString = itemNumber.ToString() + " / " + listItemsRange.Count.ToString();
                //Number of words in the set
                /*int numberOfWordsSet = Constants.setNumber * block;
                if (numberOfWordsSet>numberOfWords)
                {
                    //Exact words in the las set
                    exactWords = (Constants.setNumber * (block-1)) + numberOfWords;
                    ItemNumberString = itemNumber.ToString() + " / " + exactWords.ToString();
                }
                else
                {
                    ItemNumberString = itemNumber.ToString() + " / " + Constants.setNumber;
                    exactWords = Constants.setNumber;
                }*/

            }
            
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
