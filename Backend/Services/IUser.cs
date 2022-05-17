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

		Task<User?> GetUserByMail(string correo);

	
		Task AddUSer(User item);

	
		Task<bool> RemoveUser(string id);

	
		Task<bool> UpdateUser(string id, User user );

	
		Task<bool> RemoveAllUser();
	}
}
