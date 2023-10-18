using BancoDeDadosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeDadosAPI.Controllers
{
    [Route("api/cassandra")]
    [ApiController]
    public class CassandraController : ControllerBase
    {
        private readonly CassandraService _cassandraService;

        public CassandraController(CassandraService cassandraService)
        {
            _cassandraService = cassandraService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_cassandraService.Get());
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok(_cassandraService.Post());
        }

        [HttpPut]
        public ActionResult Put()
        {
            return Ok(_cassandraService.Put());
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok(_cassandraService.Delete());
        }
    }
}