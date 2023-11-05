using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Interfaces
{
    public interface INeo4jService
    {
        public Task<IResultCursor> ListAsync();
        public Task<DataModel> GetByIdAsync(string nodeId);
        public Task CreateAsync(DataModel data);
        public Task UpdateAsync(DataModel data);
        public Task DeleteAsync(int id);
    }
}