using Backend.DataBase;
using Backend.Interfaces;
using Backend.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace Backend.Services
{


    public class UserService : IUser
    {
        //private readonly DBContextClass context = null;
        private readonly IMongoCollection<User> _users;
        public UserService(IOptions<Mongo> mongo)
        {
            //context = new DBContextClass(mongo);

            var mongoClient = new MongoClient(mongo.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongo.Value.DatabaseName);

            // _users = mongoDatabase.GetCollection<User>(mongo.Value.UsuariosCollectionName);
            _users = mongoDatabase.GetCollection<User>("usuarios");
            
        }

        public async Task AddUSer(User item) =>
        await _users.InsertOneAsync(item);

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }

            public async Task<User> GetUserByMail(string correo)
        {
            try
            {

                var builder = Builders<User>.Filter;
                var filter = builder.Eq(User => User.correo,correo);

                return await _users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }


        public Task<bool> RemoveAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
