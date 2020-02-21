using System;
using System.ComponentModel;
using Xamarin.Forms;
using EnglishDictionary.ViewModels;
using System.Collections.Generic;
using EnglishDictionary.Models;
using System.Text.RegularExpressions;

namespace EnglishDictionary.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class BlockGame : ContentPage
    {
        ItemRandomViewModel viewModel;
        

        public BlockGame()
        {
            InitializeComponent();

            //Get count of words in the dictionary
            int numberOfWords = App.Database.GetCountAsync().Result;

            var listBlocks = new List<String>();

            double division = (double)numberOfWords /  (double)Constants.setNumber;
            double numberOfBlocks = Math.Ceiling(division);

            //Create block numbers
            for (int i=0; i< numberOfBlocks; i++)
            {
                listBlocks.Add("Set " + (i+1).ToString());
            }

            var dataTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                var stacklayout = new StackLayout
                {
                    Padding = 10,
                };
                var label = new Label()
                {
                    FontSize = 20,
                    TextColor = Color.FromHex("#5478ED"),
                };
                label.SetBinding(Label.TextProperty, ".");
                stacklayout.Children.Add(label);
                cell.View = stacklayout;
                return cell;
            });
                
            ItemsListView.ItemsSource = listBlocks;

            ItemsListView.ItemTemplate = dataTemplate;


        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as String;
            if (item == null)
                return;

            //Get number os the set
            String resultString = Regex.Match(item, @"\d+").Value;
            int numberSet = Int32.Parse(resultString);

            await Navigation.PushAsync(new BlockRandomGame(numberSet));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}