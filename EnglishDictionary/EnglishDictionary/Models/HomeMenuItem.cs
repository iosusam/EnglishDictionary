using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishDictionary.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        RandomGame,
        Reset
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
