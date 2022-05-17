using Backend.Interfaces;
using Backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUser _iUser;// = new IUser();
        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }
        // GET: userController
        // [NoCache]
        [HttpGet]
        public async Task<List<User>> GetAll()
        {

            return await _iUser.GetAllUsers();

        }
        [HttpPost]
       // [Route("add")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
             await _iUser.AddUSer(user);

            return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        }
        //[HttpGet("{correo}")]
        [Route("search")]
        //[HttpGet("{correo}")]
        public async Task<ActionResult<User>> Get([FromBody] User userFW)
        {
          var user =  await _iUser.GetUserByMail(userFW.correo);

            if (user is null)
            {
                return Problem("Ningún usuario coincide con el correo ingresado");;
            }
            return user;
        }

    }
}
