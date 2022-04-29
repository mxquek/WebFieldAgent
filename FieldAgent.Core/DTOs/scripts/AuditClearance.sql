CREATE PROCEDURE [AuditClearance]
(
    @SecurityClearanceId INT,
    @AgencyId INT
)
AS
BEGIN

SELECT
    aa.BadgeId,
    Agent.LastName + ', ' + Agent.FirstName AS 'NameLastFirst',
    Agent.DateOfBirth,
    aa.ActivationDate,
    aa.DeactivationDate
FROM AgencyAgent aa
INNER JOIN Agency a ON aa.AgencyId = a.AgencyId
INNER JOIN Agent ON aa.AgentId = Agent.AgentId
WHERE aa.SecurityClearanceId = @SecurityClearanceId
AND a.AgencyId = @AgencyId;

END
GO