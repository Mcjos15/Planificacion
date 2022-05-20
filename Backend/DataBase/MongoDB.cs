using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataBase
{
    public class Mongo
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsuariosCollectionName { get; set; } = null!;

        public string ConfiguracionesCollectionName { get; set; } = null!;
    }
}
