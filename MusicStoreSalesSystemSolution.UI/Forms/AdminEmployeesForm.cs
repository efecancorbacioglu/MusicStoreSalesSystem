using MusicStoreSalesSystemSolution.Businesses.Concrete;
using MusicStoreSalesSystemSolution.Entities;
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
    public partial class AdminEmployeesForm : Form
    {
        public AdminEmployeesForm()
        {
            InitializeComponent();
        }

        public void ListEmployees()
        {
            
            dgvEmployees.DataSource = employeeBusiness.GetAll();
            dgvEmployees.Columns["Users"].Visible = false;
            dgvEmployees.Columns["Sales"].Visible = false;
            dgvEmployees.Columns["Photo"].Visible = false;
        }

        private void clearTextBoxes()
        {
            txtId.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            pbEmployee.ImageLocation = null;
            checkIsActive.Checked = false;

        }

        EmployeeBusiness employeeBusiness = new EmployeeBusiness();
        UserBusiness userBusiness = new UserBusiness();
        
        string imagePath;
        private void AdminEmployeesForm_Load(object sender, EventArgs e)
        {
            ListEmployees();
        }

        private void dgvEmployees_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dgvEmployees.Rows[e.RowIndex];
            var employee = employeeBusiness.GetById(int.Parse(selectedRow.Cells["EmployeeId"].Value.ToString()));

            txtId.Text = selectedRow.Cells["EmployeeId"].Value.ToString();
            txtName.Text = selectedRow.Cells["FirstName"].Value.ToString();
            txtSurname.Text = selectedRow.Cells["LastName"].Value.ToString();
            txtPhone.Text = selectedRow.Cells["Phone"].Value.ToString();
            txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
            checkIsActive.Checked = (bool)selectedRow.Cells["IsActive"].Value;
            pbEmployee.Image = employee.Photo != null ? ImageBusiness.ByteToImage((byte[])selectedRow.Cells["Photo"].Value) : null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please choose a employee before deleting", "Choose Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var answer = MessageBox.Show("Are you sure to delete this employee?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    var employee = employeeBusiness.GetById(int.Parse(txtId.Text));
                    var user = userBusiness.Get(u => u.EmployeeId == employee.EmployeeId);
                    userBusiness.Delete(user);
                    employeeBusiness.Delete(int.Parse(txtId.Text));

                    
                    MessageBox.Show("The employee deleted from the database", "The product Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ListEmployees();

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtPhone.Text) ||
                string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please enter all employee information completely", "Missing Informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var answer = MessageBox.Show("Are you sure to update this employee?", "Updating", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    var employee = employeeBusiness.GetById(int.Parse(txtId.Text));

                    employeeBusiness.Edit(new Employee
                    {
                        EmployeeId = Convert.ToInt32(txtId.Text),
                        FirstName = txtName.Text,
                        LastName = txtSurname.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text,
                        Photo = pbEmployee.ImageLocation != null ? ImageBusiness.ImageToByte(pbEmployee.ImageLocation) : employee.Photo,
                        IsActive = checkIsActive.Checked,
                    });

                    MessageBox.Show("The Employee Updated", "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListEmployees();
                }
            }
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.JPEG;*.JPG;*.PNG) | *.JPEG;*.JPG;*.PNG";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pbEmployee.ImageLocation = openFileDialog.FileName;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var employee = employeeBusiness.Get(em => em.Phone == txtPhone.Text || em.Email == txtEmail.Text);
            if (employee == null)
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text) || pbEmployee.ImageLocation == null)
                {
                    MessageBox.Show("Please enter all employee information completely", "Missing Informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var answer = MessageBox.Show("Are you sure to add this employee?", "Adding", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                    
                        employeeBusiness.Add(new Employee
                        {
                            FirstName = txtName.Text,
                            LastName = txtSurname.Text,
                            Email = txtEmail.Text,
                            Phone = txtPhone.Text,
                            Photo = pbEmployee.ImageLocation != null ? ImageBusiness.ImageToByte(pbEmployee.ImageLocation) : null,
                            IsActive = checkIsActive.Checked,
                        });

                        MessageBox.Show("The employee insert the database.", "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListEmployees();
                        clearTextBoxes();
                    }
                }
            }
            else
            {
                MessageBox.Show("An employee of this information is available in the database. Please check the employee phone number and email.", "The employee exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearTextBoxes();
        }
    }
}
