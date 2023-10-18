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
        public ActionResult Get()
        {
            return Ok(_neo4jService.Get());
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok(_neo4jService.Post());
        }

        [HttpPut]
        public ActionResult Put()
        {
            return Ok(_neo4jService.Put());
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok(_neo4jService.Delete());
        }
    }
}