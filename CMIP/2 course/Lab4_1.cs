using System;
using System.Collections.Generic;
using System.IO;

namespace CryptMethodsLab1
{
    class Program
    {
        static Dictionary<char, int> Alphabet = new Dictionary<char, int>
        {
            {'А', 10},
            {'Б', 11},
            {'В', 12},
            {'Г', 13},
            {'Д', 14},
            {'Е', 15},
            {'Ж', 16},
            {'З', 17},
            {'И', 18},
            {'Й', 19},
            {'К', 20},
            {'Л', 21},
            {'М', 22},
            {'Н', 23},
            {'О', 24},
            {'П', 25},
            {'Р', 26},
            {'С', 27},
            {'Т', 28},
            {'У', 29},
            {'Ф', 30},
            {'Х', 31},
            {'Ц', 32},
            {'Ч', 33},
            {'Ш', 34},
            {'Щ', 35},
            {'Ъ', 36},
            {'Ы', 37},
            {'Ь', 38},
            {'Э', 39},
            {'Ю', 40},
            {'Я', 41},
            {' ', 99}
        };

        static int p = 193, q = 347, e = 28471, d = 7;

        static void Main(string[] args)
        {
            int[] M = BlocksDivision(StrConversion(Console.ReadLine().ToUpper()));
            int[] C = new int[M.Length];
            int j = 0;
            foreach (int i in M)
            {
                Console.WriteLine("M{0} = {1}", j, M[j]);
                j++;
            }
            j = 0;
            foreach (int i in M)
            {
                C[j] = Exponentiation(M[j], e);
                Console.WriteLine("C{0} = {1}", j, C[j]);
                j++;
            }
            int[] D = new int[M.Length];
            j = 0;
            foreach (int i in D)
            {
                D[j] = Exponentiation(C[j], d);
                Console.WriteLine("D{0} = {1}", j, D[j]);
                j++;
            }
            j = 0;
            foreach (int i in D)
            {
                if (D[j] == M[j])
                {
                    Console.WriteLine("{0} - correct", j);
                }
                else
                {
                    Console.WriteLine("{0} - incorrect", j);
                }
                j++;
            }
        }



        static int[] BlocksDivision(string str)
        {
            int[] M = new int[1];
            int i = 0;
            string block = "";
            foreach (char c in str)
            {
                block += c;
                if (int.Parse(block) >= q * p)
                {
                    block = block.Substring(block.Length - 1, 1);
                    M = Append(M, 0);
                    i++;
                }
                M[i] = int.Parse(block);
            }
            return M;
        }

        static int[] Append(int[] array, int item) // Расширение массива
        {
            int[] result = new int[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }

        static string StrConversion(string str)
        {
            string result = "";
            foreach (char c in str)
            {
                result += Convert.ToString(GetKeyByValue(c));
            }
            return result;
        }


        static int Exponentiation(int M, int e)
        {
            long result = M;
            for (int i = 0; i < e - 1; i++)
            {
                result = (long)(result * M);

                result = (long)(result % (q * p));

            }
            return (int)result;
        }

        static int GetKeyByValue(char value) // Получить из алфавита ключ по букве
        {
            foreach (var recordOfDictionary in Alphabet)
            {
                if (recordOfDictionary.Key.Equals(value))
                    return recordOfDictionary.Value;
            }
            return -1;
        }

    }
}