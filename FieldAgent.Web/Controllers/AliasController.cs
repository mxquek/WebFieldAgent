using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliasController : ControllerBase
    {
        private readonly IAliasRepository _AliasRepository;
        public AliasController(IAliasRepository aliasRepository)
        {
            _AliasRepository = aliasRepository;
        }

        [HttpGet]
        [Route("{id}", Name = "GetAlias")]
        public IActionResult GetAlias(int id)
        {
            var result = _AliasRepository.Get(id);

            if (result.Success)
            {
                return Ok(new AliasModel()
                {
                    AliasId = result.Data.AliasId,
                    AliasName = result.Data.AliasName,
                    InterpolId = result.Data.InterpolId,
                    Persona = result.Data.Persona,

                    AgentId = result.Data.AgentId
                });
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveAlias(int id)
        {
            if (!_AliasRepository.Get(id).Success)
            {
                return NotFound($"Alias {id} not found");
            }

            var result = _AliasRepository.Delete(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }

        }
    }
}
