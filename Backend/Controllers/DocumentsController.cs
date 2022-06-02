﻿using Backend.Interfaces;
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
        public async Task<List<Document>> GetAll()
        {
            //Este sería el mepol
            return await _iDocument.GetAllDocuments();

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Document document)
        {

            try
            {
                await _iDocument.AddDocument(document);

                return CreatedAtAction(nameof(GetAll), new { id = document.id }, document);
            }
            catch (Exception)
            {
                return Problem("Problema al insertar"); ;
            }

        }

        public async Task<ActionResult<Document>> Get([FromBody] Document document)
        {
            var docu = await _iDocument.GetDocumentById(document.id); 

            if (docu is null)
            {
                return Problem("Ningún documento encontrado");
            }
            return docu;
        }
    }
}
