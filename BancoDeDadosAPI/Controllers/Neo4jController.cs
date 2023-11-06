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
            var dataModels = await _neo4jService.ListAsync();
            var dataModelList = new List<DataModel>();

            foreach (var dataModel in dataModels)
            {
                dataModelList.Add(new DataModel
                {
                    Id = dataModel.Id,
                    Text = dataModel.Text,
                    Number = dataModel.Number,
                    Decimal = dataModel.Decimal,
                    Date = dataModel.Date
                });
            }

        return Ok(dataModelList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DataModel>> GetById([FromRoute] int id)
        {
            var dataModel = await _neo4jService.GetByIdAsync(id);

            if (dataModel == null)
            {
                return NotFound();
            }

            return Ok(dataModel);
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