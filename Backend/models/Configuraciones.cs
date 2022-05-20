using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Backend.models
{
    public class Configuraciones
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }

        [BsonElement("llave")]
        [JsonPropertyName("Llave")]
        public string llave { get; set; }

        [BsonElement("numeroRegistro")]
        [JsonPropertyName("NumeroRegistro")]
        public string numeroRegistro { get; set; }
    }
}
