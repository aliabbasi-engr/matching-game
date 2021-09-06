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
    public partial class HighScores : Form
    {
        public static int level;
        public HighScores()
        {
            InitializeComponent();
            double reader;
            List<double> easyLevelScores = new List<double> { };
            System.IO.StreamReader easyLevelScoresReader = new System.IO.StreamReader(Application.StartupPath + "\\Files\\Easy Level Scores.txt");
            while (easyLevelScoresReader.EndOfStream != true)
            {
                reader = double.Parse(easyLevelScoresReader.ReadLine());
                easyLevelScores.Add(reader);
                easyLevelScores.Sort();
            }
            easyLevelScoresReader.Close();
            easyLevelScores.Reverse();
            List<double> mediumLevelScores = new List<double> { };
            System.IO.StreamReader mediumLevelScoresReader = new System.IO.StreamReader(Application.StartupPath + "\\Files\\Medium Level Scores.txt");
            while (mediumLevelScoresReader.EndOfStream != true)
            {
                reader = double.Parse(mediumLevelScoresReader.ReadLine());
                mediumLevelScores.Add(reader);
                mediumLevelScores.Sort();
            }
            mediumLevelScoresReader.Close();
            mediumLevelScores.Reverse();
            List<double> hardLevelScores = new List<double> { };
            System.IO.StreamReader hardLevelScoresReader = new System.IO.StreamReader(Application.StartupPath + "\\Files\\Hard Level Scores.txt");
            while (hardLevelScoresReader.EndOfStream != true)
            {
                reader = double.Parse(hardLevelScoresReader.ReadLine());
                hardLevelScores.Add(reader);
                hardLevelScores.Sort();
            }
            hardLevelScoresReader.Close();
            hardLevelScores.Reverse();
            for (int i = 0; i <= 2; i++)
            {
                easyLevelScoresListBox.Items.Add(i+1 + ". " + easyLevelScores[i]);
            }
            for (int i = 0; i <= 2; i++)
            {
                mediumLevelScoresListBox.Items.Add(i + 1 + ". " + mediumLevelScores[i]);
            }
            for (int i = 0; i <= 2; i++)
            {
                hardLevelScoresListBox.Items.Add(i + 1 + ". " + hardLevelScores[i]);
            }
        }

        private void easyLevelSaveButton_Click(object sender, EventArgs e)
        {
            System.IO.File.Copy(Application.StartupPath + "\\Files\\Easy Level Scores.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Easy Level Scores.txt", true);
            MessageBox.Show("File saved successfuly.", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mediumLevelSaveButton_Click(object sender, EventArgs e)
        {
            System.IO.File.Copy(Application.StartupPath + "\\Files\\Medium Level Scores.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Medium Level Scores.txt", true);
            MessageBox.Show("File saved successfuly.", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void hardLevelSaveButton_Click(object sender, EventArgs e)
        {
            System.IO.File.Copy(Application.StartupPath + "\\Files\\Hard Level Scores.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Hard Level Scores.txt", true);
            MessageBox.Show("File saved successfuly.", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void easyLevelShowButton_Click(object sender, EventArgs e)
        {
            level = 1;
            ScoresRichTextBox scoresRichTextBox = new ScoresRichTextBox();
            scoresRichTextBox.ShowDialog();
        }

        private void mediumLevelShowButton_Click(object sender, EventArgs e)
        {
            level = 2;
            ScoresRichTextBox scoresRichTextBox = new ScoresRichTextBox();
            scoresRichTextBox.ShowDialog();
        }

        private void hardLevelShowButton_Click(object sender, EventArgs e)
        {
            level = 3;
            ScoresRichTextBox scoresRichTextBox = new ScoresRichTextBox();
            scoresRichTextBox.ShowDialog();
        }
    }
}
