CREATE TABLE [dbo].[LoyaltyWebTest]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FileName] NVARCHAR(100) NOT NULL, 
    [Total] INT NULL, 
    [Passed] INT NULL, 
    [Failed] INT NULL, 
    [Time] TIME NULL
)
