using FieldAgent.Core.Entities;

namespace FieldAgent.Core.Interfaces.DAL
{
    public interface IMissionAgentRepository
    {
        Response<MissionAgent> Insert(MissionAgent missionAgent);
        //Response Update(MissionAgent missionAgent);
        Response Delete(int missionId, int agentId);
        Response<MissionAgent> Get(int missionId, int agentId);
        Response<List<MissionAgent>> GetByMission(int missionId);
        Response<List<MissionAgent>> GetByAgent(int agentId);
    }
}
