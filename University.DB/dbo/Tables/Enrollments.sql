CREATE TABLE [dbo].[Enrollments]
(
	[StudentId] INT NOT NULL, 
    [SubjectId] INT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED ([StudentId] ASC, [SubjectId] ASC),
    CONSTRAINT [FK_Enrollments_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students]([Id]),
    CONSTRAINT [FK_Enrollments_Subjects] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects]([Id])
)
