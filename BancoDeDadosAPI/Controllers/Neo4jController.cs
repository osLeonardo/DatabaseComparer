using BancoDeDadosAPI.Models;
using BancoDeDadosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeDadosAPI.Controllers
{
    [Route("api/neo4j")]
    [ApiController]
    public class Neo4jController : ControllerBase
    {
        private readonly Neo4jService _neo4jService;

        public Neo4jController(Neo4jService neo4jService)
        {
            _neo4jService = neo4jService;
        }

        [HttpGet]
        public ActionResult List()
        {
            return Ok(_neo4jService.ListAsync());
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