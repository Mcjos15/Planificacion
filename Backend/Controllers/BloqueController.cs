using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BloqueController : Controller
    {
        private readonly IBloque _iBloque;
        public BloqueController(IBloque iBloque)
        {
            _iBloque = iBloque;
        }


        [HttpGet]
        public async Task<List<Bloque>> get()
        {
            //Este sería el mepol
            return await _iBloque.GetAllBloques();

        }
    }
}
