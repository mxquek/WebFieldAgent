using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL;
using FieldAgent.Web.Models;
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
        [Route("{id}")]
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

    }
}
