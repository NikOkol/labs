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
                {'а', 0 },
                {'б', 1 },
                {'в', 2 },
                {'г' ,3 },
                {'д',4 },
                {'е' ,5 },
                {'ж',6},
                {'з' ,7},
                {'и' ,8},
                {'й' ,9},
                {'к',10 },
                {'л' ,11},
                {'м' ,12},
                {'н' ,13},
                {'о' ,14},
                {'п' ,15},
                {'р' ,16},
                {'с' ,17},
                {'т' ,18},
                {'у' ,19},
                {'ф' ,20},
                {'х' ,21},
                {'ц' ,22},
                {'ч' ,23},
                {'ш' ,24},
                {'щ' ,25},
                {'ъ' ,26},
                {'ы' ,27},
                {'ь' ,28},
                {'э' ,29},
                {'ю' ,30},
                {'я' ,31 }


        };
        static void Main(string[] args) // Меню для выбора действия
        {
            const string prime_string = "бцияхъпаххъпкцмлчлпежмьктлжцднрльклзнэияхиглякцяльгксняжмкгицияпкхмрлглжшлцктажбглыкйнслйихажмциягайль";

            char[] top_words = TopOftenElems(prime_string);
            Console.WriteLine(top_words);
            for (int i = 0; i < 7; i++) // Перебор символов
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



        static char OftenElement(string str) // Поиск самой частой буквы в строке
        {
            char[] arr = str.ToCharArray();
            int count, count_max = 0;
            char max_el = 'a';
            foreach (char c in arr) // Для каждого символа строки
            {
                count = 0;
                foreach (Match m in Regex.Matches(str, c.ToString())) // считаем, сколько раз он появляется в строке.
                {
                    count++;
                }
                if (count >= count_max) // Если символ появляется чаще других, 
                {
                    count_max = count;
                    max_el = c; // то он - самый частый.
                }


            }
            return max_el;
        }

        static char[] TopOftenElems(string str) // Составление топа частых символов в заданной строке
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

        static (int a, int b) Calculating(byte first, byte second, int param) // Расчет a и b 
        {
            first -= 48;
            second -= 48;
            int a;
            int b;
            if (param == 0)
            {
                a = DivByMod(32, (first - second + 32) % 32, ((GetKeyByValue('о') - GetKeyByValue('е')) + 32) % 32);
                b = (((first - GetKeyByValue('о') * a) % 32) + 32) % 32;
            }
            else
            {
                a = DivByMod(32, (second - first + 32) % 32, ((GetKeyByValue('о') - GetKeyByValue('е')) + 32) % 32);
                b = (((second - GetKeyByValue('е') * a) % 32) + 32) % 32;

            }
            return (a, b);
        }


        static string Decryption(string str, int a, int b) // Расшифрование 
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

        static int ReciprocalNumber(int a, int m) // Поиск обратного элемента по модулю m
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

        static int DivByMod(int m, int c, int d) // Деление по модулю
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

        static char GetValueByKey(int key) // Получить из алфавита букву по ключу
        {
            foreach (var recordOfDictionary in Alphabet)
            {
                if (recordOfDictionary.Value.Equals(key))
                    return recordOfDictionary.Key;
            }
            return '1';
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