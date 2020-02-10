using System;
using System.ComponentModel;
using Xamarin.Forms;
using EnglishDictionary.ViewModels;

namespace EnglishDictionary.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RandomGame : ContentPage
    {
        ItemRandomViewModel viewModel;
        

        public RandomGame()
        {
            InitializeComponent();

            BindingContext = this.viewModel = new ItemRandomViewModel();
        }
        
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            //Check the answer responded by the user
            if (viewModel.Item.Spanish == viewModel.Respuesta)
            {
                await DisplayAlert("GOOD JOB", "", "NEXT");
                viewModel.Item = Constants.getItemRandomly();
                viewModel.Respuesta = "";
            }
            else
            {
                bool answer = await DisplayAlert("BAD ANSWER", "", "NEXT", "TRY AGAIN");
                if (answer)
                {
                    viewModel.Item = Constants.getItemRandomly();
                    viewModel.Respuesta = "";
                }
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}