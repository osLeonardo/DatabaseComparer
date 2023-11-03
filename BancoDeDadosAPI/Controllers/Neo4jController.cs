using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
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
        public ActionResult GetById([FromRoute] string id)
        {
            return Ok(_neo4jService.GetByIdAsync(id));
        }

        [HttpPost]
        public ActionResult Post(Neo4jModel data)
        {
            return Ok(_neo4jService.PostAsync(data));
        }

        [HttpPut]
        public ActionResult Put(Neo4jModel data)
        {
            return Ok(_neo4jService.UpdateAsync(data));
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(string id)
        {
            return Ok(_neo4jService.DeleteAsync(id));
        }
    }
}