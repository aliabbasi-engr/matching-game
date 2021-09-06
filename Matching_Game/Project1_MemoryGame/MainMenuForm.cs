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
    public partial class MainMenuForm : Form
    {
        public static int age;
        public static bool adminAccess = false;
        public MainMenuForm()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile(Application.StartupPath+"\\Wallpapers\\mainMenuWallpaper.jpg");
            if (adminAccess != true)
            {
                label2.Text = "Not loged in";
                label3.Text = "Click to log in";
            }
        }

        private void themeGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the game?","Memory Game",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void changeWallpaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string choosenFileLocation; 
            openFileDialog1.Title = "Open";
            openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG|*.png|All files|*.*";
            openFileDialog1.ShowDialog();
            try
            {
                choosenFileLocation = openFileDialog1.FileName;
                BackgroundImage = Image.FromFile(choosenFileLocation);
            }
            catch
            { }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ChildradioButton.Checked == true)
            {
                age = 1;
            }
            if (TeenagerRadioButton.Checked == true)
            {
                age = 2;
            }
            if (AdultRadioButton.Checked == true)
            {
                age = 3;
            }
            if (easyRadioButton.Checked == true)
            {
                EasyLevelForm easyLevelForm = new EasyLevelForm();
                easyLevelForm.ShowDialog();
            }
            else if (mediumRadioButton.Checked == true)
            {
                MediumLevelForm mediumLevelForm = new MediumLevelForm();
                mediumLevelForm.ShowDialog();
            }
            else if (hardRadioButton.Checked == true)
            {
                HardLevelForm hardLevelForm = new HardLevelForm();
                hardLevelForm.ShowDialog();
            }
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighScores highScores = new HighScores();
            highScores.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (adminAccess)
            {
                label2.Text = "Loged in";
                label3.Text = "Click to log out";
            }
            else
            {
                label2.Text = "Not loged in";
                label3.Text = "Click to log in";
            }
        }

        private void howItWorksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adminAccess == true)
            {
                HowItWorksForm howItWorks = new HowItWorksForm();
                howItWorks.Show();
            }
            else
            {
                MessageBox.Show("You don't have access to this page, Please log in first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void viewSourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adminAccess == true)
            {
                SourceCodeForm sourceCodeForm = new SourceCodeForm();
                sourceCodeForm.Show();
            }
            else
            {
                MessageBox.Show("You don't have access to this page, Please log in first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
