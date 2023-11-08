using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Services
{
    public class Neo4jService : INeo4jService
    {
        private readonly IDriver _driver;
        private readonly string _queryGet = "MATCH (n) RETURN n";
        private readonly string _queryGetById = "MATCH (n) WHERE n.Id = $nodeId RETURN n";
        private readonly string _queryCreate = @"CREATE ({ Id: $id, Text: $text, Number: $num, Decimal: $dec, Date: $date })";
        private readonly string _queryUpdate = @"MATCH (n) WHERE n.Id = $id SET n.Text = $text, n.Number = $num, n.Decimal = $dec, n.Date = $date";
        private readonly string _queryDelete = @"MATCH (n) WHERE n.Id = $id DELETE n";

        public Neo4jService()
        {
            _driver = GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("neo4j", "neoadmin"), o => o.WithConnectionTimeout(TimeSpan.FromSeconds(15)).WithMaxConnectionPoolSize(1200));
        }

        public async Task<List<IRecord>> ListAsync()
        {
            await using IAsyncSession _session = _driver.AsyncSession();
            return await _session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(_queryGet);
                return await cursor.ToListAsync();
            });
        }

        public Task<IRecord> GetByIdAsync(int nodeId)
        {
            using IAsyncSession _session = _driver.AsyncSession();
            return _session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(_queryGetById, new { nodeId });
                return await cursor.SingleAsync();
            });
        }

        public async Task<IResultCursor> CreateAsync(DataModel data)
        {
            await using IAsyncSession _session = _driver.AsyncSession();
            return await _session.ExecuteWriteAsync(async tx =>
            {
                return await tx.RunAsync(_queryCreate, new
                {
                    id = data.Id,
                    text = data.Text,
                    num = data.Number,
                    dec = data.Decimal,
                    date = data.Date
                });
            });
        }

        public async Task<IResultCursor> UpdateAsync(DataModel data)
        {
            await using IAsyncSession _session = _driver.AsyncSession();
            return await _session.ExecuteWriteAsync(async tx =>
            {
                return await tx.RunAsync(_queryUpdate, new
                {
                    id = data.Id,
                    text = data.Text,
                    num = data.Number,
                    dec = data.Decimal,
                    date = data.Date
                });
            });
        }

        public async Task<IResultCursor> DeleteAsync(int nodeId)
        {
            await using IAsyncSession _session = _driver.AsyncSession();
            return await _session.ExecuteWriteAsync(async tx =>
            {
                return await tx.RunAsync(_queryDelete, new { id = nodeId });
            });
        }
    }
}