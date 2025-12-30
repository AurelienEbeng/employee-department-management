--USE master;
--GO
--ALTER DATABASE[StudentProg]
--SET SINGLE_USER
--WITH ROLLBACK IMMEDIATE;
--GO
--DROP DATABASE[StudentProg];
--GO

--Creating the database
IF db_id('exam1en') IS NULL CREATE DATABASE exam1en;
GO

USE exam1en;
GO
 
-- Creating the Departments table without the foreign key
CREATE TABLE departments
(
    deptId INT NOT NULL ,
    name VARCHAR(50) NOT NULL,
    mgrId INT,
	Constraint pk_dept PRIMARY KEY(deptID)
);
 
 -- Inserting data into the Departments table
INSERT INTO departments (deptId, name, mgrId) 
VALUES 
(1, 'Marketing', 3),
(2, 'Accounting', 1),
(3, 'Finance', 1),
(4, 'IT', 14);
 


-- Create the Employees table with the foreign key
CREATE TABLE employees
(
    empId INT NOT NULL ,
    name VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    salary DECIMAL(10,2) NOT NULL,
    deptId INT NOT NULL,
	Constraint pk_emp PRIMARY KEY(empId),
	Constraint fk_emp_dept FOREIGN KEY (deptId) REFERENCES departments(deptId) 
	ON UPDATE CASCADE ON DELETE NO ACTION
);
 

-- Inserting data into the Employees table
INSERT INTO employees (empId, name, age, salary, deptId) 
VALUES 
(1, 'Mary', 27, 90000, 3),
(3, 'John', 32, 90000, 1),
(7, 'Brian', 28, 80000, 2),
(14, 'Anne', 28, 95000, 4),
(32, 'James', 29, 85000, 1);
 


--Add foreign key in Departments table 
ALTER TABLE departments
ADD Constraint fk_dept_emp FOREIGN KEY (mgrId) REFERENCES employees(empId) 
ON UPDATE NO ACTION ON DELETE NO ACTION;
--foreign key in department from employee