using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;


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
            var code = richTextBox1.Text;
            RunInterpreter(code);
        }


        private void Help_button_Click(object sender, EventArgs e)
        {
            Help helpform = new Help();
            helpform.ShowDialog();
        }



        private Thread interpreterThread;
        private Binary interpreter;

        private void RunInterpreter(string code)
        {
            interpreter = new Binary();
            interpreterThread = new Thread(() => {
                try
                {
                    string result = interpreter.Execute(code);
                    // Обновление UI через Invoke
                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show(result, "Program result");
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show(ex.Message);
                    });
                }
            });
            interpreterThread.Start();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                if (interpreter != null)
                {
                    interpreter.Dispose();
                    interpreter = null;
                    MessageBox.Show("Выполнение прервано", "Program result");
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}