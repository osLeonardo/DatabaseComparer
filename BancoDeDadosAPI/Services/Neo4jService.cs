using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Services
{
    public class Neo4jService : INeo4jService
    {
        private readonly IDriver _driver;
        private readonly IAsyncSession _session;
        private readonly string _queryGet = "MATCH (n) RETURN n";
        private readonly string _queryGetById = "MATCH (n) WHERE n.Id = $nodeId RETURN n";
        private readonly string _queryCreate = @"CREATE ({ Id: $id, Text: $text, Number: $num, Decimal: $dec, Date: $date })";
        private readonly string _queryUpdate = @"MATCH (n) WHERE n.Id = $id SET n.Text = $text, n.Number = $num, n.Decimal = $dec, n.Date = $date";
        private readonly string _queryDelete = @"MATCH (n) WHERE n.Id = $id DELETE n";

        public Neo4jService()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neoadmin"));
            _session = _driver.AsyncSession();
        }

        public Task<List<IRecord>> ListAsync()
        {
            return _session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(_queryGet);
                return await cursor.ToListAsync();
            });
        }

        public Task<IRecord> GetByIdAsync(int nodeId)
        {
            return _session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(_queryGetById, new { nodeId });
                return await cursor.SingleAsync();
            });
        }

        public Task CreateAsync(DataModel data)
        {
            return _session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(_queryCreate, new
                {
                    id = data.Id,
                    text = data.Text,
                    num = data.Number,
                    dec = data.Decimal,
                    date = data.Date
                });
            });
        }

        public Task UpdateAsync(DataModel data)
        {
            return _session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(_queryUpdate, new
                {
                    id = data.Id,
                    text = data.Text,
                    num = data.Number,
                    dec = data.Decimal,
                    date = data.Date
                });
            });
        }

        public  Task DeleteAsync(int nodeId)
        {
            return _session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(_queryDelete, new { id = nodeId });
            });
        }
    }
}