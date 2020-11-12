using COMP7216Prototype.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COMP7216Prototype
{
   public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Users>().Wait();
        }

        //Insert and Update new record  
        public Task<int> SaveItemAsync(Users person)
        {
            if (person.ID != 0)
            {
                return db.UpdateAsync(person);
            }
            else
            {
                return db.InsertAsync(person);
            }
        }

        //Delete  
        public Task<int> DeleteItemAsync(Users person)
        {
            return db.DeleteAsync(person);
        }

        //Read All Items  
        public Task<List<Users>> GetItemsAsync()
        {
            return db.Table<Users>().ToListAsync();
        }


        //Read Item  
        public Task<Users> GetItemAsync(string userEmail)
        {
            return db.Table<Users>().Where(i => i.Email == userEmail).FirstOrDefaultAsync();
        }
        public Task<Users> GetEmailAsync(string userEmail)
        {
            return db.Table<Users>().Where(i => i.Email == userEmail).FirstOrDefaultAsync();
        }
        public Task<Users> GetPasswordAsync(string userPassword)
        {
            return db.Table<Users>().Where(i => i.Password == userPassword).FirstOrDefaultAsync();
        }
    }
}

