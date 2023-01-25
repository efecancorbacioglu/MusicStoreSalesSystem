using MusicStoreSalesSystemSolution.Businesses.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicStoreSalesSystemSolution.UI.Forms
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        public void getMDIChildForm(Form form)
        {
            panelMain.Controls.Clear();
            form.MdiParent = this;
            panelMain.Controls.Add(form);
            form.Show();
        }

        EmployeeBusiness employeeBusiness = new EmployeeBusiness();


        private void btnLogout_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.SetToolTip(btnLogout, "Logout");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Are you sure to exit the system?", "Exit System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                this.Close();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void productsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AdminProductsForm adminProductsForm = new AdminProductsForm();
            getMDIChildForm(adminProductsForm);
        }

        private void employeesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AdminEmployeesForm adminEmployeesForm = new AdminEmployeesForm();
            getMDIChildForm(adminEmployeesForm);
        }

        private void salesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AdminSalesForm adminViewSalesForm = new AdminSalesForm();
            getMDIChildForm(adminViewSalesForm);
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Are you sure to exit the system?", "Exit System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                this.Close();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            var employee = employeeBusiness.Get(em => em.EmployeeId == LoginForm.UserInfo.EmployeeId);
            lblUserInfo.Text = employee.FirstName + " " + employee.LastName;
            pbUserPic.Image = employee.Photo != null ? ImageBusiness.ByteToImage(employee.Photo) : null;
        }
    }
}
