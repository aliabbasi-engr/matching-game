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
    public partial class ScoresRichTextBox : Form
    {
        public ScoresRichTextBox()
        {
            InitializeComponent();
            switch (HighScores.level)
            {
                case 1:
                    richTextBox1.LoadFile(Application.StartupPath + "\\Files\\Easy Level Scores.txt", RichTextBoxStreamType.PlainText);
                    break;
                case 2:
                    richTextBox1.LoadFile(Application.StartupPath + "\\Files\\Medium Level Scores.txt", RichTextBoxStreamType.PlainText);
                    break;
                case 3:
                    richTextBox1.LoadFile(Application.StartupPath + "\\Files\\Hard Level Scores.txt", RichTextBoxStreamType.PlainText);
                    break;
            }
        }
    }
}
