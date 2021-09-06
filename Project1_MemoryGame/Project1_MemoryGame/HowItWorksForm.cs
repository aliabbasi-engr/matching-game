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
    public partial class HowItWorksForm : Form
    {
        public HowItWorksForm()
        {
            InitializeComponent();
            listBox1.Items.Add("1. Save pictures pathes alphabetical in an array.");
            listBox1.Items.Add("2. Choose random indexers from alphabetical pictures pathes array and assign them to a list.");
            listBox1.Items.Add("3. Assign list pictures to each picturebox in table layot panel and save them in another array.");
            listBox1.Items.Add("4. Define a method to return X and Y of clicked tablel layot panel cell.");
            listBox1.Items.Add("5. Define a click method to show corresponding picture from array in clicked cell.");
            listBox1.Items.Add("6. Use a timer that ticks each 500 miliseconde to show both clicked cell.");
            listBox1.Items.Add("7. Use a timer that ticks each 1 second to count game running time.");
            listBox1.Items.Add("8. Define firstClick and secondeClick boolian variables to control that the app should show only 2 cells.");
            listBox1.Items.Add("9. Define a method that checkes corresponding firstClick and secondClick pictureboxes pictures to be same, if they were, make them visible.");
            listBox1.Items.Add("10. Define a method to call it each time that player clicks two cells to check winning by checking visiblity of all cells in table layot panel");
        }
    }
}
