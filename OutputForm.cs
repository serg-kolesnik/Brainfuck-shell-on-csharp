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
    public partial class OutputForm : Form
    {
        private TextBox outputTextBox;
        private Button closeButton;

        public OutputForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Настройка формы
            this.Text = "Вывод программы";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;

            // Создаем TextBox для вывода
            outputTextBox = new TextBox
            {
                Location = new Point(12, 12),
                Size = new Size(360, 200),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new Font("Consolas", 10),
                WordWrap = true
            };

            // Создаем кнопку закрытия
            closeButton = new Button
            {
                Text = "Закрыть",
                Location = new Point(160, 220),
                Size = new Size(75, 23)
            };
            closeButton.Click += (s, e) => this.Close();

            // Добавляем контролы на форму
            this.Controls.Add(outputTextBox);
            this.Controls.Add(closeButton);
        }

        public void DisplayOutput(string text)
        {
            if (outputTextBox.InvokeRequired)
            {
                outputTextBox.Invoke(new Action<string>(DisplayOutput), text);
            }
            else
            {
                // Очищаем и выводим новый текст
                outputTextBox.Clear();
                outputTextBox.AppendText("Результат выполнения:\r\n\r\n");

                // Показываем символы и их ASCII коды
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    outputTextBox.AppendText($"Символ {i + 1}: '{c}' (ASCII: {(int)c})\r\n");
                }

                outputTextBox.AppendText($"\r\nСтрока целиком: {text}");
            }
        }
    }
}
