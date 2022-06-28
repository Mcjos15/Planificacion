using System;
using Backend.models;
using Backend.DataBase;
using MongoDB.Driver;
using Backend.Interfaces;
using Microsoft.Extensions.Options;

namespace Backend.Services

{
    public class BloqueService:IBloque
    {
        private readonly IMongoCollection<Bloque> _documents;
        public BloqueService(IOptions<Mongo> mongo)
        {
            //context = new DBContextClass(mongo);

            var mongoClient = new MongoClient(mongo.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongo.Value.DatabaseName);

            // _users = mongoDatabase.GetCollection<Document>(mongo.Value.UsuariosCollectionName);
            _documents = mongoDatabase.GetCollection<Bloque>("Bloques");
        }
        
    }
}
