using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.models;

namespace Backend.Logic
{
    public class Minado
    {
        private readonly IDocument _iDocument;
        private readonly IBloque _iBloque;
        private readonly IConfiguraciones _iConfig;
        private readonly IMining _iMining;

        private bool value = false;
        private int prueba = 0;
        private int segundos = 0;

        public Minado(IDocument iDocument, IConfiguraciones iConfig, IBloque iBloque, IMining iMining) {
            _iDocument = iDocument;
            _iConfig = iConfig;
            _iBloque = iBloque;
            _iMining = iMining;
        }


        public void getMinigBloque(List<Document> list, Configuraciones config) {
            
            List<Document> listDocumentsMining = new List<Document>();
            List<Document> list3 =  _iDocument.GetAllDocuments().Result;
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
                        contBloque = _iBloque.getLAstBloque().Result;
                        if (contBloque != null)
                        {
                           // contBloque = _iBloque.getLAstBloque();
                            bloque.hashPrevio = contBloque.hash;

                            bloque.idBloque = contBloque.idBloque + 1;

                        }
                        else
                        {
                            bloque.hashPrevio = "0000000000000000000000000000000000000000000000000000000000000000";
                            bloque.idBloque = 1;
                        }
                        bloque.documentos = listDocumentsMining;
                        mining(bloque);
                         _iBloque.AddBloque(bloque);
                         deleteMany(listDocumentsMining);
                        listDocumentsMining = new List<Document>();
                        cont = 1;
                    }
                    else
                    {
                        cont++;
                    }

                }
                Console.WriteLine("sali");
                
            }

        }

        public void deleteMany( List<Document> list)
        {
            try
            {
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var docu = _iDocument.GetDocumentById(list[i].id);
                        if (docu != null)
                        {
                             _iDocument.RemoveDocument(list[i].id);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void mining(Bloque bloque)
        {

            string idDocuments = "";
            DateTime dateT = DateTime.Now;
            foreach (Document docu in bloque.documentos)
            {
                idDocuments += docu.Base64 + " - ";
            }
            idDocuments = _iMining.GetSHA256(idDocuments);


            string date = dateToIn(dateT).ToString(); ;

            string hash = _iMining.GetSHA256(date + prueba + bloque.toStringM() + idDocuments);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();



            while (hash.Substring(0, 4) != "0000")
            {
                prueba++;
                dateT = DateTime.Now;
                date = dateToIn(dateT).ToString();
                hash = _iMining.GetSHA256(date + prueba + bloque.toStringM() + idDocuments);

                Console.WriteLine(segundos + "//" + prueba + "//" + hash);
            }

            stopwatch.Stop();
            value = true;
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
        public void ThreadProc()
        {

            while (!value)
            {
                Thread.Sleep(1000);
                prueba = 0;
                segundos++;
            }
        }

    }
}
