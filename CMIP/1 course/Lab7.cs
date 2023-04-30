using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptMethodsLab1
{
    class Program
    {
        static Dictionary<string, int> k_grams = new Dictionary<string, int>
        {

        };

        static void Main(string[] args)
        {
            string prime_string = "Летун отпущен на свободу. " +
                "Качнув две лопасти свои, Как чудище морское в воду, " +
                "Скользнул в воздушные струи. Его винты поют, как струны... " +
                "Смотри: недрогнувший пилот К слепому солнцу над трибуной Стремит свой винтовой полет...";
            prime_string = prime_string.ToLower();

            string[] words = FindAllWords(prime_string);
            double[] Enthropy = new double[5];
            for (int i = 1; i <= 5; i++)
            {
                FindKGrams(words, i); // Найти все k-граммы, где k = i
                int n = 0;
                foreach (var k_gram in k_grams)
                {
                    if (k_gram.Key.Length == i) // Считаем, сколько k-грамм, k = i.
                    {
                        n++;
                    }
                }
                foreach (var k_gram in k_grams)
                {
                    if (k_gram.Key.Length == i) // Считаем вероятность p и энтропию
                    {
                        double p = (double)k_gram.Value / n; // p = m / n
                        Enthropy[i - 1] += p * Math.Log(p, 2);
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("H_{0}/{0} = {1}", i + 1, Enthropy[i] / (i + 1));
            }


        }

        static string[] FindAllWords(string str) // Составление массива строк из всех слов из текста
        {
            List<string> words = new List<string>();
            string word = "";
            foreach (char letter in str)
            {
                if (letter != ' ' && letter != '.' && letter != ',' && letter != ':')
                {
                    word += letter;
                }
                else
                {
                    if (word != "")
                    {
                        words.Add(word);
                    }
                    word = "";
                }
            }
            return words.ToArray();
        }

        static void FindKGrams(string[] words, int k)
        {
            string k_gram = "";
            foreach (string word in words)
            {
                int letter_pos = 0;
                foreach (char letter in word)
                {
                    if (letter_pos <= word.Length - k)
                    {
                        for (int i = 0; i < k; i++)
                        {
                            k_gram += word[letter_pos + i];
                        }
                        if (!k_grams.ContainsKey(k_gram))
                        {
                            k_grams.Add(k_gram, 1);
                        }
                        else
                        {
                            k_grams[k_gram]++;
                        }
                        k_gram = "";
                    }
                    else
                    {
                        break;
                    }
                    letter_pos++;
                }
            }
        }


    }
}