using System;

using EnglishDictionary.Models;

namespace EnglishDictionary.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Words Item { get; set; }
        public ItemDetailViewModel(Words item = null)
        {
            Title = item?.English;
            Item = item;
        }
    }
}
