using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptMethodsLab1
{
    class Program
    {


        static readonly byte Key = 13;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter file path to encrypt/decrypt: ");
            string FilePath = PathInput();
            Console.WriteLine("Enter new file path or '0' if you want to overwrite original file: ");
            string EditedFilePath = NotNullStringInput();
            if (EditedFilePath == "0")
            {
                FileEncryption(FilePath);
            }
            else
            {
                FileEncryption(FilePath, EditedFilePath);
            }
            Console.WriteLine("File was modified");
        }



        static byte[] Multiplicity(byte[] bytes, int key) // Сделать длину массива кратной ключу
        {
            int difference = bytes.Length % key;
            if (difference != 0)
            {
                Array.Resize(ref bytes, bytes.Length + key - difference);
                for (int i = bytes.Length - 1; i > bytes.Length - key + difference; i--)
                {
                    bytes[i] = 0;
                }
            }
            return bytes;
        }


        static void FileEncryption(string FilePath)
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            bytes = Multiplicity(bytes, 2);

            for (int i = 0; i < bytes.Length / 2; i++)
            {
                (bytes[2 * i], bytes[2 * i + 1]) = FeistelCipher(bytes[2 * i], bytes[2 * i + 1]);

            }

            File.WriteAllBytes(FilePath, bytes);
        }

        static void FileEncryption(string FilePath, string EditedFilePath)
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            bytes = Multiplicity(bytes, 2);
            for (int i = 0; i < bytes.Length / 2; i++)
            {
                (bytes[2 * i], bytes[2 * i + 1]) = FeistelCipher(bytes[2 * i], bytes[2 * i + 1]);

            }

            File.WriteAllBytes(EditedFilePath, bytes);
        }

        static (byte, byte) FeistelCipher(byte L, byte R)
        {

            byte x = R;

            for (int i = 0; i < 2; i++)
            {
                x = (byte)((x << 1) ^ (x << 3) ^ Program.Key ^ L);
                L = R;
                R = x;
                x = R;
            }

            L = (byte)((x << 1) ^ (x << 3) ^ Program.Key ^ L);


            return (L, R);
        }



        public static int IntInput() // Получить данные типа Integer
        {
            while (true)
            {
                int digit;
                if (int.TryParse(Console.ReadLine(), out digit))
                {
                    return digit;
                }
                else
                {
                    Console.WriteLine("Incorrect value");
                }
            }
        }

        static string PathInput() // Проверка на существование файла
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (File.Exists(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("File does not exist. Try again");
                }
            }
        }

        static string NotNullStringInput() // Проверка на непустую строку
        {
            string input = Console.ReadLine();
            while (input == "")
            {
                Console.WriteLine("Enter valid value");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}