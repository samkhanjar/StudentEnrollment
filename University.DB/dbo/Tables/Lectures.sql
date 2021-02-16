CREATE TABLE [dbo].[Lectures]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [LectureTheatreId] INT NOT NULL, 
    [SubjectId] INT NOT NULL, 
    [DayOfWeek] INT NOT NULL, 
    [StartTime] TIME NOT NULL, 
    [EndTime] TIME NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Lectures] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Lectures_Theaters] FOREIGN KEY ([LectureTheatreId]) REFERENCES [dbo].[LectureTheatres]([Id]),
    CONSTRAINT [FK_Lectures_Subjects] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects]([Id])
)
