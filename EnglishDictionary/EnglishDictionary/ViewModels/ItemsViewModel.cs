using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using EnglishDictionary.Models;
using EnglishDictionary.Views;
using System.Collections.Generic;

namespace EnglishDictionary.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Words> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        
        public ItemsViewModel()
        {
            Title = "Dictionary";
            Items = new ObservableCollection<Words>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                //Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

        }

        public Command<Words> removeWord
        {
            get
            {
                return new Command<Words>((words) =>
                {
                    Items.Remove(words);
                });
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                List<Words> diccionario = App.Database.GetItemsAsync().Result;

                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in diccionario)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}