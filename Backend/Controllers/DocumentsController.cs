using Backend.Interfaces;
using Backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

using System.Threading;
using System.Diagnostics;
using Backend.Logic;

namespace Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly IDocument _iDocument;
        private readonly IBloque _iBloque;
        private readonly IConfiguraciones _iConfig;
        private readonly IMining _iMining;
        public DocumentsController(IDocument iDocument, IConfiguraciones iConfig, IBloque iBloque, IMining iMining)
        {
            _iDocument = iDocument;
            _iConfig = iConfig;
            _iBloque = iBloque;
            _iMining = iMining;

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
        [Route("insertMany")]
        public async Task<IActionResult> inserList([FromBody] List<Document> document)
        {

            try
            {
                await _iDocument.addDocuments(document);

                return Ok();
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
        public async Task<IActionResult> delete(string id)
        {

            var docu = await _iDocument.GetDocumentById(id);
            if (docu is null)
            {
                return NotFound();
            }

            await _iDocument.RemoveDocument(id);

            return Ok();
        }

        [HttpPost]
        [Route("deleteMany")]
        public async Task<List<Document>> deleteMany([FromBody] List<Document> list)
        {



            try
            {
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var docu = await _iDocument.GetDocumentById(list[i].id);
                        if (docu != null)
                        {
                            await _iDocument.RemoveDocument(list[i].id);
                        }

                    }
                }


                return await get();
            }
            catch (Exception e)
            {
                throw e;
            }


            //await _iDocument.RemoveDocument(id);


        }
        //--------------MINADO---------------------------------------------------------
        [HttpGet]
        [Route("mining")]
        public async Task<IActionResult> getMining()
        {
            try
            {
                List<Document> list = await _iDocument.GetAllDocuments();
               
                Configuraciones config = await _iConfig.getNumberDocuments();

                Minado minado = new Minado(_iDocument, _iConfig, _iBloque, _iMining);

                minado.getMinigBloque(list, config);

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }

    }


}
