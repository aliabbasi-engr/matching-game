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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            richTextBox1.Text = "Concentration, also known as Match Match, Memory, Pelmanism, Shinkei-suijaku, Pexeso or simply Pairs, is a card game in which all of the cards are laid face down on a surface and two cards are flipped face up over each turn. The object of the game is to turn over pairs of matching cards. Concentration can be played with any number of players or as solitaire. It is a particularly good game for young children, though adults may find it challenging and stimulating as well. The scheme is often used in quiz shows and can be employed as an educational game.";
        }
    }
}
