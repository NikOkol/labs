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

            int[] key = { 6, 1, 2, 5, 3, 4 };

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
                            FileEncryption(FilePath, key);
                        }
                        else
                        {
                            FileEncryption(FilePath, EditedFilePath, key);
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
                            FileDecryption(EncryptedFilePath, key);
                        }
                        else
                        {
                            FileDecryption(EncryptedFilePath, DecryptingFilePath, key);
                        }
                        Console.WriteLine("File was decrypted");
                        break;

                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            }
        }



        static void FileEncryption(string FilePath, int[] key) // Шифрование файла с перезаписью оригинала
        {

            byte[] bytes = File.ReadAllBytes(FilePath);

            bytes = Multiplicity(bytes, key);

            File.WriteAllBytes(FilePath, Transposition(bytes, key));
        }

        static void FileEncryption(string FilePath, string EditedFilePath, int[] key) // Шифрование файла с созданием зашифрованной копии
        {
            byte[] bytes = File.ReadAllBytes(FilePath);

            bytes = Multiplicity(bytes, key);

            File.WriteAllBytes(EditedFilePath, Transposition(bytes, key));
        }

        static void FileDecryption(string FilePath, int[] key) // Расшифрование файла с перезаписью оригинала
        {
            byte[] bytes = File.ReadAllBytes(FilePath);

            bytes = Multiplicity(bytes, key);

            File.WriteAllBytes(FilePath, ReverseTransposition(bytes, key));
        }

        static void FileDecryption(string FilePath, string DecryptedFilePath, int[] key) // Расшифрование файла с созданием расшифрованной копии
        {
            byte[] bytes = File.ReadAllBytes(FilePath);

            bytes = Multiplicity(bytes, key);

            File.WriteAllBytes(DecryptedFilePath, ReverseTransposition(bytes, key));
        }




        static byte[] Multiplicity(byte[] bytes, int[] key) // Доведение до кратности длины файла длине ключа
        {
            int difference = bytes.Length % key.Length;
            if (difference != 0)
            {
                Array.Resize(ref bytes, bytes.Length + key.Length - difference);
                for (int i = bytes.Length - 1; i > bytes.Length - key.Length + difference; i--)
                {
                    bytes[i] = 0;
                }
            }
            return bytes;
        }


        static byte[] Transposition(byte[] bytes, int[] key) // Перестановка
        {
            byte[] result = new byte[bytes.Length];
            int res_pos = 0;

            for (int i = 0; i < bytes.Length; i += key.Length)
            {
                byte[] transposition = new byte[key.Length];

                for (int j = 0; j < key.Length; j++)
                {
                    transposition[key[j] - 1] = bytes[i + j];
                }

                for (int j = 0; j < key.Length; j++)
                {
                    result[res_pos++] = transposition[j];
                }
            }

            return result;
        }

        static byte[] ReverseTransposition(byte[] bytes, int[] key) // Обратная перестановка
        {
            byte[] result = new byte[bytes.Length];
            int res_pos = 0;

            for (int i = 0; i < bytes.Length; i += key.Length)
            {
                byte[] transposition = new byte[key.Length];

                for (int j = 0; j < key.Length; j++)
                {
                    transposition[j] = bytes[i + key[j] - 1];
                }

                for (int j = 0; j < key.Length; j++)
                {
                    result[res_pos++] = transposition[j];
                }
            }

            return ClearNullBytes(result);
        }

        static byte[] ClearNullBytes(byte[] bytes)
        {
            int counter = 0;
            int i = bytes.Length - 1;
            while (bytes[i] == 0 && i != 0)
            {
                counter++;
                i--;
            }
            byte[] result = new byte[bytes.Length - counter];
            for (int j = 0; j < result.Length; j++)
            {
                result[j] = bytes[j];
            }
            return result;
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