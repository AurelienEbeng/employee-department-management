using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeesDepartments
{
    public partial class Form1 : Form
    {
        private bool OKToChange = true;
        public Form1()
        {
            InitializeComponent();
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Employees.GetEmployees();
                bindingSource1.Sort = "empId";
                dataGridView1.DataSource = bindingSource1;
            }
            else
            {
                OKToChange = true;
            }
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Employees.GetEmployees();
                bindingSource1.Sort = "empId";
                dataGridView1.DataSource = bindingSource1;
            }
            else
            {
                OKToChange = true;
            }
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Departments.GetDepartments();
                bindingSource1.Sort = "deptId";
                dataGridView1.DataSource = bindingSource2;
            }
            else
            {
                OKToChange = true;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Employees.UpdateEmployees() == -1)
            {
                Validate();
            }
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Departments.UpdateDepartments();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            OKToChange = true;
            BindingSource temp = (BindingSource)dataGridView1.DataSource;
            Validate();
            

            if (temp == bindingSource1)
            {
                if (BusinessLayer.Employees.UpdateEmployees() == -1)
                {
                    OKToChange = false;
                }
            }
            else if (temp == bindingSource2)
            {
                if (BusinessLayer.Departments.UpdateDepartments() == -1)
                {
                    OKToChange = false;
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            /*
            MessageBox.Show("Impossible to insert / update / delete");
            e.Cancel = false;
            OKToChange = false;*/

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BindingSource temp = (BindingSource)dataGridView1.DataSource;

            if (temp == bindingSource1)
            {
                if (BusinessLayer.Employees.UpdateEmployees() == -1)
                {
                    OKToChange = false;
                }
            }
            else if (temp == bindingSource2)
            {
                if (BusinessLayer.Departments.UpdateDepartments() == -1)
                {
                    OKToChange = false;
                }
            }

            if (!OKToChange)
            {
                e.Cancel = true;
                OKToChange = true;
            }
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            try
            {
                dataGridView1.Rows.RemoveAt(index);

                Data.DataTables.ReloadTables();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Delete Error",MessageBoxButtons.OK);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close the app?", "Exit", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question).ToString() == "Yes")
            {
                Application.Exit();
            }
        }
    }
}
