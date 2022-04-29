using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private IAgentRepository _AgentRepository;

        public AgentController(IAgentRepository agentRepository)
        {
            _AgentRepository = agentRepository;
        }
    }
}
