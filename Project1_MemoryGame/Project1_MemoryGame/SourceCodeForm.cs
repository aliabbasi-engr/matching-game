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
    public partial class SourceCodeForm : Form
    {
        public SourceCodeForm()
        {
            InitializeComponent();
            richTextBox1.LoadFile(Application.StartupPath + "\\Files\\Easy Level Source Code.txt", RichTextBoxStreamType.PlainText);
        }
    }
}
