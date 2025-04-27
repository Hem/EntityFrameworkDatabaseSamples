-- Use the DbDemo database
USE DbDemo;
GO


-- 1. Select all users and their group names (if any)
SELECT
    au.UserId,
    au.Username,
    au.Email,
    
    ag.GroupId,
    ag.GroupName

FROM
    AppUsers AS au
LEFT JOIN
    AppUsersInGroups AS agu ON au.UserId = agu.UserId
LEFT JOIN
    AppGroups AS ag ON agu.GroupId = ag.GroupId;