using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainfuck_interpretator
{
    internal class binary_debug
    {
        public string Execute(string code, string input = "", int maxIterations = 1000000)
        {
            char[] array_code = code.ToCharArray();
            byte[] bin = new byte[30000];
            int index = 0;
            string output = "";
            int inputIndex = 0;
            int iterationCount = 0;

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
                        break;

                    case '.':
                        output += (char)bin[index];
                        break;

                    case ',':
                        if (inputIndex < input.Length)
                        {
                            bin[index] = (byte)input[inputIndex];
                            inputIndex++;
                        }
                        break;

                    case '[':
                        iterationCount = bin[index];
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
                                iterationCount++;
                                if (iterationCount > maxIterations)
                                {
                                    throw new Exception($"Превышен лимит итераций ({maxIterations}). Возможен бесконечный цикл.");
                                }
                                if (i < 0) break;
                                if (array_code[i] == ']') depth++;
                                if (array_code[i] == '[') depth--;
                            }
                        }
                        break;
                }
            }

            return output;
        }
    }
}
