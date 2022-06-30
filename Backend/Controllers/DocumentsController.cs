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

namespace Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly IDocument _iDocument;
        private readonly IBloque _iBloque;
        private readonly IConfiguraciones _iConfig;
        static bool value = false;
        static int prueba = 0;
        static int segundos = 0;
        public DocumentsController(IDocument iDocument, IConfiguraciones iConfig, IBloque iBloque)
        {
            _iDocument = iDocument;
            _iConfig = iConfig;
            _iBloque = iBloque;

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
                List<Document> listDocumentsMining = new List<Document>();
                Configuraciones config = await _iConfig.getNumberDocuments();
                Bloque bloque;
                Bloque contBloque;

                int qRegistros = Convert.ToInt32(config.numeroRegistro);
                int cont = 1;


                if (list.Count >= qRegistros)
                {
                    for (int i = 0; i < list.Count; i++)
                    {

                        listDocumentsMining.Add(list[i]);

                        if (cont == qRegistros)
                        {
                            bloque = new Bloque();
                            if (await _iBloque.getLAstBloque() != null)
                            {
                                contBloque = await _iBloque.getLAstBloque();
                                bloque.hashPrevio = contBloque.hash;
                               
                                bloque.idBloque = contBloque.idBloque+1;
                                Console.WriteLine("-------------");

                            }
                            else
                            {
                                bloque.hashPrevio = "0000000000000000000000000000000000000000000000000000000000000000";
                                bloque.idBloque = 1;
                            }
                            bloque.documentos = listDocumentsMining;
                            mining(bloque);
                            await _iBloque.AddBloque(bloque);
                           // await deleteMany(listDocumentsMining);
                            listDocumentsMining = new List<Document>();
                            cont = 1;
                        }
                        else
                        {
                            cont++;
                        }

                    }
                    return Ok(list.Count);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }

        public static void mining(Bloque bloque)
        {

            string idDocuments = "";
            DateTime dateT = DateTime.Now;
            foreach (Document docu in bloque.documentos)
            {
                idDocuments += docu.Base64 + " - ";
            }
            idDocuments = GetSHA256(idDocuments);


            string date = dateToIn(dateT).ToString(); ;

            string hash = GetSHA256(date + prueba + bloque.toStringM()+idDocuments);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            

            while (hash.Substring(0, 4) != "0000")
            {
                prueba++;
                dateT = DateTime.Now;
                date = dateToIn(dateT).ToString();
                hash = GetSHA256(date + prueba + bloque.toStringM()+ idDocuments);

                Console.WriteLine(segundos + "//" + prueba + "//" + hash);
            }

            stopwatch.Stop();
           
            value = true;

            Console.WriteLine("-------------");
            Console.WriteLine(date + "//" + prueba + "//" + hash);
            //Console.WriteLine(dateDate);
            bloque.hash = hash;
            bloque.prueba = prueba;
            bloque.fechaMinado = dateT;
            bloque.milisegundos = (int)stopwatch.ElapsedMilliseconds;
        }
        public static long dateToIn(DateTime dataTime)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            return (dataTime.Ticks - centuryBegin.Ticks);
            //TimeSpan elapsedSpan = new TimeSpan(elapsedTicks));
        }
        public static void ThreadProc()
        {

            while (!value)
            {
                Thread.Sleep(1000);
                prueba = 0;
                segundos++;
            }
        }
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }


}
