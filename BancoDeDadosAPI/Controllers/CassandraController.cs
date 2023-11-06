using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Models;
using Cassandra;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BancoDeDadosAPI.Controllers
{
    [Route("api/cassandra")]
    [ApiController]
    public class CassandraController : ControllerBase
    {
        private readonly ICassandraService _cassandraService;

        public CassandraController(ICassandraService cassandraService)
        {
            _cassandraService = cassandraService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DataModel>>> List()
        {
            var rows = await _cassandraService.ListAsync();

            List<DataModel> resultList = rows.Select(row => new DataModel()
            {
                Id = row.GetValue<int>("id"),
                Text = row.GetValue<string>("texto"),
                Number = row.GetValue<int>("numero"),
                Decimal = row.GetValue<float>("num_decimal"),
                Date = row.GetValue<DateTime>("data_completa")
            }).ToList();

            return Ok(resultList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DataModel>> GetById([FromRoute] int id)
        {
            var rows = await _cassandraService.GetByIdAsync(id);

            var row = rows.SingleOrDefault();

            DataModel result = new DataModel()
            {
                Id = row.GetValue<int>("id"),
                Text = row.GetValue<string>("texto"),
                Number = row.GetValue<int>("numero"),
                Decimal = row.GetValue<float>("num_decimal"),
                Date = row.GetValue<DateTime>("data_completa")
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DataModel>> Create([FromBody] DataModel data)
        {
            await _cassandraService.PostAsync(data);

            return Ok(data);
        }

        [HttpPut]
        public async Task<ActionResult<DataModel>> Update([FromBody] DataModel data)
        {
            await _cassandraService.UpdateAsync(data);

            return Ok(data);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _cassandraService.DeleteAsync(id);

            return Ok();
        }
    }
}