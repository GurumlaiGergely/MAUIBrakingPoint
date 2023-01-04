using BrakingPoint.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrakingPoint.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection connection;

        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<LoginModel>().Wait();
        }

        public Task<LoginModel> GetLoginDataAsync(string username)
        {
            return connection.Table<LoginModel>().Where(i => i.UserName == username).FirstOrDefaultAsync();
        }

        public Task<int> SaveLoginDataAsync(LoginModel logindata) 
        {
            return connection.InsertAsync(logindata);
        }
    }
}
