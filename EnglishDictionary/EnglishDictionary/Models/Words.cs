using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EnglishDictionary.Models
{
    public class Words
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string English { get; set; }
        public string Spanish { get; set; }
        public int Ocurrencias { get; set; }
        public int Errores { get; set; }
    }
}
