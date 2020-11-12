using COMP7216Prototype.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using COMP7216Prototype.Model;

namespace COMP7216Prototype
{
   public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Customers>().Wait();
        }

        //Insert and Update new record  
        public Task<int> SaveItemAsync(Customers person)
        {
            if (person.customerId != 0)
            {
                return db.UpdateAsync(person);
            }
            else
            {
                return db.InsertAsync(person);
            }
        }

        //Delete  
        public Task<int> DeleteItemAsync(Customers person)
        {
            return db.DeleteAsync(person);
        }

        //Read All Items  
        public Task<List<Customers>> GetItemsAsync()
        {
            return db.Table<Customers>().ToListAsync();
        }


        //Read Item  
        public Task<Customers> GetItemAsync(string userEmail)
        {
            return db.Table<Customers>().Where(i => i.email == userEmail).FirstOrDefaultAsync();
        }
        public Task<Customers> GetEmailAsync(string userEmail)
        {
            return db.Table<Customers>().Where(i => i.email == userEmail).FirstOrDefaultAsync();
        }
        public Task<Customers> GetPasswordAsync(string userPassword)
        {
            return db.Table<Customers>().Where(i => i.password == userPassword).FirstOrDefaultAsync();
        }
    }
}

