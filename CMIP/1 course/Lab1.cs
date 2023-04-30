using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptMethodsLab1
{
    class Program
    {

        static void Main(string[] args) // Меню для выбора действия
        {
            while (true)
            {
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Encryption");
                Console.WriteLine("2 - Decryption");
                int choice = Int_input();
                switch (choice)
                {
                    case 0:
                        return;

                    case 1:      // Выбор шифрования
                        Console.WriteLine("Enter file path to encrypt: ");
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
                        Console.WriteLine("File was encrypted");
                        break;

                    case 2:     // Выбор дешифровки
                        Console.WriteLine("Enter file path to decrypt: ");
                        string EncryptedFilePath = PathInput();
                        Console.WriteLine("Enter new file path or '0' if you want to overwrite original file: ");
                        string DecryptingFilePath = NotNullStringInput();

                        if (DecryptingFilePath == "0")
                        {
                            FileDecryption(EncryptedFilePath);
                        }
                        else
                        {
                            FileDecryption(EncryptedFilePath, DecryptingFilePath);
                        }
                        Console.WriteLine("File was decrypted");
                        break;

                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            }
        }



        static void FileEncryption(string FilePath) // Шифрование файла с перезаписью оригинала
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            int i = 0;
            foreach (byte b in bytes)
            {
                bytes[i] = (byte)((bytes[i] * 5 + 23) % 256);
                i++;
            }

            File.WriteAllBytes(FilePath, bytes);
        }

        static void FileEncryption(string FilePath, string EditedFilePath) // Шифрование файла с созданием зашифрованной копии
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            int i = 0;
            foreach (byte b in bytes)
            {
                bytes[i] = (byte)((bytes[i] * 5 + 23) % 256);
                i++;
            }

            File.WriteAllBytes(EditedFilePath, bytes);
        }

        static void FileDecryption(string FilePath) // Расшифрование файла с перезаписью оригинала
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            int i = 0;
            foreach (byte b in bytes)
            {
                bytes[i] = (byte)(((bytes[i] - 23 + 256) * ReciprocalNumber(5, 256)) % 256);
                i++;
            }

            File.WriteAllBytes(FilePath, bytes);
        }

        static void FileDecryption(string FilePath, string DecryptedFilePath) // Расшифрование файла с созданием расшифрованной копии
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            int i = 0;
            foreach (byte b in bytes)
            {
                bytes[i] = (byte)(((bytes[i] - 23 + 256) * ReciprocalNumber(5, 256)) % 256);
                i++;
            }

            File.WriteAllBytes(DecryptedFilePath, bytes);
        }

        static int ReciprocalNumber(int a, int m) // Поиск обратного элемента по модулю m
        {
            int x = 1;
            while (((a * x) % m) != 1)
            {
                x++;
                if (x > m)
                {
                    Console.WriteLine("Error: Reciprosal does not exist");
                    return 0;
                }
            }
            return x;
        }

        static string NotNullStringInput()
        {
            string input = Console.ReadLine();
            while (input == "")
            {
                Console.WriteLine("Enter valid value");
                input = Console.ReadLine();
            }
            return input;
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

        public static int Int_input() // Чтение integer
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
    }
}