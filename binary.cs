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
        public char[] Execute(string code)
        {
            // char[] charArray = sentence.ToCharArray(); для перевода кода
            byte[] bin = new byte[30000];
            int index = 0;
            char plus = '+';
            char minus = '-';
            char next = '>';
            char back = '<';
            char prt = '.';
            char inp = ',';
            char lp = '[';
            char llp = ']';
            int rc = 0;
            char endl = '\n';
            char ex = '@';
            char deb = '%';
            char[] array_code = code.ToCharArray();
            for (int i = 0; i < array_code.Length; i++)
            {
                switch (array_code[i]) 
                {
                    case '+': bin[index]++ ; break;
                    case '-': bin[index]-- ; break;
                    case '>': index++ ; break;
                    case '<': index-- ; break;
                    case '.': print_char(bin); break;
                    case ',': input_char(bin); break;
                    case ']':
                        if (bin[index - 1] != 0)
                        {
                            int depth = 1;
                            while (depth > 0)
                            {
                                i--;
                                if (i < 0)
                                    throw new Exception("Не найдена открывающая скобка для ']'");

                                if (array_code[i] == ']')
                                    depth++;
                                else if (array_code[i] == '[')
                                    depth--;
                            }
                        }
                        break;
                }
            }
            return bin;
        } 
    }
}
