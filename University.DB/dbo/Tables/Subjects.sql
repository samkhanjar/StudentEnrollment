CREATE TABLE [dbo].[Subjects]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_tbl_Subjects] PRIMARY KEY CLUSTERED ([Id] ASC)
)
