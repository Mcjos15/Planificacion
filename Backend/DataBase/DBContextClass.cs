using Backend.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataBase
{

    public class DBContextClass
    {
        private readonly IMongoDatabase _database = null;

        public DBContextClass(IOptions<Mongo> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DatabaseName);

        }

        public IMongoCollection<User> user
        {
            get
            {
                return _database.GetCollection<User>("usuarios");
            }


        }
    }
}
