using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brainfuck_interpretator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            OutputForm output = new OutputForm();
            binary_debug main = new binary_debug();
            string deb1 = main.Execute(richTextBox1.Text);
            deblabel.Text = deb1;
            output.label(deb1);

        }

        private void Help_button_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
