using Backend.DataBase;
using Backend.Interfaces;
using Backend.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Backend.Services
{
    public class DocumentService:IDocument
    {
        private readonly IMongoCollection<Document> _documents;
        public DocumentService(IOptions<Mongo> mongo)
        {
            //context = new DBContextClass(mongo);

            var mongoClient = new MongoClient(mongo.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongo.Value.DatabaseName);

            // _users = mongoDatabase.GetCollection<Document>(mongo.Value.UsuariosCollectionName);
            _documents = mongoDatabase.GetCollection<Document>("Documents");
        }

        public async Task AddDocument(Document item)=>
            await _documents.InsertOneAsync(item);
        

    
        public async Task<List<Document>> GetAllDocuments()
        {
            try
            {
                return await _documents.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }
        public Task<Document> GetDocumentById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllDocument()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveDocument(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocument(string id, Document document)
        {
            throw new NotImplementedException();
        }
    }
}
