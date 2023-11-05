using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Services
{
    public class Neo4jService : INeo4jService
    {
        private readonly IDriver _driver;

        public Neo4jService()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neoadmin"));
        }

        public Task ListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task GetByIdAsync(int nodeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateAsync(DataModel data)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = @"CREATE ({ Id: $id, Text: $text, Number: $num, Decimal: $dec, Date: datetime($date) })";
                var parameters = new
                {
                    id = data.Id,
                    text = data.Text,
                    num = data.Number,
                    dec = data.Decimal,
                    date = data.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                };

                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(query, parameters);
                });
            }
        }

        public async Task UpdateAsync(DataModel data)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = @"MATCH (n)
                              WHERE n.Id = $id
                                SET n.Text = $text, n.Number = $num, n.Decimal = $dec, n.Date = datetime($date)";
                var parameters = new
                {
                    id = data.Id,
                    text = data.Text,
                    num = data.Number,
                    dec = data.Decimal,
                    date = data.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                };

                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(query, parameters);
                });
            }
        }

        public async Task DeleteAsync(int nodeId)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = @"MATCH (n)
                              WHERE n.Id = $id
                             DELETE n";

                var parameters = new { id = nodeId };

                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(query, parameters);
                });
            }
        }
    }
}