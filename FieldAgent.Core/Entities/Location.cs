using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FieldAgent.Core.Entities
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Street1 { get; set; }
        public string? Street2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }

        //Many-to-one Agency
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }

        //Overrides
        public override bool Equals(object obj)
        {
            return obj is Location location &&
                LocationId == location.LocationId &&
                LocationName == location.LocationName &&
                Street1 == location.Street1 &&
                Street2 == location.Street2 &&
                City == location.City &&
                PostalCode == location.PostalCode &&
                CountryCode == location.CountryCode &&
                AgencyId == location.AgencyId;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(LocationId, LocationName, Street1, Street2, City, PostalCode, CountryCode, AgencyId);
        }
    }
}
