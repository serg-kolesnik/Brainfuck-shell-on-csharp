using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Brainfuck_interpretator
{
    internal class binary
    {
        public void Execute(string code)
        {
            char[] array_code = code.ToCharArray(); // для посимвольного чтения кода
            byte[] bin = new byte[30000]; // память
            int index = 0; // указатель

            for (int i = 0; i < array_code.Length; i++)
            {
                switch (array_code[i])
                {
                    case '+':
                        bin[index]++;
                        break;

                    case '-':
                        bin[index]--;
                        break;

                    case '>':
                        if (index < bin.Length - 1) index++;
                        break;

                    case '<':
                        if (index > 0) index--;
                        break;// до этого всё работает... а после нетъ

                    case '.':
                        // Определяем количество символов для вывода
                        int output_amount = 1;
                        while (i + output_amount < array_code.Length &&
                               array_code[i + output_amount] == ',')
                        {
                            output_amount++;
                        }

                        // Собираем байты из памяти
                        byte[] outputBytes = new byte[output_amount];
                        for (int j = 0; j < output_amount; j++)
                        {
                            if (index + j < bin.Length)
                            {
                                outputBytes[j] = bin[index + j];
                            }
                        }

                        // Преобразуем в ASCII строку
                        string outputText = Encoding.ASCII.GetString(outputBytes);

                        // Отправляем в форму вывода
                        OutputForm outputForm = new OutputForm();
                        outputForm.ShowDialog();
                        outputForm.DisplayOutput(outputText);

                        // Пропускаем обработанные запятые
                        i += output_amount - 1;
                        break;

                    case ',':
                        // Определяем количество символов для ввода
                        int input_amount = 1;
                        while (i + input_amount < array_code.Length &&
                               array_code[i + input_amount] == ',')
                        {
                            input_amount++;
                        }

                        // Запуск формы ввода
                        InputForm inputForm = new InputForm();
                        inputForm.ShowDialog();
                        string input = inputForm.GetInputText();

                        // Записываем данные в память
                        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                        for (int j = 0; j < input_amount && j < inputBytes.Length; j++)
                        {
                            if (index + j < bin.Length)
                            {
                                bin[index + j] = inputBytes[j];
                            }
                        }

                        // Пропускаем обработанные запятые
                        i += input_amount - 1;
                        break;

                    case '[': // начало цикла
                        if (bin[index] != 0)
                        {
                            int depth = 1;
                            while (depth > 0)
                            {
                                i++;
                                if (i >= array_code.Length) break;
                                if (array_code[i] == '[') depth++;
                                if (array_code[i] == ']') depth--;
                            }
                        }
                        break;

                    case ']': // конец цикла
                        if (bin[index] != 0)
                        {
                            int depth = 1;
                            while (depth > 0)
                            {
                                i--;
                                if (i < 0) break;
                                if (array_code[i] == ']') depth++;
                                if (array_code[i] == '[') depth--;
                            }
                        }
                        break;
                }
            }
        }
    }
}
