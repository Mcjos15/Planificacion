using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.models;
using System.Linq;

namespace Backend.Interfaces
{
    public interface IConfiguraciones
    {

        Task<List<Configuraciones>> GetAllConfigs();

        Task AddConfig(Configuraciones item);

        Task<Configuraciones> getConfig(string id);

        Task Updateconfig(string id, Configuraciones config);

        Task RemoveConfig(string id);



    }
}
