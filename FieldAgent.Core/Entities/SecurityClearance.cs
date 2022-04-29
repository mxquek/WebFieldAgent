using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FieldAgent.Core.Entities
{
    [Table("SecurityClearance")]
    public class SecurityClearance
    {
        [Key]
        public int SecurityClearanceId { get; set; }
        public string SecurityClearanceName { get; set; }

        //One-to-Many AgencyAgents
        public List<AgencyAgent> AgencyAgents { get; set; }

        //Overrides
        public override bool Equals(object obj)
        {
            return obj is SecurityClearance sc &&
                SecurityClearanceId == sc.SecurityClearanceId &&
                SecurityClearanceName == sc.SecurityClearanceName;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(SecurityClearanceId, SecurityClearanceName);
        }
    }
}
