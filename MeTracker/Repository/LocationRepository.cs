using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MeTracker.Models;
using SQLite;

namespace MeTracker.Repository
{
    public class LocationRepository : ILocationReposiroty
    {
        private SQLiteAsyncConnection connection;
        public LocationRepository()
        {
        }

        public async Task<List<Location>> GetAll()
        {
            await CreateConnection();

            return await connection.Table<Location>().ToListAsync();
        }

        public async Task Save(Location location)
        {
            await CreateConnection();
            await connection.InsertAsync(location);
        }

        private async Task CreateConnection()
        {
            if (connection != null)
                return;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Location.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<Location>();
        }
    }
}
