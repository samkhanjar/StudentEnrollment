CREATE TABLE [dbo].[Students]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_tbl_Students] PRIMARY KEY CLUSTERED ([Id] ASC)
)
