using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FieldAgent.DAL.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IConfigurationRoot _Config;
        private readonly string _ConnectionString;
        private readonly FactoryMode _Mode;

        public ReportsRepository(IConfigurationRoot config)
        {
            _Config = config;
            string environment = _Mode == FactoryMode.TEST ? "Test" : "Prod";
            _ConnectionString = _Config[$"ConnectionStrings:{environment}"];
        }

        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            Response<List<ClearanceAuditListItem>> result = new Response<List<ClearanceAuditListItem>>();
            result.Data = new List<ClearanceAuditListItem>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    var command = new SqlCommand("AuditClearance", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SecurityClearanceId", securityClearanceId);
                    command.Parameters.AddWithValue("@AgencyId", agencyId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClearanceAuditListItem item = new ClearanceAuditListItem();
                            item.BadgeId = (Guid)reader["BadgeId"];
                            item.NameLastFirst = reader["NameLastFirst"].ToString();
                            item.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            item.ActivationDate = (DateTime)reader["ActivationDate"];
                            item.DeactivationDate = (DateTime)reader["DeactivationDate"];
                            result.Data.Add(item);
                        }
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            Response<List<PensionListItem>> result = new Response<List<PensionListItem>>();
            result.Data = new List<PensionListItem>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    var command = new SqlCommand("PensionList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AgencyId", agencyId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PensionListItem item = new PensionListItem();
                            item.AgencyName = reader["ShortName"].ToString();
                            item.BadgeId = (Guid)reader["BadgeId"];
                            item.NameLastFirst = reader["NameLastFirst"].ToString();
                            item.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            item.DeactivationDate = (DateTime)reader["DeactivationDate"];
                            result.Data.Add(item);
                        }
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            Response<List<TopAgentListItem>> result = new Response<List<TopAgentListItem>>();
            result.Data = new List<TopAgentListItem>();

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    var command = new SqlCommand("TopAgents", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TopAgentListItem item = new TopAgentListItem();
                            item.NameLastFirst = reader["NameLastFirst"].ToString();
                            item.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            item.CompletedMissionCount = (int)reader["NumberOfMissions"];

                            result.Data.Add(item);
                        }
                        result.Success = true;
                    }
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
