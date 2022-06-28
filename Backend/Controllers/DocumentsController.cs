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
        public DocumentsController(IDocument iDocument,IConfiguraciones iConfig,IBloque iBloque)
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

        [HttpPost]
        [Route("deleteMany")]
        public async Task<List<Document>> deleteMany([FromBody] List<Document> list)
        {
            


            try
            {
                if( list != null ) 
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
            }catch(Exception e)
            {
                throw e;
            }
           

            //await _iDocument.RemoveDocument(id);

            
        }
        [HttpGet]
        [Route("mining")]
        public async Task<IActionResult> getMining()
        {


            

            try
            {
                List<Document> list = await _iDocument.GetAllDocuments();
                List<Bloque> listBloque = new List<Bloque>();
                List<Document> listDocumentsMining = new List<Document>();
                Bloque bloque;
                Bloque contBloque;

                int qRegistros = 5;
                int cont = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if((list.Count - i) >= qRegistros)
                    {
                        if (cont <= qRegistros)
                        {
                            bloque = new Bloque();
                            mining(bloque);
                            listBloque.Add(bloque);
                            if(_iBloque.getLAstBloque() == null)
                            {
                                bloque.hashPrevio = "0000000000000000000000000000000000000000000000000000000000000000";

                            }
                            else
                            {
                                contBloque = await _iBloque.getLAstBloque();
                                bloque.hashPrevio = contBloque.hash;
                            }

                            await _iBloque.AddBloque(bloque);

                        }
                    }
                    else
                    {
                        break;
                    }
                    
                }
                    


                return Ok();
            }
            catch (Exception)
            {
                return Problem("Problema al insertar"); ;
            }



        }

        public static void mining(Bloque bloque)
        {
            

           
          

                Thread thr = new Thread(new ThreadStart(ThreadProc));

                thr.Start();

                string date = dateToIn(DateTime.Now).ToString();

                string hash = GetSHA256(date + prueba);

                while (hash.Substring(0, 4) != "0000")
                {
                    prueba++;
                    date = dateToIn(DateTime.Now).ToString();
                    hash = GetSHA256(date + prueba);

                    Console.WriteLine(segundos + "//" + prueba + "//" + hash);
                }
                value = true;


                Console.WriteLine("-------------");

                Console.WriteLine(date + "//" + prueba + "//" + hash);
            //Console.WriteLine(dateDate);
            bloque.hash = hash;
            bloque.prueba = prueba;
            bloque.fechaMinado = DateTime.Parse(date);
            bloque.milisegundos = segundos;



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
