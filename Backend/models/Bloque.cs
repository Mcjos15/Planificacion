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

        [BsonElement("fechaMinado")]
        [JsonPropertyName("fechaMinado")]
        public DateTime fechaMinado { get; set; }

        [BsonElement("prueba")]
        [JsonPropertyName("prueba")]
        public int prueba { get; set; }


        [BsonElement("milisegundos")]
        [JsonPropertyName("milisegundos")]
        public int milisegundos { get; set; }

        [BsonElement("documentos")]
        [JsonPropertyName("documentos")]
        public string documentos { get; set; }


        [BsonElement("hashPrevio")]
        [JsonPropertyName("hashPrevio")]
        public string hashPrevio { get; set; }



        [BsonElement("hash")]
        [JsonPropertyName("hash")]
        public string hash { get; set; }

    }
}
