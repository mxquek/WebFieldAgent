using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{

    public class AliasRepository :IAliasRepository
    {
        public DbFactory DbFac { get; set; }
        public AliasRepository(DbFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Alias> Insert(Alias alias)
        {
            Response<Alias> result = new Response<Alias>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Aliases.Add(alias);
                    result.Data = alias;
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
        public Response Update(Alias alias)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Aliases.Update(alias);
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
        public Response Delete(int aliasId)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {   
                    db.Aliases.Remove(db.Aliases.Find(aliasId));
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

        public Response<Alias> Get(int aliasId)
        {
            Response<Alias> result = new Response<Alias>();
            using(var db = DbFac.GetDbContext())
            {
                result.Data = db.Aliases.Find(aliasId);
                result.Success = true;
            }
            if (result.Data == null)
            {
                result.Success = false;
                result.Message = $"Alias #{aliasId} not found";
            }
            return result;
        }
        public Response<List<Alias>> GetByAgent(int agentId)
        {
            Response<List<Alias>> result = new Response<List<Alias>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Aliases
                                    .Where(a => a.AgentId == agentId).ToList();
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
                result.Message = $"Alias for Agent #{agentId} not found";
            }

            return result;
        }

    }
}
