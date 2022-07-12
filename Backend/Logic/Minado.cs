using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Backend.models;

namespace Backend.Logic
{
    public class Minado
    {

        private bool value = false;
        private int prueba = 0;
        private int segundos = 0;

        public Minado(bool value, int prueba, int segundos)
        {
            this.prueba = prueba;
            this.value = value;
            this.segundos = segundos;
        }


       

        public void mining(Bloque bloque)
        {

            string idDocuments = "";
            DateTime dateT = DateTime.Now;
            foreach (Document docu in bloque.documentos)
            {
                idDocuments += docu.Base64 + " - ";
            }
            idDocuments = GetSHA256(idDocuments);


            string date = dateToIn(dateT).ToString(); ;

            string hash = GetSHA256(date + prueba + bloque.toStringM() + idDocuments);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();



            while (hash.Substring(0, 4) != "0000")
            {
                prueba++;
                dateT = DateTime.Now;
                date = dateToIn(dateT).ToString();
                hash = GetSHA256(date + prueba + bloque.toStringM() + idDocuments);

                Console.WriteLine(segundos + "//" + prueba + "//" + hash);
            }

            stopwatch.Stop();

            value = true;

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
        public void ThreadProc()
        {

            while (!value)
            {
                Thread.Sleep(1000);
                prueba = 0;
                segundos++;
            }
        }
        public string GetSHA256(string str)
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
