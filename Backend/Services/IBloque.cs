using System;
using Backend.models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Backend.Interfaces
{
    public interface IBloque
    {
        Task<List<Bloque>> GetAllBloques();

        Task AddBloque(Bloque item);
        Task<Bloque> getLAstBloque();
    }
}
