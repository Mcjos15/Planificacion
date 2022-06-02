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
    public class DocumentsController : Controller
    {
        private readonly IDocument _iDocument;
        public DocumentsController(IDocument iDocument)
        {
            _iDocument = iDocument;

        }

        [HttpGet]
        public async Task<List<Document>> get()
        {
            //Este sería el mepol
            return await _iDocument.GetAllDocuments();

        }

        [HttpPost]
        public async Task<IActionResult> post([FromBody] Document document)
        {

            try
            {
                await _iDocument.AddDocument(document);

                return CreatedAtAction(nameof(get), new { id = document.id }, document);
            }
            catch (Exception)
            {
                return Problem("Problema al insertar"); ;
            }

        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<Document>> getById([FromBody] Document document)
        {
            var docu = await _iDocument.GetDocumentById(document.id); 

            if (docu is null)
            {
                return Problem("Ningún documento encontrado");
            }
            return docu;
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> delete( string id)
        {

            var docu = await _iDocument.GetDocumentById(id);
            if (docu is null)
            {
                return NotFound();
            }

            await _iDocument.RemoveDocument(id);

            return Ok();
        }
    }
}
