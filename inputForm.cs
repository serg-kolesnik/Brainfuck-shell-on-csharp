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
    public partial class InputForm : Form
    {
        private TextBox inputTextBox;
        private Button okButton;
        private Button cancelButton;
        private string inputText = "";

        public InputForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Настройка формы
            this.Text = "Ввод данных";
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;

            // Создаем метку
            Label promptLabel = new Label
            {
                Text = "Введите данные:",
                Location = new Point(12, 9),
                Size = new Size(260, 20)
            };

            // Создаем поле ввода
            inputTextBox = new TextBox
            {
                Location = new Point(12, 32),
                Size = new Size(260, 20)
            };

            // Создаем кнопку OK
            okButton = new Button
            {
                Text = "OK",
                Location = new Point(116, 70),
                Size = new Size(75, 23)
            };
            okButton.Click += OkButton_Click;

            // Создаем кнопку Cancel
            cancelButton = new Button
            {
                Text = "Отмена",
                Location = new Point(197, 70),
                Size = new Size(75, 23)
            };
            cancelButton.Click += (s, e) => this.Close();

            // Добавляем контролы на форму
            this.Controls.Add(promptLabel);
            this.Controls.Add(inputTextBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            // Устанавливаем кнопку по умолчанию
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            inputText = inputTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string GetInputText()
        {
            return inputText;
        }
    }
}
