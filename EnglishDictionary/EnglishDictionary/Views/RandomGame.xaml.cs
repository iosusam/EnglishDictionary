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

        async void OnButtonCheckClicked(object sender, EventArgs args)
        {
            //The upper or lower case doesnt matter
            String user_answer = viewModel.Respuesta.ToLower();
            String correct_anser = viewModel.Item.Spanish.ToLower();

            //Check the answer responded by the user
            if (correct_anser.Contains(user_answer) && user_answer != "")
            {
                if (correct_anser == user_answer)
                {
                    await DisplayAlert("GOOD JOB", "", "NEXT");
                    viewModel.Item = Constants.getItemRandomly();
                    viewModel.Respuesta = "";
                }
                else
                {
                    await DisplayAlert("GOOD JOB", "Same meaning: " + viewModel.Item.Spanish, "NEXT");
                    viewModel.Item = Constants.getItemRandomly();
                    viewModel.Respuesta = "";
                }
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

        async void OnButtonGiveUpClicked(object sender, EventArgs args)
        {
            await DisplayAlert(viewModel.Item.Spanish, "", "OK");
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}