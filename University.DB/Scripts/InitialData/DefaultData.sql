IF NOT EXISTS(SELECT * FROM dbo.LectureTheatres)
BEGIN
SET IDENTITY_INSERT dbo.LectureTheatres ON 
INSERT dbo.LectureTheatres (Id, Name, Capacity, CreatedDate) VALUES(1, 'LectureTheater 1', 10, GETDATE())
INSERT dbo.LectureTheatres (Id, Name, Capacity, CreatedDate) VALUES(2, 'LectureTheater 2', 20, GETDATE())
INSERT dbo.LectureTheatres (Id, Name, Capacity, CreatedDate) VALUES(3, 'LectureTheater 3', 15, GETDATE())
INSERT dbo.LectureTheatres (Id, Name, Capacity, CreatedDate) VALUES(4, 'LectureTheater 4', 12, GETDATE())
INSERT dbo.LectureTheatres (Id, Name, Capacity, CreatedDate) VALUES(5, 'LectureTheater 5', 10, GETDATE())
SET IDENTITY_INSERT dbo.LectureTheatres OFF
END
GO

IF NOT EXISTS(SELECT * FROM dbo.Subjects)
BEGIN
SET IDENTITY_INSERT dbo.Subjects ON 
INSERT dbo.Subjects (Id, Name, CreatedDate) VALUES(1, 'Subject 1', GETDATE())
INSERT dbo.Subjects (Id, Name, CreatedDate) VALUES(2, 'Subject 2', GETDATE())
INSERT dbo.Subjects (Id, Name, CreatedDate) VALUES(3, 'Subject 3', GETDATE())
INSERT dbo.Subjects (Id, Name, CreatedDate) VALUES(4, 'Subject 4', GETDATE())
INSERT dbo.Subjects (Id, Name, CreatedDate) VALUES(5, 'Subject 5', GETDATE())
SET IDENTITY_INSERT dbo.Subjects OFF
END
GO


IF NOT EXISTS(SELECT * FROM dbo.Lectures)
BEGIN
SET IDENTITY_INSERT dbo.Lectures ON 
INSERT dbo.Lectures (Id, Name, StartTime, EndTime, DayOfWeek, LectureTheatreId, SubjectId, CreatedDate) VALUES(1, 'Lecture 1', '9:30', '10:30', 2, 1, 1, GETDATE())
INSERT dbo.Lectures (Id, Name, StartTime, EndTime, DayOfWeek, LectureTheatreId, SubjectId, CreatedDate) VALUES(2, 'Lecture 2', '8:30', '9:30', 3, 1, 2, GETDATE())
INSERT dbo.Lectures (Id, Name, StartTime, EndTime, DayOfWeek, LectureTheatreId, SubjectId, CreatedDate) VALUES(3, 'Lecture 3', '11:00', '12:00', 4, 2, 3, GETDATE())
INSERT dbo.Lectures (Id, Name, StartTime, EndTime, DayOfWeek, LectureTheatreId, SubjectId, CreatedDate) VALUES(4, 'Lecture 4', '10:00', '11:00', 3, 3, 4, GETDATE())
SET IDENTITY_INSERT dbo.Lectures OFF
END
GO