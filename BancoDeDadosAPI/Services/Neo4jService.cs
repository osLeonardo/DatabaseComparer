using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Neo4j.Driver;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace BancoDeDadosAPI.Services
{
    public class Neo4jService : INeo4jService
    {
        private readonly IDriver _driver;

        public Neo4jService(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public async Task<IResultCursor> ListAsync()
        {
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                // Define your Cypher query to list nodes and relationships.
                string cypherQuery = "MATCH (n)-[r]-(m) RETURN n, r, m";

                // Run the query and return the result cursor.
                return await session.RunAsync(cypherQuery);
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<Neo4jModel> GetByIdAsync(string id)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = "MATCH (n:Neo4jModel { Id: $id }) RETURN n";
                var parameters = new { id };

                var result = await session.ReadTransactionAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, parameters);
                    var record = await cursor.SingleAsync();

                    return record?["n"].As<INode>();
                });

                if (result != null)
                {
                    return new Neo4jModel
                    {
                        Id = result["Id"].As<int>(),
                        Text = result["Text"].As<string>(),
                        Number = result["Number"].As<int>(),
                        Decimal = result["Decimal"].As<float>(),
                        Date = result["Date"].As<DateTime>()
                    };
                }

                return null;
            }
        }

        public async Task PostAsync(Neo4jModel data)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = @"
                CREATE (n:Neo4jModel { Id: $id, Text: $text, Number: $num, Decimal: $dec, Date: $date })";
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

        public async Task UpdateAsync(Neo4jModel data)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = @"
                MATCH (n:Neo4jModel { Id: $id })
                SET n.Text = $text, n.Number = $num, n.Decimal = $dec, n.Date = $date";
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

        public async Task DeleteAsync(string id)
        {
            using (var session = _driver.AsyncSession())
            {
                var query = "MATCH (n:Neo4jModel { Id: $id }) DELETE n";
                var parameters = new { id };

                await session.WriteTransactionAsync(async tx =>
                {
                    await tx.RunAsync(query, parameters);
                });
            }
        }
    }
}