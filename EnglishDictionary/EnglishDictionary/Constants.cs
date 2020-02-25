using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EnglishDictionary.Models;

namespace EnglishDictionary
{
    public static class Constants
    {
        public const string DatabaseFilename = "englishWords.db3";
        public static int setNumber = 10;

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        public static Words getItemRandomly()
        {
            //Get all items from DB
            List<Words> fullList = App.Database.GetItemsAsync().Result;

            //Get item to play
            List<Words> playListWords = getItemsToPlay(fullList);

            //Random number between list selected
            int numItemsTotal = App.Database.GetCountAsync().Result;
            int randomNumber = new Random().Next(0, numItemsTotal-1);

            return playListWords[randomNumber];
        }

        private static List<Words> getItemsToPlay(List<Words> listaItems)
        {
            int countRepeticiones = 0;
            int mediaRepeticiones = 0;
            int numItemsTotal = App.Database.GetCountAsync().Result;
            //Recorrer todos los items y quedarse con la media de repeticiones
            foreach (Words item in listaItems)
            {
                mediaRepeticiones += item.Ocurrencias;
            }
            mediaRepeticiones = countRepeticiones / numItemsTotal;

            return App.Database.GetItemByOcurrencias(mediaRepeticiones).Result;

        }

        public static List<Words> getItemsBlock(int block)
        {
            //Return list with items range
            List<Words> resultList = new List<Words>();
            //Get all items from DB
            List<Words> fullList = App.Database.GetItemsAsync().Result;
            //Get count items
            int countItems = App.Database.GetCountAsync().Result;

            //Item block range
            int minItem = ((block-1) * setNumber);

            //Get Items
            if (minItem + setNumber > countItems)
            {
                resultList = fullList.GetRange(minItem, countItems - minItem);
            }
            else
            {
                resultList = fullList.GetRange(minItem, setNumber);
            }

            return resultList;
        }

        public static string resetConfirmation = "¿Are you sure to delete al diccionary?";
        public static string setFinish = "This set is finish";
    }
}
