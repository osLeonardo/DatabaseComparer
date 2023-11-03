using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Interfaces
{
    public interface INeo4jService
    {
        public Task<IResultCursor> ListAsync();
        public Task<Neo4jModel> GetByIdAsync(string id);
        public Task PostAsync(Neo4jModel data);
        public Task UpdateAsync(Neo4jModel data);
        public Task DeleteAsync(string id);
    }
}