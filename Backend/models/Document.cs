using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace Backend.models
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        [BsonElement("propietario")]
        [JsonPropertyName("propietario")]
        public string propietario { get; set; }

        [BsonElement("name")]

        public string name { get; set; }
        [BsonElement("type")]

        public string type { get; set; }


        [BsonElement("dateCreation")]

        public string dateCreation { get; set; }

        [BsonElement("size")]

        public string size { get; set; }
        [BsonElement("Base64")]
        public string Base64 { get; set; }
        public Document()
        {
        }
    }
}
