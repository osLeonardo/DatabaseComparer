using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Interfaces
{
    public interface INeo4jService
    {
        public Task<List<IRecord>> ListAsync();
        public Task <IRecord> GetByIdAsync(int nodeId);
        public Task<IResultCursor> CreateAsync(DataModel data);
        public Task<IResultCursor> UpdateAsync(DataModel data);
        public Task<IResultCursor> DeleteAsync(int nodeId);
    }
}