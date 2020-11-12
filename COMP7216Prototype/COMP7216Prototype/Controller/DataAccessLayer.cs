using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using SQLite;

namespace COMP7216Prototype.Controller
{
    public class DataAccessLayer
    {
        public const string DatabaseFilename = "CreditShareSystem.db3";
        public SQLiteConnection db { get; set; }
        public SQLiteAsyncConnection dbAsync { get; set; }

        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        public string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        public DataAccessLayer()
        {
            db = new SQLiteConnection(DatabasePath);
            dbAsync = new SQLiteAsyncConnection(DatabasePath);
            //CreateDummyTables();
        }

        public void CreateDummyTables()
        {
            db.DropTable<Customers>();
            db.DropTable<CreditTypes>();
            db.DropTable<Credits>();
            db.DropTable<CreditRequests>();
            db.DropTable<CreditShares>();

            //customers
            db.CreateTable<Customers>();
            db.Insert(new Customers
            {
                firstName = "Bob",
                lastName = "Crawford",
                address = "123 Some Street",
                email = "bob@email.com",
                password = "1",
                phoneNumber = "123-123123"
            });

            db.Insert(new Customers
            {
                firstName = "Joe",
                lastName = "Crawford",
                address = "123 Some Street",
                email = "joe@email.com",
                password = "1",
                phoneNumber = "123-123123"
            });

            //credit types
            db.CreateTable<CreditTypes>();
            db.Insert(new CreditTypes
            {
                creditType = "dollars"
            });
            db.Insert(new CreditTypes
            {
                creditType = "minutes"
            });
            db.Insert(new CreditTypes
            {
                creditType = "GB data"
            });
            db.Insert(new CreditTypes
            {
                creditType = "texts"
            });

            //credits
            //Bob
            db.CreateTable<Credits>();
            db.Insert(new Credits
            {
                amount = 100,
                creditTypeId = 1,
                customerId = 1
            });
            db.Insert(new Credits
            {
                amount = 100,
                creditTypeId = 2,
                customerId = 1
            });
            db.Insert(new Credits
            {
                amount = 100,
                creditTypeId = 3,
                customerId = 1
            });
            db.Insert(new Credits
            {
                amount = 100,
                creditTypeId = 4,
                customerId = 1
            });

            //Joe
            db.Insert(new Credits
            {
                amount = 0,
                creditTypeId = 1,
                customerId = 2
            });
            db.Insert(new Credits
            {
                amount = 0,
                creditTypeId = 2,
                customerId = 2
            });
            db.Insert(new Credits
            {
                amount = 0,
                creditTypeId = 3,
                customerId = 2
            });
            db.Insert(new Credits
            {
                amount = 0,
                creditTypeId = 4,
                customerId = 2
            });

            ////credit requests
            db.CreateTable<CreditRequests>();
            //db.Insert(new CreditRequests
            //{
            //    creditAmount = 20,
            //    creditTypeId = 1,
            //    requestAccepted = true,
            //    requesterId = 2,
            //    shareId = 1,
            //    shareUserId = 1,
            //    timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
            //    timeStampTime = DateTime.Now.ToString("hh:mm")
            //});

            ////credit shares
            db.CreateTable<CreditShares>();
            //db.Insert(new CreditShares
            //{
            //    creditAmount = 20,
            //    creditTypeId = 1,
            //    receiverUserId = 2,
            //    requestId = -1,
            //    shareUserId = 1,
            //    timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
            //    timeStampTime = DateTime.Now.ToString("hh:mm")
            //});
        }

        //Insert and Update new record  
        public Task<int> SaveItemAsync(Customers person)
        {
            if (person.customerId != 0)
            {
                return dbAsync.UpdateAsync(person);
            }
            else
            {
                return dbAsync.InsertAsync(person);
            }
        }

        //Delete  
        public Task<int> DeleteItemAsync(Customers person)
        {
            return dbAsync.DeleteAsync(person);
        }

        //Read All Items  
        public Task<List<Customers>> GetItemsAsync()
        {
            return dbAsync.Table<Customers>().ToListAsync();
        }


        //Read Item  
        public Task<Customers> GetItemAsync(string userEmail)
        {
            return dbAsync.Table<Customers>().Where(i => i.email == userEmail).FirstOrDefaultAsync();
        }
        public Task<Customers> GetEmailAsync(string userEmail)
        {
            return dbAsync.Table<Customers>().Where(i => i.email == userEmail).FirstOrDefaultAsync();
        }
        public Task<Customers> GetPasswordAsync(string userPassword)
        {
            return dbAsync.Table<Customers>().Where(i => i.password == userPassword).FirstOrDefaultAsync();
        }
    }
}
