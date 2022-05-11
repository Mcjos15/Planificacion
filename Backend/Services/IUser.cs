using Backend.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IUser
    {
		Task<List<User>> GetAllUsers();
		Task<User> GetUser(string id);

		// add new note document
		Task AddUSer(User item);

		// remove a single document / note
		Task<bool> RemoveUser(string id);

		// update just a single document / note
		Task<bool> UpdateUser(string id, User user );

		// should be used with high cautious, only in relation with demo setup
		Task<bool> RemoveAllUser();
	}
}
