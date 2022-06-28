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

        public async Task addDocuments(List<Document> items) => await _documents.InsertManyAsync(items);
        public async Task<Document> GetDocumentById(string id)
        {
            try
            {
                var builder = Builders<Document>.Filter;
                var filter = builder.Eq(Document => Document.id, id);
                return await _documents.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }

       

        public  async Task<List<Document>> RemoveAllDocument(List<Document> list)
        {
            try
            {
                //await _documents.DeleteMany
                    return list;

            }
            catch(Exception e)
            {

                throw e;
            }
            //return false;
        }

        public async Task<bool> RemoveDocument(string id)
        {
            try
            {

                await _documents.DeleteOneAsync(x => x.id == id);
               
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public Task<bool> UpdateDocument(string id, Document document)
        {
            throw new NotImplementedException();
        }
    }
}
