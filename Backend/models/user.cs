using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace Backend.models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        [BsonElement("nombre")]
        [JsonPropertyName("Name")]
        public string name { get; set; }
        [BsonElement("correo")]

        public string correo { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        public User()
        {
       
        }
    }
}
