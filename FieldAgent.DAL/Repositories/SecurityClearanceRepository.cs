using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        public DbFactory DbFac { get; set; }
        public SecurityClearanceRepository(DbFactory dbfac)
        {
            DbFac = dbfac;
        }
        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> result = new Response<SecurityClearance>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.SecurityClearances.Find(securityClearanceId);
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
                result.Message = $"Security Clearance #{securityClearanceId} not found";
            }
            return result;
        }

        public Response<List<SecurityClearance>> GetAll()
        {
            Response<List<SecurityClearance>> result = new Response<List<SecurityClearance>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.SecurityClearances.ToList();
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
                result.Message = $"There are no Security Clearances";
            }
            return result;
        }
    }
}
