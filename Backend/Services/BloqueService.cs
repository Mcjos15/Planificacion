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
        private readonly IMongoCollection<Bloque> _bloque;
        public BloqueService(IOptions<Mongo> mongo)
        {
            //context = new DBContextClass(mongo);

            var mongoClient = new MongoClient(mongo.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongo.Value.DatabaseName);

            // _users = mongoDatabase.GetCollection<Document>(mongo.Value.UsuariosCollectionName);
            _bloque = mongoDatabase.GetCollection<Bloque>("Bloques");
        }

      

        public async Task AddBloque(Bloque item) => await _bloque.InsertOneAsync(item);
        public async Task<List<Bloque>> GetAllBloques()
        {
            try
            {
                return await _bloque.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }


        public async Task<Bloque> getLAstBloque()
        {
            try
            {
                List<Bloque> listBloque = await _bloque.Find(_ => true).ToListAsync();

                if(listBloque.Count == 0)
                {
                    return null;
                }
                else
                {
                    return listBloque[listBloque.Count - 1];

                }

            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }

        
    }
}
