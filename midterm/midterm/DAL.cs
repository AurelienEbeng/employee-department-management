using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Connect
    {
        private static String empDeptConnectionString = GetConnectString();
        
        internal static String ConnectionString { get => empDeptConnectionString; }

        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "exam1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }
    }// end of internal class Connect

    internal class DataTables
    {
        private static SqlDataAdapter adapterEmployees = InitAdapterEmployees();
        private static SqlDataAdapter adapterDepartments = InitAdapterDepartments();

        public static DataSet ds = InitDataSet();
        private static SqlDataAdapter InitAdapterEmployees()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * from employees order by empId",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            builder.ConflictOption = ConflictOption.OverwriteChanges;
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterDepartments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * from departments order by deptId",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            builder.ConflictOption = ConflictOption.OverwriteChanges;
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();

            loadDepartments(ds);
            loadEmployees(ds);
            createFkDepartments(ds);
            createFkEmployees(ds);
            return ds;
        }

        private static void loadDepartments(DataSet ds)
        {
            adapterDepartments.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterDepartments.Fill(ds, "departments");
        }

        private static void loadEmployees(DataSet ds)
        {
            adapterEmployees.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterEmployees.Fill(ds, "employees");
        }

        private static void createFkDepartments(DataSet ds)
        {
            ForeignKeyConstraint fk_dept_emp = new ForeignKeyConstraint("fk_dept_emp",
                new DataColumn[]
                {
                    ds.Tables["employees"].Columns["empId"],
                },
                new DataColumn[]
                {
                    ds.Tables["departments"].Columns["mgrId"],
                });
            fk_dept_emp.DeleteRule = Rule.None;
            fk_dept_emp.UpdateRule = Rule.None;
            ds.Tables["departments"].Constraints.Add(fk_dept_emp);
        }


        private static void createFkEmployees(DataSet ds)
        {
            ForeignKeyConstraint fk_emp_dept = new ForeignKeyConstraint("fk_emp_dept",
                new DataColumn[]
                {
                    ds.Tables["departments"].Columns["deptId"],
                },
                new DataColumn[]
                {
                    ds.Tables["employees"].Columns["deptId"],
                });
            fk_emp_dept.DeleteRule = Rule.None;
            fk_emp_dept.UpdateRule = Rule.Cascade;
            ds.Tables["employees"].Constraints.Add(fk_emp_dept);
        }


        internal static SqlDataAdapter getAdapterEmployees()
        {
            return adapterEmployees;
        }

        internal static SqlDataAdapter getAdapterDepartments()
        {
            return adapterDepartments;
        }

        internal static DataSet getDataSet()
        {
            return ds;
        }

        internal static void ReloadTables()
        {
            ds = InitDataSet();
        }
    }// end of internal class DataTables


    internal class Employees
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEmployees();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetEmployees()
        {
            return ds.Tables["employees"];
        }

        internal static int UpdateEmployees()
        {
            if (!ds.Tables["employees"].HasErrors)
            {
                return adapter.Update(ds.Tables["employees"]);
            }
            else
            {
                return -1;
            }
        }
    }//end of internal class Employees


    internal class Departments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterDepartments();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetDepartments()
        {
            return ds.Tables["departments"];
        }

        internal static int UpdateDepartments()
        {
            if (!ds.Tables["departments"].HasErrors)
            {
                return adapter.Update(ds.Tables["departments"]);
            }
            else
            {
                return -1;
            }
        }
    }//end of internal class Departments
}// end of namespace Data
