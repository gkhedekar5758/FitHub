GO
CREATE DATABASE Fithub
GO

GO
USE Fithub
GO

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

)
