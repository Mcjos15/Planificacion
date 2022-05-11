using Backend.DataBase;
using Backend.Interfaces;
using Backend.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{


    public class UserService : IUser
    {
        private readonly DBContextClass context = null;

        public UserService(IOptions<Mongo> mongo)
        {
            context = new DBContextClass(mongo);
        }

        public async Task AddUSer(User item) =>
        await context.user.InsertOneAsync(item);

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await context.user.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }

            public Task<User> GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUser(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            throw new NotImplementedException();
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
