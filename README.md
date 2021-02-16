# StudentEnrollment
Enrollment System

TASK:
Create a RESTful Web Api to manage a persisted domain model relating to the problem domain of a university where:
•	Students enrol in Subjects;
•	Subjects are taught through a set of Lectures on a weekly schedule; and
•	Each Lecture is delivered in a Lecture Theatre on a given day of the week at a given time for a given duration.
•	Each lecture theatre has a nominated capacity.
Design and implement a persistable domain model to represent the above concepts.  You may use the persistence mechanism (i.e. orm/database) of your choice.
Expose CRUD-style operations for each entity type via the Restful API to support at a minimum:
•	Creating, reading students
•	Creating, reading subjects
•	Creating, reading lecture theatres
•	Creating, reading lectures on a schedule as sub-resources of a subject, where the lecture theatre is identified as a property of the lecture.
•	Reading enrolments as a collection sub-resource of a student resource, returning the list of enrolments
•	Reading students as a collection sub-resource of a subject, returning the list of students enrolled in the subject.
Expose a RESTful api to represent the operation of a student enrolling in a subject.   When a student enrols in a subject, the following business rules should be enforced:
•	If the enrolment would cause any of the lectures to exceed the capacity of its nominated lecture theatre, the enrolment should be rejected
•	If the enrolment would cause the student to have > 10 hours of lectures in a week, the enrolment should be rejected
•	If the enrolment is successful, a notification should be sent to the student.  For the purposes of the exercise, the notification can be simulated by dumping a file to disk.

How to Run: 
Once you open the solution in visual studio. First you need to publish your database locally. 

Right-clicking on a database project 'University.DB' will allow you to access the publication dialog. After entering the connection string for your target database click on 'Publish' and that should publish your local database.

Update connection string in appsettings.json file to reflect your local database. 

Set Univeristy.Api as startup project. Then build and run the application.

Improvements Needed:
1. Adding more error handling and validations.
2. Add test cases.
3. Add logging.
4. Enable post-deployment script in 'University.DB' to run automatically.
5. Refactoring the code more