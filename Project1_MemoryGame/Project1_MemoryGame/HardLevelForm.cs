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
    public partial class HardLevelForm : Form
    {
        PictureBox firstClickedPictureBox = null;
        PictureBox secondClickedPictureBox = null;
        List<string> picturesLocations = new List<string>() { };
        int moves = 0;
        long score = 0;
        bool timerStatus;
        int intSeconde = 0;
        int originalSeconde = 0;
        string stringSeconde = "";
        int intMinute = 0;
        string stringMinute = "";
        int intHour = 0;
        string stringHour = "";
        string timer = "";

        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
            {
                return null;
            }
            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();
            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
            {
                w -= widths[i];
            }
            int col = i + 1;
            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
            {
                h -= heights[i];
            }
            int row = i + 1;
            return new Point(col, row);
        }

        public void RandomPictureAssignment()
        {
            picturesLocations.Clear();
            Random random = new Random();
            List<string> picturesNumbers = new List<string>()
            {
                "0","0","1","1","2","2","3","3","4","4","5","5","6","6","7","7","8","8","9","9","10","10","11","11","12","12","13","13","14","14","15","15","16","16","17","17"
            };
            int randomNumber;
            string[] picturesPathes;
            if (MainMenuForm.age == 1)
            {
                picturesPathes = System.IO.Directory.GetFiles(Application.StartupPath + "\\PictureSets\\ChildPack");
                foreach (Control control in tableLayoutPanel_66.Controls)
                {
                    PictureBox pictureBox = control as PictureBox;
                    if (pictureBox != null)
                    {
                        randomNumber = random.Next(picturesNumbers.Count);
                        picturesLocations.Add(picturesPathes[int.Parse(picturesNumbers[randomNumber])]);
                        picturesNumbers.RemoveAt(randomNumber);
                    }
                }
            }
            else if (MainMenuForm.age == 2)
            {
                picturesPathes = System.IO.Directory.GetFiles(Application.StartupPath + "\\PictureSets\\TeenagerPack");
                foreach (Control control in tableLayoutPanel_66.Controls)
                {
                    PictureBox pictureBox = control as PictureBox;
                    if (pictureBox != null)
                    {
                        randomNumber = random.Next(picturesNumbers.Count);
                        picturesLocations.Add(picturesPathes[int.Parse(picturesNumbers[randomNumber])]);
                        picturesNumbers.RemoveAt(randomNumber);
                    }
                }
            }
            else if (MainMenuForm.age == 3)
            {
                picturesPathes = System.IO.Directory.GetFiles(Application.StartupPath + "\\PictureSets\\AdultPack");
                foreach (Control control in tableLayoutPanel_66.Controls)
                {
                    PictureBox pictureBox = control as PictureBox;
                    if (pictureBox != null)
                    {
                        randomNumber = random.Next(picturesNumbers.Count);
                        picturesLocations.Add(picturesPathes[int.Parse(picturesNumbers[randomNumber])]);
                        picturesNumbers.RemoveAt(randomNumber);
                    }
                }
            }
        }

        public void CheckGameResult()
        {
            bool success = true;
            foreach (Control control in tableLayoutPanel_66.Controls)
            {
                PictureBox pictureBox = control as PictureBox;
                if (pictureBox.ImageLocation == null)
                {
                    success = false;
                }
            }
            if (success)
            {
                timer2.Stop();
                timerStatus = false;
                score = (9999 / originalSeconde + moves) * 10;
                System.IO.StreamWriter scoreWriter = new System.IO.StreamWriter(Application.StartupPath + "\\Files\\Hard Level Scores.txt", true);
                scoreWriter.WriteLine(score);
                scoreWriter.Close();
                if (MessageBox.Show("Congratulation! You matched all cards in " + timer + " with " + moves + " moves.\nYour score is " + score, "You won!", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }
        public HardLevelForm()
        {
            InitializeComponent();
            timerStatus = true;
            if (MainMenuForm.age == 1)
            {
                BackgroundImage = Image.FromFile(Application.StartupPath + "\\Wallpapers\\childFormWallpaper.jpg");
                tableLayoutPanel_66.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Wallpapers\\childTableLayoutPanelWallpaper.png");
            }
            else if (MainMenuForm.age == 2)
            {
                BackgroundImage = Image.FromFile(Application.StartupPath + "\\Wallpapers\\teenagerFormWallpaper.jpg");
                tableLayoutPanel_66.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Wallpapers\\teenagerTableLayoutPanelWallpaper.jpg");
            }
            else if (MainMenuForm.age == 3)
            {
                BackgroundImage = Image.FromFile(Application.StartupPath + "\\Wallpapers\\adultFormWallpaper.jpg");
                tableLayoutPanel_66.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Wallpapers\\adultTableLayoutPanelWallpaper.jpg");
            }
            foreach (Control control in tableLayoutPanel_66.Controls)
            {
                PictureBox pictureBox = control as PictureBox;
                if (pictureBox.ImageLocation != null)
                {
                    pictureBox.ImageLocation = null;
                    firstClickedPictureBox = null;
                    secondClickedPictureBox = null;
                }
            }
            RandomPictureAssignment();
        }

        private void HardLevelForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (timerStatus)
            {
                moves += 1;
                movesCounterLabel.Text = moves.ToString();
                if (timer1.Enabled == true)
                {
                    return;
                }
                PictureBox clickedPictureBox = sender as PictureBox;
                var cellPos = GetRowColIndex(tableLayoutPanel_66, tableLayoutPanel_66.PointToClient(Cursor.Position));
                switch (cellPos.ToString())
                {
                    case "{X=5,Y=5}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[0];
                            return;
                        }
                        break;
                    case "{X=4,Y=5}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[1];
                            return;
                        }
                        break;
                    case "{X=3,Y=5}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[2];
                            return;
                        }
                        break;
                    case "{X=2,Y=5}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[3];
                            return;
                        }
                        break;
                    case "{X=1,Y=5}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[4];
                            return;
                        }
                        break;
                    case "{X=0,Y=5}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[5];
                            return;
                        }
                        break;
                    case "{X=5,Y=4}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[6];
                            return;
                        }
                        break;
                    case "{X=4,Y=4}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[7];
                            return;
                        }
                        break;
                    case "{X=3,Y=4}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[8];
                            return;
                        }
                        break;
                    case "{X=2,Y=4}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[9];
                            return;
                        }
                        break;
                    case "{X=1,Y=4}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[10];
                            return;
                        }
                        break;
                    case "{X=0,Y=4}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[11];
                            return;
                        }
                        break;
                    case "{X=5,Y=3}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[12];
                            return;
                        }
                        break;
                    case "{X=4,Y=3}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[13];
                            return;
                        }
                        break;
                    case "{X=3,Y=3}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[14];
                            return;
                        }
                        break;
                    case "{X=2,Y=3}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[15];
                            return;
                        }
                        break;
                    case "{X=1,Y=3}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[16];
                            return;
                        }
                        break;
                    case "{X=0,Y=3}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[17];
                            return;
                        }
                        break;
                    case "{X=5,Y=2}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[18];
                            return;
                        }
                        break;
                    case "{X=4,Y=2}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[19];
                            return;
                        }
                        break;
                    case "{X=3,Y=2}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[20];
                            return;
                        }
                        break;
                    case "{X=2,Y=2}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[21];
                            return;
                        }
                        break;
                    case "{X=1,Y=2}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[22];
                            return;
                        }
                        break;
                    case "{X=0,Y=2}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[23];
                            return;
                        }
                        break;
                    case "{X=5,Y=1}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[24];
                            return;
                        }
                        break;
                    case "{X=4,Y=1}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[25];
                            return;
                        }
                        break;
                    case "{X=3,Y=1}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[26];
                            return;
                        }
                        break;
                    case "{X=2,Y=1}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[27];
                            return;
                        }
                        break;
                    case "{X=1,Y=1}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[28];
                            return;
                        }
                        break;
                    case "{X=0,Y=1}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[29];
                            return;
                        }
                        break;
                    case "{X=5,Y=0}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[30];
                            return;
                        }
                        break;
                    case "{X=4,Y=0}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[31];
                            return;
                        }
                        break;
                    case "{X=3,Y=0}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[32];
                            return;
                        }
                        break;
                    case "{X=2,Y=0}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[33];
                            return;
                        }
                        break;
                    case "{X=1,Y=0}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[34];
                            return;
                        }
                        break;
                    case "{X=0,Y=0}":
                        if (clickedPictureBox.ImageLocation != null)
                        {
                            return;
                        }
                        if (firstClickedPictureBox == null)
                        {
                            firstClickedPictureBox = clickedPictureBox;
                            firstClickedPictureBox.ImageLocation = picturesLocations[35];
                            return;
                        }
                        break;
                }
                switch (cellPos.ToString())
                {
                    case "{X=5,Y=5}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[0];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=4,Y=5}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[1];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=3,Y=5}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[2];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=2,Y=5}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[3];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=1,Y=5}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[4];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=0,Y=5}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[5];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=5,Y=4}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[6];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=4,Y=4}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[7];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=3,Y=4}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[8];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=2,Y=4}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[9];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=1,Y=4}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[10];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=0,Y=4}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[11];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=5,Y=3}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[12];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=4,Y=3}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[13];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=3,Y=3}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[14];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=2,Y=3}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[15];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=1,Y=3}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[16];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=0,Y=3}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[17];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=5,Y=2}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[18];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=4,Y=2}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[19];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=3,Y=2}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[20];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=2,Y=2}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[21];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=1,Y=2}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[22];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=0,Y=2}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[23];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=5,Y=1}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[24];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=4,Y=1}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[25];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=3,Y=1}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[26];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=2,Y=1}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[27];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=1,Y=1}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[28];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=0,Y=1}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[29];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=5,Y=0}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[30];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=4,Y=0}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[31];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=3,Y=0}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[32];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=2,Y=0}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[33];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=1,Y=0}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[34];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                    case "{X=0,Y=0}":
                        secondClickedPictureBox = clickedPictureBox;
                        secondClickedPictureBox.ImageLocation = picturesLocations[35];
                        CheckGameResult();
                        if (firstClickedPictureBox.ImageLocation == secondClickedPictureBox.ImageLocation)
                        {
                            firstClickedPictureBox = null;
                            secondClickedPictureBox = null;
                            return;
                        }
                        timer1.Start();
                        break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClickedPictureBox.ImageLocation = null;
            secondClickedPictureBox.ImageLocation = null;
            firstClickedPictureBox = null;
            secondClickedPictureBox = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            originalSeconde += 1;
            intSeconde += 1;
            while (intSeconde > 59)
            {
                intSeconde -= 59;
                intMinute += 1;
            }
            while (intMinute > 59)
            {
                intMinute -= 59;
                intHour += 1;
            }
            if (intSeconde < 10)
            {
                stringSeconde = "0" + intSeconde.ToString();
            }
            else
            {
                stringSeconde = intSeconde.ToString();
            }
            if (intMinute < 10)
            {
                stringMinute = "0" + intMinute.ToString();
            }
            else
            {
                stringMinute = intMinute.ToString();
            }
            if (intHour < 10)
            {
                stringHour = "0" + intHour.ToString();
            }
            else
            {
                stringHour = intHour.ToString();
            }
            timer = stringHour + ":" + stringMinute + ":" + stringSeconde;
            timerLabel.Text = timer;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (timerStatus)
            {
                timer2.Stop();
                timerStatus = false;
                pauseButton.Text = "Continue";
            }
            else
            {
                timer2.Start();
                timerStatus = true;
                pauseButton.Text = "Pause";
            }
        }
    }
}
