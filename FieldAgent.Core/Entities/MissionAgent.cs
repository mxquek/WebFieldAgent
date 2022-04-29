using System.ComponentModel.DataAnnotations.Schema;

namespace FieldAgent.Core.Entities
{
    [Table("MissionAgent")]
    public class MissionAgent
    {
        //Primary Keys
        public int MissionId { get; set; }
        public int AgentId  { get; set; }

        public Mission Mission { get; set; }
        public Agent Agent { get; set; }

        //Overrides
        public override bool Equals(object obj)
        {
            return obj is MissionAgent missionAgent &&
                missionAgent.MissionId == MissionId &&
                missionAgent.AgentId == AgentId;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(MissionId, AgentId);
        }
    }
}
