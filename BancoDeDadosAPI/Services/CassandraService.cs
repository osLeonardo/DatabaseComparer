using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Cassandra;

namespace BancoDeDadosAPI.Services
{
    public class CassandraService : ICassandraService
    {
        //CQL de criação do banco
        //CREATE TABLE [nome] (id int PRIMARY KEY, data_completa timestamp, num_decimal float, numero int, texto text);
        private readonly Cassandra.ISession _session;
        private readonly string keySpace = "test_api";
        private readonly string table = "teste";

        private SimpleStatement _psGet;
        private PreparedStatement _psGetById;
        private PreparedStatement _psCreate;
        private PreparedStatement _psUpdate;
        private PreparedStatement _psDelete;

        public CassandraService() {
            var cluster = Cluster.Builder()
                .AddContactPoint("127.0.0.1")
                .Build();
            _session = cluster.Connect(keySpace);
            _psGet = new SimpleStatement($"SELECT * FROM {table};");
            _psGetById = _session.Prepare($"SELECT * FROM {table} WHERE id = ?;");
            _psCreate = _session.Prepare($"INSERT INTO {table} (id, texto, numero, num_decimal, data_completa) VALUES (?, ?, ?, ?, ?);");
            _psUpdate = _session.Prepare($"UPDATE {table} SET texto = ?, numero = ?, num_decimal = ?, data_completa = ? WHERE id = ? ;");
            _psDelete = _session.Prepare($"DELETE FROM {table} WHERE id = ?;");
        }

        public Task<RowSet> ListAsync()
        {
            return _session.ExecuteAsync(_psGet);
        }

        public Task<RowSet> GetByIdAsync(int id)
        {
            return _session.ExecuteAsync(_psGetById.Bind(id));
        }

        public Task<RowSet> PostAsync(DataModel data)
        {
            return _session.ExecuteAsync(_psCreate.Bind(data.Id, data.Text, data.Number, data.Decimal, data.Date));
        }

        public Task<RowSet> UpdateAsync(DataModel data)
        {
            return _session.ExecuteAsync(_psUpdate.Bind(data.Text, data.Number, data.Decimal, data.Date, data.Id));
        }

        public async Task<RowSet> DeleteAsync(int id)
        {
            return await _session.ExecuteAsync( _psDelete.Bind(id));
        }
    }
}