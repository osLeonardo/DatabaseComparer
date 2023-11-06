using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Interfaces
{
    public interface INeo4jService
    {
        public Task<List<DataModel>> ListAsync();
        public Task <DataModel> GetByIdAsync(int nodeId);
        public Task CreateAsync(DataModel data);
        public Task UpdateAsync(DataModel data);
        public Task DeleteAsync(int nodeId);
    }
}