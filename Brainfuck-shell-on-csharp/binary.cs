using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainfuck_interpretator
{
    public class Binary : IDisposable
    {

        public string Execute(string code, string input = "")
        {
            char[] array_code = code.ToCharArray();
            byte[] bin = new byte[30000];
            int index = 0;
            string output = "";
            int inputIndex = 0;

            for (int i = 0; i < array_code.Length; i++)
            {
                if (stopRequested) break;

                switch (array_code[i])
                {
                    case '+': bin[index]++; break;
                    case '-': bin[index]--; break;
                    case '>': if (index < bin.Length - 1) index++; break;
                    case '<': if (index > 0) index--; break;
                    case '.': output += (char)bin[index]; break;
                    case ',':
                        if (inputIndex < input.Length)
                            bin[index] = (byte)input[inputIndex++];
                        break;
                    case '[':
                        if (bin[index] == 0)
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
                            i--;
                        }
                        else
                        {
                            int depth = 1;
                            int startPos = i;
                            while (depth > 0)
                            {
                                startPos--;
                                if (array_code[startPos] == ']') depth++;
                                if (array_code[startPos] == '[') depth--;
                            }
                        }
                        break;
                }
            }
            return output;
        }

        private bool stopRequested = false;

        public void Dispose()
        {
            stopRequested = true;
        }
    }
}
