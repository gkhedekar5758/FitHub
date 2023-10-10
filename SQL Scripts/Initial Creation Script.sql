GO
CREATE DATABASE Fithub
GO

GO
USE Fithub
GO
--=======================
-- Table
--=======================

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
GO
CREATE TABLE [dbo].[User]
(
[UserID] int PRIMARY KEY IDENTITY(1,1),
[Email] varchar(100),
[Password] varchar(50),
[FirstName] varchar(30) not null,
[LastName] varchar(30) not null,
[ExternalLoginProvider] varchar(30),
[ExternalLoginProviderName] varchar(30),
[ExternalProviderKey] varchar(50),
[IsExternalProvider] bit,
[IsActive] bit,
[RoleID] int FOREIGN KEY REFERENCES [dbo].[UserRoles]([RoleID])
)

GO


CREATE TABLE [dbo].[Class]
(
ClassID int PRIMARY KEY IDENTITY(1,1),
ClassName VARCHAR(50) NOT NULL,
ClassDescription VARCHAR(500),
ClassShortDescription VARCHAR(100),
PhotoURL VARCHAR(50),
PricePerSession Money
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

GO
CREATE TABLE [dbo].[Testimony]
(
TestimonyID INT PRIMARY KEY IDENTITY(1,1),
Testimony VARCHAR(MAX),
Approved BIT DEFAULT 0,
UserID INT FOREIGN KEY REFERENCES [dbo].[User](UserID)
)

GO

CREATE TABLE [dbo].[UserInfo]
(
UserInfoID INT PRIMARY KEY IDENTITY(1,1),
[Height] int null,
[Weight] int null,
BMI int,
MobileNo varchar(10),
EmergencyMobileNo varchar(10) null,
[UserID] int FOREIGN KEY REFERENCES [dbo].[User](UserID)
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
            ,LastName,
            ExternalLoginProvider,ExternalLoginProviderName,
			ExternalProviderKey,IsExternalProvider,IsActive,
			USR.Name,USR.NormalisedName,USERINFO.Height,USERINFO.Weight,USERINFO.BMI
			FROM [dbo].[User] US INNER JOIN [dbo].[UserRoles] USR ON US.RoleID=USR.RoleID
			LEFT JOIN [dbo].[UserInfo] USERINFO ON US.UserID=USERINFO.UserID
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

GO
CREATE PROCEDURE dbo.uspReadCoachRatingByCoachIDUserID
@CoachID int,
@UserID int
AS
BEGIN
  SELECT RatingID,CoachID,UserID,RatingValue FROM dbo.Rating WHERE CoachID=@CoachID AND UserID=@UserID
END

GO
CREATE PROCEDURE dbo.uspInsertTestimony
@Testimony VARCHAR(MAX),
@UserID int,
@TestimonyID INT OUTPUT
AS
BEGIN
  DECLARE @OUT TABLE (tableID int)

  INSERT INTO [dbo].[Testimony]
  (Testimony,UserID,Approved)
  OUTPUT INSERTED.$IDENTITY INTO @OUT
  VALUES
  (@Testimony,@UserID,1) --TODO: only admin can approve this, but at this time let's approve it, when admin module is invoked we can change it

  SET @TestimonyID=(SELECT tableID FROM @OUT)
END

GO
CREATE PROCEDURE dbo.uspModifyTestimony
@Testimony VARCHAR(MAX),
@UserID INT,
@TestimonyID INT
AS
BEGIN
  UPDATE [dbo].[Testimony] SET Testimony=@Testimony WHERE TestimonyID=@TestimonyID AND UserID=@UserID
END
GO

CREATE PROCEDURE dbo.uspReadUserTestimony
@UserID int
AS
BEGIN
  SELECT  COALESCE(Testimony,'') as Testimony,UserID,TestimonyID FROM [dbo].[Testimony] WHERE UserID=@UserID
END
GO


CREATE PROCEDURE dbo.uspAddCoachRating
@UserID int,
@CoachID int,
@Rating int
AS
BEGIN
  INSERT INTO Rating (RatingValue,UserID,CoachID)
  VALUES (@Rating,@UserID,@CoachID)
END

GO
CREATE PROCEDURE dbo.uspUpdateCoachRating
@UserID int,
@CoachID int,
@Rating int
AS
BEGIN
  Update Rating set RatingValue=@Rating
  where CoachID=@CoachID and UserID=@UserID
  
END
GO

CREATE PROCEDURE dbo.uspUserInsert
@email varchar(50),
@password varchar(50)=null,
@firstname varchar(50),
@lastname varchar(50),
@externalloginprovider varchar(50)=null,
@externalloginprovidername varchar(50)=null,
@externalproviderkey varchar(50)=null,
@isexternalprovider bit=0,
@isactive bit=1,
@RoleID int=1,
@ID_OUT INT OUTPUT
AS
BEGIN
DECLARE @out TABLE (tableID INT)  

	INSERT INTO [dbo].[User] (Email,Password,FirstName,LastName,ExternalLoginProvider,ExternalLoginProviderName,ExternalProviderKey,IsExternalProvider,IsActive,RoleID)
	OUTPUT INSERTED.$IDENTITY INTO @out  
	VALUES
	(@email,@password,@firstname,@lastname,@externalloginprovider,@externalloginprovidername,@externalproviderkey,@isexternalprovider,@isactive,@RoleID)

	SET @ID_OUT = (SELECT tableID FROM @out)  
END

GO
 CREATE PROCEDURE dbo.uspUserInfoInsert
 @weight int,
 @height int,
 @BMI int,
 @mobileNo varchar(10),
 @emergencyNo varchar(10),
 @userId int
 AS
 BEGIN
	INSERT INTO dbo.UserInfo 
	(Height,Weight,BMI,MobileNo,EmergencyMobileNo,UserID)
	VALUES 
	(@height,@weight,@BMI,@mobileNo,@emergencyNo,@userId)
 END

 GO
 CREATE PROCEDURE dbo.uspGetAllCoachesRatingByUserID
 @UserID int
 AS
 BEGIN
  SELECT COA.CoachName AS CoachName,RAT.RatingValue AS RatingValue,COA.PhotoURL AS PhotoURL,
  RAT.UserID AS UserID,COA.CoachID AS CoachID FROM dbo.Coach COA INNER JOIN dbo.Rating RAT ON COA.CoachID=RAT.CoachID WHERE RAT.UserID=@UserID
  UNION
  SELECT CoachName AS CoachName,NULL AS RatingValue,PhotoURL AS PhotoURL,@UserID  AS UserID,CoachID FROM Coach
  WHERE CoachID NOT IN (SELECT CoachID FROM Rating WHERE UserID=@UserID)
 END
