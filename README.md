# HospitalProject

## ERD:
Department Table has 2 columns: DeptId (pk), DeptName\
Job Table has 6 columns: JobId (pk), JobTitle, Responsibility, Qualification, Offer, DeptId (fk)

The Department and Job tables have a 1-M relationships. A job belongs to one department. A department can have many jobs.

### Need To Be Done:

Leo (Lam) Nguyen: Patient, Appointment

Anubhav Sharma: Doctor, Department

Anurag Negi: Article, Author

Qianying (Kate) Huang: Adminstrator 


#### Done:
Leo (Lam) Nguyen:
Anubhav Sharma:
Anurag Negi:

Qianying (Kate) Huang: 
- Job CRUD, \
- Job Views(List, New, Edit, DeleteConfirm, Details, Error),\
- Department CRUD,\
- Department Views (List, New, Edit, DeleteConfirm, Details, Error)
- ListJobsForDepartment

##### TODO:
ListDoctorsForDepartment
ListAppointmentsForPatient
ListAppointmentsForDoctor
ListLogForAdmin




