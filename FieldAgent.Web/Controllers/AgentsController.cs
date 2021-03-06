using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL;
using FieldAgent.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private IAgentRepository _AgentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            _AgentRepository = agentRepository;
        }

        [HttpGet]
        [Route("{id}", Name = "GetAgent")]
        public IActionResult GetAgent(int id)
        {
            var result = _AgentRepository.Get(id);

            if (result.Success)
            {
                return Ok(new AgentModel()
                {
                    AgentId = result.Data.AgentId,
                    FirstName = result.Data.FirstName,
                    LastName = result.Data.LastName,
                    DateOfBirth = result.Data.DateOfBirth,
                    Height = result.Data.Height
                });
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet]
        [Route("{id}/missions")]
        public IActionResult GetMissions(int id)
        {
            if (!_AgentRepository.Get(id).Success)
            {
                return NotFound($"Agent {id} not found");
            }

            var result = _AgentRepository.GetMissions(id);

            if (result.Success)
            {
                if (result.Data.Count == 0)
                {
                    return NotFound($"No missions found for Agent {id}");
                }

                return Ok
                (
                    result.Data.Select
                    (mission => new MissionModel()
                        {
                            MissionId = mission.MissionId,
                            CodeName = mission.CodeName,
                            StartDate = mission.StartDate,
                            ProjectedEndDate = mission.ProjectedEndDate,
                            ActualEndDate = mission.ActualEndDate,
                            OperationalCost = mission.OperationalCost,
                            Notes = mission.Notes,

                            AgencyId = mission.AgencyId
                        }
                    )
                );
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        public IActionResult AddAgent(AgentModel agentModel)
        {
            if (ModelState.IsValid)
            {
                Agent agent = new Agent
                {
                    FirstName = agentModel.FirstName,
                    LastName = agentModel.LastName,
                    DateOfBirth = agentModel.DateOfBirth,
                    Height = agentModel.Height
                };

                var result = _AgentRepository.Insert(agent);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetAgent), new { id = result.Data.AgentId }, result.Data);
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
        public IActionResult RemoveAgent(int id)
        {
            if (!_AgentRepository.Get(id).Success)
            {
                return NotFound($"Agent {id} could not be found");
            }

            var result = _AgentRepository.Delete(id);

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
        public IActionResult EditAgent(AgentModel agentModel)
        {
            if(ModelState.IsValid && agentModel.AgentId > 0)
            {
                Agent agent = new Agent
                {
                    AgentId = agentModel.AgentId,
                    FirstName = agentModel.FirstName,
                    LastName = agentModel.LastName,
                    DateOfBirth = agentModel.DateOfBirth,
                    Height = agentModel.Height
                };

                var findResult = _AgentRepository.Get(agent.AgentId);
                if (!findResult.Success)
                {
                    return NotFound($"Agent {agent.AgentId} not found");
                }

                var result = _AgentRepository.Update(agent);

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
                if(agentModel.AgentId <= 0)
                {
                    ModelState.AddModelError("agentId", "Invalid Agent ID");
                }
                return BadRequest(ModelState);
            }
        }
    }
}
