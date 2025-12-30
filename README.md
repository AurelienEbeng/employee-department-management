# Functional Requirements
## Database Creation
- The system shall create a SQL Server database named exam1en.
- The system shall create a Departments table with the following attributes:
     - DeptId (INT, NOT NULL, Primary Key)
     - Name (VARCHAR(50), NOT NULL)
     - MgrId (INT, Foreign Key)
- The system shall create an Employees table with the following attributes:
     - EmpId (INT, NOT NULL, Primary Key)
     - Name (VARCHAR(50), NOT NULL)
     - Age (INT, NOT NULL)
     - Salary (DECIMAL(10,2), NOT NULL)
     - DeptId (INT, NOT NULL, Foreign Key)
- The system shall enforce a foreign key relationship from Employees.DeptId to Departments.DeptId.
- The system shall prevent deletion of a department if employees are assigned to it.
- The system shall propagate updates of Departments.DeptId to Employees.DeptId.
- The system shall enforce a foreign key relationship from Departments.MgrId to Employees.EmpId.
- The system shall prevent deletion of an employee who is assigned as a department manager.
- The system shall prevent updating the EmpId of an employee who is a department manager.
- The system shall insert predefined department and employee records into the database.
- The system shall execute the SQL script to create the database and tables successfully.

## Application Functionality 
- The system shall be a 3-tier C# Windows Forms application using ADO.NET.
- The application shall provide a main menu with an option to manage Employees.
- The application shall display employee data using a DataGridView.
- The application shall allow users to view, add, update, and delete one or multiple employee records.
- The application shall use SqlDataAdapter for employee data operations.
- The application shall provide a main menu option to manage Departments.
- The application shall display department data using a DataGridView.
- The application shall allow users to view, add, update, and delete one or multiple department records.
- The application shall use SqlDataAdapter for department data operations.
- The system shall load both Employees and Departments DataTables into memory before performing operations to correctly enforce foreign key restrictions.

## Business Rules
- The system shall prevent insertion or update of an employee younger than 18 years old.
- The system shall prevent insertion or update of an employee older than 120 years old.
- The system shall prevent insertion or update of an employee with a salary less than or equal to CAD 15,000.00.
- The system shall display specific error messages when business rules are violated.
- The system shall reject invalid insert or update operations.

# Entity Relationship Diagram
<img width="339" height="580" alt="image" src="https://github.com/user-attachments/assets/75e1131a-0f4b-4eb8-83a3-0e7623d5a9f5" />
