# HospitalProject

## ERD:
Department Table has 2 columns: DeptId (pk), DeptName

Job Table has 6 columns: JobId (pk), JobTitle, Responsibility, Qualification, Offer, DeptId (fk)

The Department and Job tables have a 1-M relationships. A job belongs to one department. A department can have many jobs.

### Done:
Job CRUD, \
Job Views(List, New, Edit, DeleteConfirm, Details, Error),\
Department CRUD\
Department Views (List, New, Edit, DeleteConfirm, Details, Error)

### TODO:
ListJobsForDepartment

Show department page:\
-department info\
-all jobs that are applicable for that department\
-all doctors in that department




