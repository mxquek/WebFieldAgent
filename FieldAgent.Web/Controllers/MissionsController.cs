using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private IMissionRepository _MissionRepository;

        public MissionsController(IMissionRepository missionRepository)
        {
            _MissionRepository = missionRepository;
        }

        [HttpGet]
        [Route("{id}", Name = "GetMission")]
        public IActionResult GetMission(int id)
        {
            var result = _MissionRepository.Get(id);

            if (result.Success)
            {
                return Ok(new MissionModel()
                {
                    MissionId = result.Data.MissionId,
                    CodeName = result.Data.CodeName,
                    StartDate = result.Data.StartDate,
                    ProjectedEndDate = result.Data.ProjectedEndDate,
                    ActualEndDate = result.Data.ActualEndDate,
                    OperationalCost = result.Data.OperationalCost,
                    Notes = result.Data.Notes,

                    AgencyId = result.Data.AgencyId
                });
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
