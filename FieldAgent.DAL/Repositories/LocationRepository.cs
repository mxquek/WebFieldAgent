using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public DbFactory DbFac { get; set; }
        public LocationRepository(DbFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Location> Get(int locationId)
        {
            Response<Location> result = new Response<Location> ();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Locations.Find(locationId);
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
                result.Message = $"Location #{locationId} not found";
            }
            return result;
        }
        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> result = new Response<List<Location>>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    result.Data = db.Locations
                                    .Where(l => l.AgencyId == agencyId).ToList();
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
                result.Message = $"Locations for Agency #{agencyId} not found";
            }

            return result;
        }

        public Response<Location> Insert(Location location)
        {
            Response<Location> result = new Response<Location>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Locations.Add(location);
                    result.Data = location;
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
        public Response Update(Location location)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Locations.Update(location);
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
        public Response Delete(int locationId)
        {
            Response result = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Locations.Remove(db.Locations.Find(locationId));
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
