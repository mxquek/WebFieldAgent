using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repositories
{
    public class MissionRepository : IMissionRepository
    {
        public DbFactory DbFac { get; set; }
        public MissionRepository(DbFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> result = new Response<Mission>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Missions.Add(mission);
                    result.Data = mission;
                    result.Success = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Response Update(Mission mission)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Missions.Update(mission);
                    result.Success = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Response Delete(int missionId)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var missionAgents = db.MissionAgents
                                            .Where(ma => ma.MissionId == missionId);
                    foreach (var missionAgent in missionAgents)
                    {
                        db.MissionAgents.Remove(missionAgent);
                    }

                    db.Missions.Remove(db.Missions.Find(missionId));
                    result.Success = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Response<Mission> Get(int missionId)
        {
            Response<Mission> result = new Response<Mission>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Missions.Find(missionId);
                    result.Success = true;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            if (result.Data == null)
            {
                result.Success = false;
                result.Message = $"Mission #{missionId} not found";
            }
            return result;
        }
        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> result = new Response<List<Mission>>();
            result.Data = new List<Mission>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Missions
                                    .Where(m => m.AgencyId == agencyId).ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            if (result.Data == null)
            {
                result.Success = false;
                result.Message = $"Missions for Agency #{agencyId} not found";
            }

            return result;
        }
        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<Mission>> result = new Response<List<Mission>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.MissionAgents
                                        .Include(ma => ma.Mission)
                                        .Where(ma => ma.AgentId == agentId)
                                        .Select(ma => ma.Mission)
                                        .ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            if (result.Data == null)
            {
                result.Success = false;
                result.Message = $"No missions found for Agent #{agentId}";
            }

            return result;
        }
    }
}
