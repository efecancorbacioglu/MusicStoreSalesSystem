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
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }
        UserBusiness userBusiness = new UserBusiness();
        private void btnSendPass_Click(object sender, EventArgs e)
        {
            var user = userBusiness.Get(u => u.Employee.Email == txtEmailFP.Text && u.UserName == txtUsernameFP.Text);

            if (string.IsNullOrEmpty(txtUsernameFP.Text) && string.IsNullOrEmpty(txtEmailFP.Text))
            {
                epForgotPass.SetError(txtUsernameFP, "Please Enter Your User Name");
                epForgotPass.SetError(txtEmailFP, "Please Enter Your Email");
            }
            else if (string.IsNullOrEmpty(txtUsernameFP.Text))
            {
                epForgotPass.SetError(txtUsernameFP, "Please Enter Your User Name");
                epForgotPass.SetError(txtEmailFP, null);
            }
            else if (string.IsNullOrEmpty(txtEmailFP.Text))
            {
                epForgotPass.SetError(txtUsernameFP, null);
                epForgotPass.SetError(txtEmailFP, "Please Enter Your Email");
            }
            else
            {
                epForgotPass.SetError(txtUsernameFP, null);
                epForgotPass.SetError(txtEmailFP, null);
                if (user != null)
                {
                    MailBusiness mailBusiness = new MailBusiness();
                    string mailBody = "Your Password: " + user.UserPassword;

                    try
                    {
                        mailBusiness.SendMail(txtEmailFP.Text, "Password Reminder", mailBody);
                        MessageBox.Show("Your password send your Email", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There is currently a problem in the system. Please try again later.", "Operaion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    this.Close();

                }
                else
                {
                    MessageBox.Show("A user with this information could not be found in the database. Please check your information.", "User does not exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
