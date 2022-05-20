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
    public class ConfiguracionesService : IConfiguraciones
    {
        //private readonly DBContextClass context = null;
        private readonly IMongoCollection<Configuraciones> _configuraciones;

        public ConfiguracionesService(IOptions<Mongo> mongo)
        {
            //context = new DBContextClass(mongo);

            var mongoClient = new MongoClient(mongo.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongo.Value.DatabaseName);

            // _users = mongoDatabase.GetCollection<User>(mongo.Value.UsuariosCollectionName);
            _configuraciones = mongoDatabase.GetCollection<Configuraciones>("configuraciones");

        }

 
        public async Task AddConfig(Configuraciones item) =>
       await _configuraciones.InsertOneAsync(item);

        public async Task<List<Configuraciones>> GetAllConfigs()
        {
            try
            {
                return await _configuraciones.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }
        }

    }



}
