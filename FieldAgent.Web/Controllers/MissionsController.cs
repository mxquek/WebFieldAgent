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

        [HttpDelete, Authorize]
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

        [HttpPut, Authorize]
        public IActionResult EditMission(MissionModel missionModel)
        {
            if (ModelState.IsValid && missionModel.MissionId > 0)
            {
                Mission mission = new Mission()
                {
                    MissionId = missionModel.MissionId,
                    CodeName = missionModel.CodeName,
                    StartDate = missionModel.StartDate,
                    ProjectedEndDate = missionModel.ProjectedEndDate,
                    ActualEndDate = missionModel.ActualEndDate,
                    OperationalCost = missionModel.OperationalCost,
                    Notes = missionModel.Notes,

                    AgencyId = missionModel.AgencyId
                };

                if (!_MissionRepository.Get(mission.MissionId).Success)
                {
                    return NotFound($"Mission {mission.MissionId} not found");
                }

                var result = _MissionRepository.Update(mission);

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
                if (missionModel.MissionId <= 0)
                {
                    ModelState.AddModelError("missionId", "Invalid Mission ID");
                }
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("agency/{agencyId}")]
        public IActionResult GetByAgency(int agencyId)
        {
            var result = _MissionRepository.GetByAgency(agencyId);
            if (result.Success)
            {
                if(result.Data.Count == 0)
                {
                    return NotFound($"No missions found for Agency {agencyId}");
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
                    })
                );
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
            var result = _MissionRepository.GetByAgent(agentId);
            if (result.Success)
            {
                if (result.Data.Count == 0)
                {
                    return NotFound($"No missions found for Agent {agentId}");
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
                    })
                );
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }


}
