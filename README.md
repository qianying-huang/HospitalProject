HospitalProject

Department Table has 2 columns: DeptId (pk), DeptName

Job Table has 7 columns: JobId (pk), JobTitle, JobDate, Responsibility, Qualification, Offer, DeptId (fk)

The Department and Job tables have a 1-M relationships. A job belongs to one department. A department can have many jobs.

Done:
Job CRUD, 
Job Views(List, Details)

NEED TO FIX:
Job View(New)
When I try to add a job, there is an error message "SqlException: The conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value"
