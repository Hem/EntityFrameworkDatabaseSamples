-- Create the DbDemo database
CREATE DATABASE DbDemo;
GO

-- Use the DbDemo database
USE DbDemo;
GO

-- Create the AppUsers table
CREATE TABLE AppUsers (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastLogin DATETIME2
);
GO

-- Create the AppGroups table
CREATE TABLE AppGroups (
    GroupId INT PRIMARY KEY IDENTITY(1,1),
    GroupName NVARCHAR(255) NOT NULL UNIQUE,
    [Description] NVARCHAR(MAX),
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    AdminId INT NULL,
    CONSTRAINT FK_AppGroups_Users FOREIGN KEY (AdminId) REFERENCES AppUsers(UserId),
);
GO

-- Create the AppUsersInGroups table
CREATE TABLE AppUsersInGroups (
    -- AppUsersInGroupsId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    GroupId INT NOT NULL,
    CONSTRAINT PK_AppUsersInGroups PRIMARY KEY (UserId, GroupId),
    CONSTRAINT FK_AppUsersInGroups_Users FOREIGN KEY (UserId) REFERENCES AppUsers(UserId),
    CONSTRAINT FK_AppUsersInGroups_Groups FOREIGN KEY (GroupId) REFERENCES AppGroups(GroupId),
    JoinedDate DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Create an index on the Email column of AppUsers for faster lookups.
CREATE NONCLUSTERED INDEX IX_AppUsers_Email
ON AppUsers (Email);
GO

-- Create an index on the GroupName column of AppGroups for faster lookups.
CREATE NONCLUSTERED INDEX IX_AppGroups_GroupName
ON AppGroups (GroupName);
GO

-- Optionally, you might want an index on UserId and GroupId in AppUsersInGroups
-- for performance reasons, depending on your query patterns.  The primary key
-- already serves as an index, but this additional index *might* help.  Test
-- your specific queries to see if it makes a difference.
CREATE NONCLUSTERED INDEX IX_AppUsersInGroups_UserId
ON AppUsersInGroups (UserId);
GO

CREATE NONCLUSTERED INDEX IX_AppUsersInGroups_GroupId
ON AppUsersInGroups (GroupId);
GO

-- Use the DbDemo database
USE DbDemo;
GO

-- Insert dummy data into AppUsers
INSERT INTO AppUsers (Username, Email, FirstName, LastName)
VALUES
    ('john.doe', 'john.doe@example.com', 'John', 'Doe'),
    ('jane.smith', 'jane.smith@example.com','Jane', 'Smith'),
    ('bob.johnson', 'bob.johnson@example.com', 'Bob', 'Johnson');
GO

-- Insert dummy data into AppGroups
-- Insert 3 groups and assign admins.  Note that the AdminId values refer to the UserId values from the AppUsers table.
INSERT INTO AppGroups (GroupName, Description, AdminId)
VALUES
    ('Administrators', 'Users with full administrative privileges', 1), -- John is the admin
    ('Editors', 'Content editors with publishing rights', 2),       -- Jane is the admin
    ('Readers', 'Read-only users', 3);                           -- Bob is the admin
GO

-- Insert dummy data into AppUsersInGroups
INSERT INTO AppUsersInGroups (UserId, GroupId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (1, 2);
GO
