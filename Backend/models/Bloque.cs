using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Backend.models
{
    public class Bloque
    {

        public Bloque()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }

        [BsonElement("idBloque")]
        public int idBloque { get; set; }

        [BsonElement("fechaMinado")]

        public DateTime fechaMinado { get; set; }

        [BsonElement("prueba")]
        public int prueba { get; set; }


        [BsonElement("milisegundos")]
        public int milisegundos { get; set; }

        [BsonElement("documentos")]
        public List<Document> documentos { get; set; }


        [BsonElement("hashPrevio")]
        public string hashPrevio { get; set; }



        [BsonElement("hash")]
        public string hash { get; set; }

        public string toStringM()
        {
           
            return idBloque  + "-"  + hashPrevio+" - ";
        }

    }
}
