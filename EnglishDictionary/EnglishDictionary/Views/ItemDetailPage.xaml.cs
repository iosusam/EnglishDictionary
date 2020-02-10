using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EnglishDictionary.Models;
using EnglishDictionary.ViewModels;

namespace EnglishDictionary.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
            
            var item = new Words
            {
                English = "Item 1",
                Spanish = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void update_item(object sender, EventArgs e)
        {
            await App.Database.UpdateItemBy(viewModel.Item);
            await Navigation.PopModalAsync();
        }
    }
}