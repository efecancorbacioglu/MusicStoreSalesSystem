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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public static User UserInfo { get; set; }
        private UserBusiness userBusiness = new UserBusiness();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) && string.IsNullOrEmpty(txtPassword.Text))
            {
                epLogin.SetError(txtUserName, "Please Enter Your User Name");
                epLogin.SetError(txtPassword, "Please Enter Your Password");
            }else if (string.IsNullOrEmpty(txtUserName.Text))
            {
                epLogin.SetError(txtUserName, "Please Enter Your User Name");
                epLogin.SetError(txtPassword, null);
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                epLogin.SetError(txtUserName, null);
                epLogin.SetError(txtPassword, "Please Enter Your Password");
            }
            else
            {
                epLogin.SetError(txtUserName, null);
                epLogin.SetError(txtPassword, null);
                var user = userBusiness.Get(u => u.UserName == txtUserName.Text && u.UserPassword == txtPassword.Text );

                if(user != null)
                {
                    if (userBusiness.UserIsAdmin(user.UserName))
                    {
                        UserInfo = user;
                        AdminMainForm adminMainForm = new AdminMainForm();
                        adminMainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        UserInfo = user;
                        EmployeeSalesForm employeeMainForm = new EmployeeSalesForm();
                        employeeMainForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("The user can not find in the database. Please check your informations", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordForm forgotPassForm = new ForgotPasswordForm();
            forgotPassForm.Show();
        }

    }
}
