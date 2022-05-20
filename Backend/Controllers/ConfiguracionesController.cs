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
    public class ConfiguracionesController : Controller
    {
        private readonly IConfiguraciones _iConfiguraciones;

        public ConfiguracionesController(IConfiguraciones iConfiguraciones)
        {
            _iConfiguraciones = iConfiguraciones;
        }

        [HttpGet]
        [Route("getAllConfig")]
        public async Task<List<Configuraciones>> GetAllConfig()
        {

            return await _iConfiguraciones.GetAllConfigs();

        }

        [HttpPost]
        [Route("addConfig")]
        public async Task<IActionResult> Post([FromBody] Configuraciones configuraciones)
        {
            await _iConfiguraciones.AddConfig(configuraciones);

            return Ok();
        }
    }
}
