using System;
using System.Linq;
using GamerHub.mobile.core.Models.Db;
using GamerHub.mobile.core.Services.Db;
using SQLite;

namespace GamerHub.mobile.android.Services
{
    public class SqlLiteService : ISqlLiteService
    {
        private readonly SQLiteConnection _sqlLiteConnection;
        public SqlLiteService()
        {
            var dbName = "gamershub.sqlite";
                var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var path = System.IO.Path.Combine(dbPath, dbName);
                _sqlLiteConnection = new SQLiteConnection(path);
                _sqlLiteConnection.CreateTable<UserCredentialsModel>();
            }

        public UserCredentialsModel GetCredentialsStoredInDb() =>
            (from c in _sqlLiteConnection.Table<UserCredentialsModel>() select c)
            .FirstOrDefault();

        public void SaveCredentials(UserCredentialsModel model)
        {
            _sqlLiteConnection.DropTable<UserCredentialsModel>();
                _sqlLiteConnection.CreateTable<UserCredentialsModel>();

                _sqlLiteConnection.Insert(model);
        }
    }
}