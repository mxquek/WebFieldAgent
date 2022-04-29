using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class MissionAgentRepository : IMissionAgentRepository
    {
        public DbFactory DbFac { get; set; }
        public MissionAgentRepository(DbFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<MissionAgent> Get(int missionId, int agentId)
        {
            Response<MissionAgent> result = new Response<MissionAgent>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.MissionAgents.Find(missionId, agentId);
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
                result.Message = $"MissionAgent with Mission #{missionId} and Agent #{agentId} not found";
            }
            return result;
        }
        public Response<List<MissionAgent>> GetByAgent(int agentId)
        {
            Response<List<MissionAgent>> result = new Response<List<MissionAgent>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.MissionAgents
                                    .Where(ma => ma.AgentId == agentId).ToList();
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
                result.Message = $"MissionAgents for Agent #{agentId} not found";
            }
            return result;
        }
        public Response<List<MissionAgent>> GetByMission(int missionId)
        {
            Response<List<MissionAgent>> result = new Response<List<MissionAgent>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.MissionAgents
                                    .Where(ma => ma.MissionId == missionId).ToList();
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
                result.Message = $"MissionAgents for Mission #{missionId} not found";
            }
            return result;
        }

        public Response<MissionAgent> Insert(MissionAgent missionAgent)
        {
            Response<MissionAgent> result = new Response<MissionAgent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.MissionAgents.Add(missionAgent);
                    result.Data = missionAgent;
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
        /*public Response Update(MissionAgent missionAgent)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.MissionAgents.Update(missionAgent);
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
        */
        public Response Delete(int missionId, int agentId)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.MissionAgents.Remove(db.MissionAgents.Find(missionId, agentId));
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
    }
}
