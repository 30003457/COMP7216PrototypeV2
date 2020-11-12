using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using SQLite;

namespace COMP7216Prototype.Controller
{
    public class DataAccessLayer
    {
        public const string DatabaseFilename = "CreditShareSystem.db3";
        public SQLiteConnection db { get; set; }

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
            CreateDummyTables();
        }

        private void CreateDummyTables()
        {
            //customers
            db.CreateTable<Customers>();
            db.Insert(new Customers
            {
                firstName = "Bob",
                lastName = "Crawford",
                address = "123 Some Street",
                email = "bob.crawford@email.com",
                password = "123123123",
                phoneNumber = "123-123123"
            });

            db.Insert(new Customers
            {
                firstName = "Joe",
                lastName = "Crawford",
                address = "123 Some Street",
                email = "joe.crawford@email.com",
                password = "123123123",
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
                creditType = "GB"
            });
            db.Insert(new CreditTypes
            {
                creditType = "texts"
            });

            //credits
            db.CreateTable<Credits>();
            db.Insert(new Credits
            {
                amount = 100,
                creditTypeId = 1,
                customerId = 1
            });

            //credit requests
            db.CreateTable<CreditRequests>();
            db.Insert(new CreditRequests
            {
                creditAmount = 20,
                creditTypeId = 1,
                requestAccepted = true,
                requesterId = 2,
                shareId = 1,
                shareUserId = 1,
                timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                timeStampTime = DateTime.Now.ToString("hh:mm")
            });

            //credit shares
            db.CreateTable<CreditShares>();
            db.Insert(new CreditShares
            {
                creditAmount = 20,
                creditTypeId = 1,
                receiverUserId = 2,
                requestId = -1,
                shareUserId = 1,
                timeStampDate = DateTime.Now.ToString("dd/MM/yyyy"),
                timeStampTime = DateTime.Now.ToString("hh:mm")
            });
        }
    }
}
