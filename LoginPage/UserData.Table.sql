CREATE TABLE [dbo].[UserData]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserName] NCHAR(20) NOT NULL, 
    [Email] NCHAR(50) NOT NULL, 
    [Password] NCHAR(20) NOT NULL, 
    [TeamLeaderCode] INT NULL 
)
