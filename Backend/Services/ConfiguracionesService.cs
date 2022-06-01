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

            MongoClient mongoClient = new MongoClient(mongo.Value.ConnectionString);

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

        public async Task<Configuraciones> getConfig(string id) {
            try
            {
                return await _configuraciones.Find(x => x.id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;

            }

        }

        public async Task Updateconfig(string id, Configuraciones config) =>
         await _configuraciones.ReplaceOneAsync(x => x.id == id, config);

        public async Task RemoveConfig(string id) =>
            await _configuraciones.DeleteOneAsync(x => x.id == id);

    }



}
