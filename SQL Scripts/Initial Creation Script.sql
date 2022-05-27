GO
CREATE DATABASE Fithub
GO

GO
USE Fithub
GO
--=======================
-- Table
--=======================
GO
CREATE TABLE [dbo].[User]
(
[UserID] int PRIMARY KEY IDENTITY(1,1),
[Email] varchar(100),
[Password] varchar(50),
[FirstName] varchar(30) not null,
[LastName] varchar(30) not null,
[DateOfBirth] datetime,
[ExternalLoginProvider] varchar(30),
[ExternalLoginProviderName] varchar(30),
[ExternalProviderKey] varchar(50),
[IsExternalProvider] bit,
[IsActive] bit
[RoleID] int FOREIGN KEY REFERENCES [dbo].[UserRoles]([RoleID])
)

GO

CREATE TABLE [dbo].[UserRoles]
(
[RoleID] int PRIMARY KEY IDENTITY(1,1),
[Name] varchar(50),
[NormalisedName] varchar(50)
)
go
--fill the data in the UserROles table
INSERT INTO DBO.UserRoles (Name,NormalisedName) VALUES ('Viewer','VIEWER');
INSERT INTO DBO.UserRoles (Name,NormalisedName) VALUES ('User','USER');
INSERT INTO DBO.UserRoles (Name,NormalisedName) VALUES ('Administrator','ADMINISTRATOR');

go
CREATE TABLE [dbo].[Class]
(
ClassID int PRIMARY KEY IDENTITY(1,1),
ClassName VARCHAR(50) NOT NULL,
ClassDescription VARCHAR(500),
ClassShortDescription VARCHAR(100),
PhotoURL VARCHAR(50)
)

GO

CREATE TABLE [dbo].[Coach]
(
CoachID int PRIMARY KEY IDENTITY(1,1),
CoachName varchar(60),
Degree varchar(50),
PhotoURL VARCHAR(50)
)

go

CREATE TABLE [dbo].[ClassCoach]
(
ClassCoachID int PRIMARY KEY IDENTITY(1,1),
ClassID int FOREIGN KEY REFERENCES [dbo].[Class] (ClassID),
CoachID int FOREIGN KEY REFERENCES [dbo].[Coach] (CoachID)
)

GO

CREATE TABLE [dbo].[Rating]
(
RatingID INT PRIMARY KEY IDENTITY(1,1),
RatingValue INT,
CoachID INT FOREIGN KEY REFERENCES [dbo].[Coach](CoachID),
UserID INT FOREIGN KEY REFERENCES [dbo].[User](UserID)
)
--============================
-- SP creation
--============================
GO
CREATE PROCEDURE dbo.uspReadUser
/*
Read the user from DB with his roles
*/
@EmailID varchar(50)
AS
BEGIN
	SELECT US.UserID, US.Email,US.Password,FirstName
            ,LastName,DateOfBirth,
            ExternalLoginProvider,ExternalLoginProviderName,
			ExternalProviderKey,IsExternalProvider,IsActive,
			USR.Name,USR.NormalisedName
			FROM [dbo].[User] US INNER JOIN [dbo].[UserRoles] USR ON US.RoleID=USR.RoleID
			WHERE US.Email = @EmailID
END


GO

CREATE PROCEDURE dbo.uspReadCoachesByClassID
--CLASS ID for which we want to have coaches
@ClassID int
AS
BEGIN
	SELECT COA.CoachID,COA.CoachName,COA.PhotoURL 
	FROM [dbo].[Coach] COA INNER JOIN [dbo].[ClassCoach] CC
	ON COA.CoachID=CC.CoachID
	WHERE CC.ClassID=@ClassID
END


GO

CREATE PROCEDURE dbo.uspReadCoachByCoachID
@CoachID int
AS
BEGIN
  SELECT COA.CoachID,COA.CoachName,COA.Degree,COA.PhotoURL,CLA.ClassName FROM [dbo].[Coach] COA LEFT JOIN [dbo].[ClassCoach] CC ON
  COA.CoachID=CC.CoachID INNER JOIN [dbo].[Class] CLA ON
  CLA.ClassID=CC.ClassID
  WHERE COA.CoachID=@CoachID
  
END
