using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        public DbFactory DbFac { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public MissionRepository MissionRepository { get; set; }

        public AgencyRepository(DbFactory dbfac, LocationRepository locationRepo, MissionRepository missionRepo)
        {
            DbFac = dbfac;
            LocationRepository = locationRepo;
            MissionRepository = missionRepo;
        }

        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> result = new Response<Agency>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Agencies.Find(agencyId);
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
                result.Message = $"Agency #{agencyId} not found";
            }
            return result;
        }
        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> result = new Response<List<Agency>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Agencies.ToList();
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
                result.Message = $"Could not retireve all agencies";
            }
            return result;
        }

        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> result = new Response<Agency>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agencies.Add(agency);
                    db.SaveChanges();

                    result.Data = agency;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Response Update(Agency agency)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agencies.Update(agency);
                    db.SaveChanges();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Response Delete(int agencyId)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var locations = LocationRepository.GetByAgency(agencyId);
                    foreach (Location location in locations.Data)
                    {
                        LocationRepository.Delete(location.LocationId);
                    }

                    var agencyAgents = db.AgencyAgents
                                        .Where(aa => aa.AgencyId == agencyId);
                    foreach (var agencyAgent in agencyAgents)
                    {
                        db.AgencyAgents.Remove(agencyAgent);
                    }

                    var missions = MissionRepository.GetByAgency(agencyId);
                    foreach (Mission mission in missions.Data)
                    {
                        MissionRepository.Delete(mission.MissionId);
                    }

                    db.Agencies.Remove(db.Agencies.Find(agencyId));
                    db.SaveChanges();

                    result.Success = true;
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
