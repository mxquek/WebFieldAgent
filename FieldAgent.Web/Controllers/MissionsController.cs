using FieldAgent.Core.Entities;
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

        [HttpPost]
        public IActionResult AddMission(MissionModel missionModel)
        {
            if (ModelState.IsValid)
            {
                Mission mission = new Mission()
                {
                    CodeName = missionModel.CodeName,
                    StartDate = missionModel.StartDate,
                    ProjectedEndDate = missionModel.ProjectedEndDate,
                    ActualEndDate = missionModel.ActualEndDate,
                    OperationalCost = missionModel.OperationalCost,
                    Notes = missionModel.Notes,
                    AgencyId = missionModel.AgencyId
                };

                var result = _MissionRepository.Insert(mission);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetMission), new { id = result.Data.MissionId}, result.Data);
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

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveMission(int id)
        {
            if (!_MissionRepository.Get(id).Success)
            {
                return NotFound($"Mission {id} not found");
            }

            var result = _MissionRepository.Delete(id);

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
