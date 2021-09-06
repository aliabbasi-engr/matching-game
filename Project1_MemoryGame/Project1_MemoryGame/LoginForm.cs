using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_MemoryGame
{
    public partial class LoginForm : Form
    {
        string adminUsername = "aliabbasi";
        string adminPassword = "5256";
        public LoginForm()
        {
            InitializeComponent();
            if (MainMenuForm.adminAccess)
            {
                userNameTextBox.Text = adminUsername;
                paswordTextBox.Text = "*********";
                logButton.Text = "Log out";
            }
            else
            {
                userNameTextBox.Text = "";
                paswordTextBox.Text = "";
                logButton.Text = "Log in";
            }
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            if (MainMenuForm.adminAccess)
            {
                MainMenuForm.adminAccess = false;
                userNameTextBox.Text = "";
                paswordTextBox.Text = "";
                logButton.Text = "Log in";
                if ((MessageBox.Show("Loged out successfuly.", "Access status", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK))
                {
                    this.Close();
                }
            }
            else
            {
                if (userNameTextBox.Text == adminUsername && paswordTextBox.Text == adminPassword)
                {
                    MainMenuForm.adminAccess = true;
                    logButton.Text = "Log out";
                    if (MessageBox.Show("Loged in successfuly.", "Access status", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (MessageBox.Show("Invalid Username or Password.", "Unable to log in", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        userNameTextBox.Text = "";
                        paswordTextBox.Text = "";
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}
