using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptMethodsLab1
{
    class Program
    {
        static Dictionary<char, int> Alphabet = new Dictionary<char, int>
        {
                {'�', 0 },
                {'�', 1 },
                {'�', 2 },
                {'�' ,3 },
                {'�',4 },
                {'�' ,5 },
                {'�',6},
                {'�' ,7},
                {'�' ,8},
                {'�' ,9},
                {'�',10 },
                {'�' ,11},
                {'�' ,12},
                {'�' ,13},
                {'�' ,14},
                {'�' ,15},
                {'�' ,16},
                {'�' ,17},
                {'�' ,18},
                {'�' ,19},
                {'�' ,20},
                {'�' ,21},
                {'�' ,22},
                {'�' ,23},
                {'�' ,24},
                {'�' ,25},
                {'�' ,26},
                {'�' ,27},
                {'�' ,28},
                {'�' ,29},
                {'�' ,30},
                {'�' ,31 }


        };
        static void Main(string[] args) // ���� ��� ������ ��������
        {
            const string prime_string = "������������������������������������������������������������������������������������������������������";

            char[] top_words = TopOftenElems(prime_string);
            Console.WriteLine(top_words);
            for (int i = 0; i < 7; i++) // ������� ��������
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i != j)
                    {
                        var (a, b) = Calculating((byte)top_words[i], (byte)top_words[j], 0);
                        Console.WriteLine(Decryption(prime_string, a, b));
                        (a, b) = Calculating((byte)top_words[i], (byte)top_words[j], 1);
                        Console.WriteLine(Decryption(prime_string, a, b));
                    }
                }
            }

        }



        static char OftenElement(string str) // ����� ����� ������ ����� � ������
        {
            char[] arr = str.ToCharArray();
            int count, count_max = 0;
            char max_el = 'a';
            foreach (char c in arr) // ��� ������� ������� ������
            {
                count = 0;
                foreach (Match m in Regex.Matches(str, c.ToString())) // �������, ������� ��� �� ���������� � ������.
                {
                    count++;
                }
                if (count >= count_max) // ���� ������ ���������� ���� ������, 
                {
                    count_max = count;
                    max_el = c; // �� �� - ����� ������.
                }


            }
            return max_el;
        }

        static char[] TopOftenElems(string str) // ����������� ���� ������ �������� � �������� ������
        {
            char[] top = new char[32];
            int i = 0;
            while (str != "")
            {
                top[i] = OftenElement(str);
                str = str.Replace(top[i].ToString(), "");
                i++;
            }
            return top;
        }

        static (int a, int b) Calculating(byte first, byte second, int param) // ������ a � b 
        {
            first -= 48;
            second -= 48;
            int a;
            int b;
            if (param == 0)
            {
                a = DivByMod(32, (first - second + 32) % 32, ((GetKeyByValue('�') - GetKeyByValue('�')) + 32) % 32);
                b = (((first - GetKeyByValue('�') * a) % 32) + 32) % 32;
            }
            else
            {
                a = DivByMod(32, (second - first + 32) % 32, ((GetKeyByValue('�') - GetKeyByValue('�')) + 32) % 32);
                b = (((second - GetKeyByValue('�') * a) % 32) + 32) % 32;

            }
            return (a, b);
        }


        static string Decryption(string str, int a, int b) // ������������� 
        {

            char[] arr = str.ToCharArray();
            int[] word_nums = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                word_nums[i] = (ReciprocalNumber(a, 32) * (GetKeyByValue(arr[i]) - b + 32)) % 32;
                arr[i] = GetValueByKey(word_nums[i]);
            }
            return new string(arr);
        }

        static int ReciprocalNumber(int a, int m) // ����� ��������� �������� �� ������ m
        {
            int x = 1;
            while (((a * x) % m) != 1)
            {
                x++;
                if (x > m)
                {
                    return 0;
                }
            }
            return x;
        }

        static int DivByMod(int m, int c, int d) // ������� �� ������
        {
            int x = 0;
            if (d < 0)
            {
                d += m;
            }
            while ((c + x * m) % d != 0)
            {
                x++;
                if (x > m)
                {
                    Console.WriteLine("Inf cycle!");
                    break;
                }
            }
            return ((c + x * m) / d) + m % m;
        }

        static char GetValueByKey(int key) // �������� �� �������� ����� �� �����
        {
            foreach (var recordOfDictionary in Alphabet)
            {
                if (recordOfDictionary.Value.Equals(key))
                    return recordOfDictionary.Key;
            }
            return '1';
        }

        static int GetKeyByValue(char value) // �������� �� �������� ���� �� �����
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