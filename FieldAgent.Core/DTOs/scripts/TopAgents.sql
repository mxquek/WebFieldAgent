CREATE PROCEDURE [TopAgents]
AS
BEGIN

SELECT TOP (3)
    a.LastName + ', ' + a.FirstName AS 'NameLastFirst',
    a.DateOfBirth,
    Count(m.MissionId) AS 'NumberOfMissions'
FROM Agent a
INNER JOIN MissionAgent ma ON a.AgentId = ma.AgentId
INNER Join Mission m ON ma.MissionId = m.MissionId
WHERE m.ActualEndDate IS NOT NULL
GROUP BY a.LastName + ', ' + a.FirstName, a.DateOfBirth
ORDER BY NumberOfMissions DESC;

END