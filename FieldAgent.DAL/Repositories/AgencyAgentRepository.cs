using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        public DbFactory DbFac { get; set; }
        public AgencyAgentRepository(DbFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<AgencyAgent> Get(int agencyId, int agentId)
        {
            Response<AgencyAgent> result = new Response<AgencyAgent>();
            try
            {
                using(var db = DbFac.GetDbContext())
                {
                    result.Data = db.AgencyAgents.Find(agencyId, agentId);
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
                result.Message = $"AgencyAgent with Agency #{agencyId} and Agent #{agentId} not found";
            }
            return result;
        }
        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> result = new Response<List<AgencyAgent>>();
            try
            {
                using(var db = DbFac.GetDbContext())
                {
                    result.Data = db.AgencyAgents
                                    .Where(aa => aa.AgencyId == agencyId).ToList();
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
                result.Message = $"AgencyAgents for Agency #{agencyId} not found";
            }
            return result;
        }
        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> result = new Response<List<AgencyAgent>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.AgencyAgents
                                    .Where(aa => aa.AgentId == agentId).ToList();
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
                result.Message = $"AgencyAgents for Agent #{agentId} not found";
            }
            return result;
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> result = new Response<AgencyAgent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgents.Add(agencyAgent);
                    result.Data = agencyAgent;
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
        public Response Update(AgencyAgent agencyAgent)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgents.Update(agencyAgent);
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
        public Response Delete(int agencyId, int agentId)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgents.Remove(db.AgencyAgents.Find(agencyId, agentId));
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
