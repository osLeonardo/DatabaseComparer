using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;

namespace BancoDeDadosAPI.Controllers
{
    [Route("api/neo4j")]
    [ApiController]
    public class Neo4jController : ControllerBase
    {
        private readonly INeo4jService _neo4jService;

        public Neo4jController(INeo4jService neo4jService)
        {
            _neo4jService = neo4jService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DataModel>>> List()
        {
            var res = await _neo4jService.ListAsync();

            List<DataModel> resultList = res.ToListAsync().Result.Select(row => new DataModel()
            {
                Id = row.As<INode>().Properties["Id"].As<int>(),
                Text = row.As<INode>().Properties["Text"].As<string>(),
                Number = row.As<INode>().Properties["Number"].As<int>(),
                Decimal = row.As<INode>().Properties["Decimal"].As<float>(),
                Date = row.As<INode>().Properties["Date"].As<DateTime>()
            }).ToList();

            return Ok(resultList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DataModel>> GetById([FromRoute] string id)
        {
            var result = await _neo4jService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DataModel>> Create([FromBody] DataModel data)
        {
            await _neo4jService.CreateAsync(data);

            return Ok(data);
        }

        [HttpPut]
        public async Task<ActionResult<DataModel>> Update([FromBody] DataModel data)
        {
            await _neo4jService.UpdateAsync(data);

            return Ok(data);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DataModel>> Delete([FromRoute] int id)
        {
            await _neo4jService.DeleteAsync(id);

            return Ok();
        }
    }
}