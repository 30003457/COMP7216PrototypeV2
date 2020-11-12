using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using COMP7216Prototype.Controller;
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
        }
    }
}
