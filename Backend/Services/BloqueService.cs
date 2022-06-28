using System;
using Backend.models;
using Backend.DataBase;
using MongoDB.Driver;
using Backend.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Backend.Services

{
    public class BloqueService : IBloque
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

      

        public async Task AddBloque(Bloque item) => await _documents.InsertOneAsync(item);
        public Task<List<Bloque>> GetAllBloques()
        {
            throw new NotImplementedException();
        }


        Task<Bloque> IBloque.getLAstBloque()
        {

            return (Task<Bloque>)_documents.Find(_ => true).Sort("_id").Limit(1); ;
        }
    }
}
