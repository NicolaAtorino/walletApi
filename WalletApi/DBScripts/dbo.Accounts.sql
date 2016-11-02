CREATE TABLE [dbo].[Accounts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [Balance] DECIMAL NOT NULL, 
    [Enabled] BIT NOT NULL DEFAULT 1
)
