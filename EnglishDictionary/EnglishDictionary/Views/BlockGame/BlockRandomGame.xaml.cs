using System;
using System.ComponentModel;
using Xamarin.Forms;
using EnglishDictionary.ViewModels;

namespace EnglishDictionary.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class BlockRandomGame : ContentPage
    {
        ItemRandomViewModel viewModel;
        int block = 0;

        public BlockRandomGame(int blockpass)
        {
            InitializeComponent();
            block = blockpass;
            BindingContext = this.viewModel = new ItemRandomViewModel(block);
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
                    getNextItem();
                }
                else
                {
                    await DisplayAlert("GOOD JOB", "Same meaning: " + viewModel.Item.Spanish, "NEXT");
                    getNextItem();
                }
            }
            else
            {
                bool answer = await DisplayAlert("BAD ANSWER", "", "NEXT", "TRY AGAIN");
                if (answer)
                {
                    getNextItem();
                }
            }
        }

        async void OnButtonGiveUpClicked(object sender, EventArgs args)
        {
            await DisplayAlert(viewModel.Item.Spanish, "", "OK");
            getNextItem();            
        }

        private async void getNextItem()
        {
            //Check if the block is finished
            if (viewModel.itemNumber >= viewModel.listItemsRange.Count)
            {
                bool answer = await DisplayAlert(Constants.setFinish, "", "ANOTHER SET", "TRY AGAIN");
                if(answer)
                {
                    await Navigation.PopAsync();
                    //getNextItem();
                }
                else
                {
                    viewModel.itemNumber = 0;
                    viewModel.Item = viewModel.listItemsRange[viewModel.itemNumber];
                    viewModel.itemNumber++;
                    viewModel.Respuesta = "";
                }
            }
            else
            {
                viewModel.Item = viewModel.listItemsRange[viewModel.itemNumber];
                viewModel.itemNumber++;
                viewModel.Respuesta = "";
            }
            viewModel.ItemNumberString = viewModel.itemNumber.ToString() + " / " + viewModel.listItemsRange.Count.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}