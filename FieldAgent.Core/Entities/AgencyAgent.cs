using System.ComponentModel.DataAnnotations.Schema;

namespace FieldAgent.Core.Entities
{
    [Table("AgencyAgent")]
    public class AgencyAgent
    {
        //Primary Keys
        public int AgencyId { get; set; }
        public int AgentId  { get; set; }

        //Class Members
        public Guid BadgeId { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public bool IsActive { get; set; } = true;

        //Many-to-one
        public Agency Agency { get; set; }
        public Agent Agent { get; set; }

        public int SecurityClearanceId { get; set; }
        public SecurityClearance SecurityClearance { get; set; }

        //Overrides
        public override bool Equals(object obj)
        {
            return obj is AgencyAgent agencyAgent &&
                agencyAgent.AgencyId == AgencyId &&
                agencyAgent.AgentId == AgentId &&
                agencyAgent.BadgeId == BadgeId &&
                agencyAgent.ActivationDate == ActivationDate &&
                agencyAgent.DeactivationDate == DeactivationDate &&
                agencyAgent.IsActive == IsActive &&

                agencyAgent.SecurityClearanceId == SecurityClearanceId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AgencyId, AgentId, BadgeId, ActivationDate, DeactivationDate,IsActive, SecurityClearanceId);
        }
    }
}
