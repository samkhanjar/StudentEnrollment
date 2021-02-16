CREATE TABLE [dbo].[LectureTheatres]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Capacity] INT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_tbl_Theaters] PRIMARY KEY CLUSTERED ([Id] ASC)
)
