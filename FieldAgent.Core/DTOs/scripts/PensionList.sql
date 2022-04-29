CREATE PROCEDURE [PensionList]
(
    @AgencyId as int
)
AS
BEGIN
SELECT
    a.ShortName,
    aa.BadgeId,
    Agent.LastName + ', ' + Agent.FirstName AS 'NameLastFirst',
    Agent.DateOfBirth,
    aa.DeactivationDate
FROM AgencyAgent aa
INNER JOIN Agency a ON aa.AgencyId = a.AgencyId
INNER JOIN Agent ON aa.AgentId = Agent.AgentId
INNER Join SecurityClearance sc on aa.SecurityClearanceId = sc.SecurityClearanceId
WHERE sc.SecurityClearanceName = 'Retired'
AND a.AgencyId = @AgencyId;

END