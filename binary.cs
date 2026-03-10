using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brainfuck_interpretator
{
    internal class binary
    {
        public byte[] Execute(string code)
        {
            // char[] charArray = sentence.ToCharArray(); для перевода кода
            byte[] bin = new byte[30000];
            int index = 0;
            char[] array_code = code.ToCharArray();
            for (int i = 0; i < array_code.Length; i++)
            {
                int input_amount = 0;
                int temp = i;
                switch (array_code[i]) 
                {
                    case '+': bin[index]++ ; break;
                    case '-': bin[index]-- ; break;
                    case '>': index++ ; break;
                    case '<': index-- ; break;
                    case '.': //print_char(bin);
                              break;
                    case ',': 
                        while (array_code[i+input_amount] == ',') 
                        {
                            input_amount++;
                        }
                        //запуск inputForm
                        inputForm nen = new inputForm();
                        //string input = nen.labelToString();
                        for (int m = 0; input_amount>m;m++)
                        {

                        }
                        break;
                    case '[':
                        if (bin[index] == 0)
                        {
                            int depth = 1;
                            while (depth > 0)
                            {
                                i++;
                                if (array_code[i] == '[') depth++;
                                if (array_code[i] == ']') depth--;
                            }
                        }
                        break;

                    case ']':
                        if (bin[index] != 0)
                        {
                            int depth = 1;
                            while (depth > 0)
                            {
                                i--;
                                if (array_code[i] == ']') depth++;
                                if (array_code[i] == '[') depth--;
                            }
                        }
                        break;

                }
            }
            return bin;
        } 
    }
}
