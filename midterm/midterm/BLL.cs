using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLayer
{
    internal class Departments
    {
        internal static int UpdateDepartments()
        {
            return Data.Departments.UpdateDepartments();
        }
    }
    
    internal class Employees
    {
        internal static int UpdateEmployees()
        {
            DataTable dt = Data.Employees.GetEmployees().GetChanges
                (DataRowState.Added | DataRowState.Modified);
            if ((dt != null) && ((dt.Select("age<18").Length
                > 0) || (dt.Select("age > 120").Length > 0)
                || (dt.Select("salary <= 15000").Length > 0)
                ))
            {
                if (dt.Select("age<18").Length> 0)
                {
                    MessageBox.Show("Age cannot be less than 18","Command Rejected",
                        MessageBoxButtons.OK);
                }


                else if (dt.Select("age>120").Length > 0)
                {
                    MessageBox.Show("Age cannot be greater than 120", "Command Rejected",
                        MessageBoxButtons.OK);
                }



                else if (dt.Select("salary<=15000").Length > 0)
                {
                    MessageBox.Show("Salary cannot be less than or equal to 15000"
                        , "Command Rejected",MessageBoxButtons.OK);
                }

                Data.Employees.GetEmployees().RejectChanges();
                return -1;
            }
            else
            {
                return Data.Employees.UpdateEmployees();
            }
        }
    }
}
