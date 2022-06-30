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
        [HttpGet]
        [Route("getConfig/{id}")]
        public async Task<ActionResult<Configuraciones>> Get(string id) {

            var config = await _iConfiguraciones.getConfig(id);
            if (config is null) {
                return NotFound();
            }
            return config;
        }

        [HttpPut]
        [Route("updateConfig/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Configuraciones configuraciones) {

            var config = await _iConfiguraciones.getConfig(id);
            if (config is null)
            {
                return NotFound();
            }

            configuraciones.id = config.id;

            await _iConfiguraciones.Updateconfig(id, configuraciones);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteConfig/{id}")]
        public async Task<IActionResult> Delete(string id){

            var config = await _iConfiguraciones.getConfig(id);
            if (config is null)
            {
                return NotFound();
            }

            await _iConfiguraciones.RemoveConfig(id);

            return Ok();
        }

        [HttpGet]
        [Route("getNumberDocuments")]
        public async Task<ActionResult<Configuraciones>> getNumberDocuments()
        {

            var config = await _iConfiguraciones.getNumberDocuments();
            if (config is null)
            {
                return NotFound();
            }
            return config;
        }


    }
}
