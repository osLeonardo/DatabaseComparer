using BancoDeDadosAPI.Models;
using Cassandra;

namespace BancoDeDadosAPI.Interfaces
{
    public interface ICassandraService
    {
        public Task<RowSet> ListAsync();
        public Task<RowSet> GetByIdAsync(int id);
        public Task<RowSet> PostAsync(DataModel data);
        public Task<RowSet> UpdateAsync(DataModel data);
        public Task<RowSet> DeleteAsync(int id);

    }
}
