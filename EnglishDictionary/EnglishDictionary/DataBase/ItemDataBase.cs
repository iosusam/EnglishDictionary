using EnglishDictionary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishDictionary.DataBase
{
    public class ItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public ItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Words).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Words)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Words>> GetItemsAsync()
        {
            return Database.Table<Words>().OrderByDescending(x => x.dateRegister).ToListAsync();
        }

        public Task<List<Words>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Words>("SELECT * FROM [Words] WHERE [Done] = 0");
        }

        public Task<Words> GetItemAsync(int id)
        {
            return Database.Table<Words>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Words item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Words item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> DeleteAllItemAsync()
        {
            return Database.DeleteAllAsync<Words>();
        }
        public Task<int> GetCountAsync()
        {
            return Database.Table<Words>().CountAsync();
        }
        public Task<Words> GetItemRandomly()
        {
            return Database.Table<Words>().FirstOrDefaultAsync();
        }

        public Task<List<Words>> GetItemByOcurrencias(int ocurrencias)
        {
            return Database.Table<Words>().Where(i => i.Ocurrencias <= ocurrencias).ToListAsync();
        }

        public Task<int> UpdateItemBy(Words item)
        {
            return Database.UpdateAsync(item);
        }

        public Task<List<Words>> GetItemByName(string name)
        {
            return Database.Table<Words>().Where(i => i.English.Contains(name) || i.Spanish.Contains(name)).ToListAsync();
        }
    }
}
