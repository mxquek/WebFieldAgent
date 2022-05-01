using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Web.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("agent/{agentId}")]
        public IActionResult GetByAgent(int agentId)
        {
            var result = _AliasRepository.GetByAgent(agentId);
            if (result.Success)
            {
                if (result.Data.Count == 0)
                {
                    return NotFound($"No aliases found for Agent {agentId}");
                }

                return Ok
                (
                    result.Data.Select
                    (alias => new AliasModel()
                    {
                        AliasId = alias.AliasId,
                        AliasName = alias.AliasName,
                        InterpolId = alias.InterpolId,
                        Persona = alias.Persona,

                        AgentId = alias.AgentId
                    })
                );
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        [HttpPost]
        public IActionResult AddAlias(AliasModel aliasModel)
        {
            if (ModelState.IsValid)
            {
                Alias alias = new Alias()
                {
                    AliasName = aliasModel.AliasName,
                    InterpolId = aliasModel.InterpolId,
                    Persona = aliasModel.Persona,

                    AgentId = aliasModel.AgentId
                };

                var result = _AliasRepository.Insert(alias);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetAlias), new {id = result.Data.AliasId}, result.Data);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete, Authorize]
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

        [HttpPut, Authorize]
        public IActionResult EditAlias(AliasModel aliasModel)
        {
            if(ModelState.IsValid && aliasModel.AliasId > 0)
            {
                Alias alias = new Alias()
                {
                    AliasId = aliasModel.AliasId,
                    AliasName = aliasModel.AliasName,
                    InterpolId = aliasModel.InterpolId,
                    Persona = aliasModel.Persona,

                    AgentId = aliasModel.AgentId
                };

                if (!_AliasRepository.Get(alias.AliasId).Success)
                {
                    return NotFound($"Alias {alias.AliasId} not found");
                }

                var result = _AliasRepository.Update(alias);

                if (result.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                if(aliasModel.AliasId <= 0)
                {
                    ModelState.AddModelError("aliasId", "Invalid Alias ID");
                }
                return BadRequest(ModelState);
            }
        }

        
    }
}
