using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptMethodsLab1
{
    class Program
    {
        static readonly byte[,] S2_block = new byte[4, 16]
        {
            { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
            { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
            { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2 ,15 },
            { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }
        };

        static readonly int[] Key = { 1, 3 }; 

        static void Main(string[] args)
        {
            

            while (true)
            {
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Encryption");
                Console.WriteLine("2 - Decryption");
                int choice = IntInput();
                switch (choice)
                {
                    case 0:
                        return;

                    case 1:     
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

                    case 2:     
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

        static void FileDecryption(string FilePath)
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            string bits = "";
            int bytes_pos = 0;

            foreach (byte b in bytes)
            {

                for (int i = 0; i < 8 - Convert.ToString(b, 2).Length; i++) // Добавить нули слева до 8ми знаков
                {
                    bits += "0";
                }

                bits += Convert.ToString(b, 2);

                if (bits.Length % 6 == 0) // Когда количество бит в байтах станет кратно 6ти (24 бит), шифруем 3 байта
                {
                    byte[] new_bytes = BlocksDecryption(bits);
                    for (int i = 0; i < 3; i++)
                    {
                        bytes[bytes_pos - 2 + i] = new_bytes[i];
                    }
                    bits = "";
                }

                bytes_pos++;
            }

            File.WriteAllBytes(FilePath, bytes);
        }

        static void FileDecryption(string FilePath, string EditedFilePath)
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            string bits = "";
            int bytes_pos = 0;

            foreach (byte b in bytes)
            {

                for (int i = 0; i < 8 - Convert.ToString(b, 2).Length; i++) // Добавить нули слева до 8ми знаков
                {
                    bits += "0";
                }

                bits += Convert.ToString(b, 2);

                if (bits.Length % 6 == 0) // Когда количество бит в байтах станет кратно 6ти (24 бит), шифруем 3 байта
                {
                    byte[] new_bytes = BlocksDecryption(bits);
                    for (int i = 0; i < 3; i++)
                    {
                        bytes[bytes_pos - 2 + i] = new_bytes[i];
                    }
                    bits = "";
                }

                bytes_pos++;
            }

            File.WriteAllBytes(EditedFilePath, bytes);
        }

        static void FileEncryption(string FilePath) 
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            bytes = Multiplicity(bytes, 3);
            string bits = "";
            int bytes_pos = 0;

            foreach (byte b in bytes)
            {

                for (int i = 0; i < 8 - Convert.ToString(b, 2).Length; i++) // Добавить нули слева до 8ми знаков
                {
                    bits += "0";
                }

                bits += Convert.ToString(b, 2);

                if (bits.Length % 6 == 0) // Когда количество бит в байтах станет кратно 6ти (24 бит), шифруем 3 байта
                {
                    byte[] new_bytes = BlocksEncryption(bits);
                    for (int i = 0; i < 3; i++)
                    {
                        bytes[bytes_pos - 2 + i] = new_bytes[i];
                    }
                    bits = "";
                }

                bytes_pos++;
            }

            File.WriteAllBytes(FilePath, bytes);
        }

        static void FileEncryption(string FilePath, string EditedFilePath)
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            bytes = Multiplicity(bytes, 3);
            string bits = "";
            int bytes_pos = 0;

            foreach (byte b in bytes)
            {

                for (int i = 0; i < 8 - Convert.ToString(b, 2).Length; i++) // Добавить нули слева до 8ми знаков
                {
                    bits += "0";
                }

                bits += Convert.ToString(b, 2);

                if (bits.Length % 6 == 0) // Когда количество бит в байтах станет кратно 6ти (24 бит), шифруем 3 байта
                {
                    byte[] new_bytes = BlocksEncryption(bits);
                    for (int i = 0; i < 3; i++)
                    {
                        bytes[bytes_pos - 2 + i] = new_bytes[i];
                    }
                    bits = "";
                }

                bytes_pos++;
            }

            File.WriteAllBytes(EditedFilePath, bytes);
        }

        static byte[] Multiplicity(byte[] bytes, int key) // Сделать длину массива кратной числу key
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

        static byte[] BlocksEncryption(string bits)
        {
            string[] six_bits_blocks = new string[4];
            six_bits_blocks[0] = bits.Substring(0, 6);
            six_bits_blocks[1] = bits.Substring(6, 6);
            six_bits_blocks[2] = bits.Substring(12, 6);
            six_bits_blocks[3] = bits.Substring(18, 6);


            string new_bits = "";

            foreach (string block in six_bits_blocks)
            {
                int S_block_line = BinaryToDecimal(Convert.ToString(block[Program.Key[0]]) + Convert.ToString(block[Program.Key[1]]));
                string column_binary;
                if (Program.Key[0] < Program.Key[1])
                {
                    column_binary = block.Remove(Program.Key[0], 1);
                    column_binary = column_binary.Remove(Program.Key[1] - 1, 1);
                }
                else
                {
                    column_binary = block.Remove(Program.Key[1], 1);
                    column_binary = column_binary.Remove(Program.Key[0] - 1, 1);
                }
                int S_block_column = BinaryToDecimal(column_binary);

                string expanded_bits = Convert.ToString(S2_block[S_block_line, S_block_column], 2);



                expanded_bits = BitsExpand(expanded_bits, 4);



                string expanded_block_line = Convert.ToString(S_block_line, 2);

                expanded_block_line = BitsExpand(expanded_block_line, 2);


                new_bits += expanded_block_line + expanded_bits;

            }

            byte[] new_bytes = new byte[3];
            new_bytes[0] = BinaryToDecimal(new_bits.Substring(0, 8));
            new_bytes[1] = BinaryToDecimal(new_bits.Substring(8, 8));
            new_bytes[2] = BinaryToDecimal(new_bits.Substring(16, 8));

            return new_bytes; // Вернули 3 байта
        }

        static byte[] BlocksDecryption(string bits)
        {
            string[] six_bits_blocks = new string[4];
            six_bits_blocks[0] = bits.Substring(0, 6);
            six_bits_blocks[1] = bits.Substring(6, 6);
            six_bits_blocks[2] = bits.Substring(12, 6);
            six_bits_blocks[3] = bits.Substring(18, 6);

            string new_bits = "";

            foreach (string block in six_bits_blocks)
            {
                int S_block_line = BinaryToDecimal(block.Substring(0, 2));
                int S_block_column = 0;
                for (int i = 0; i < 16; i++)
                {
                    if (BinaryToDecimal(block.Substring(2, 4)) == Program.S2_block[S_block_line, i])
                    {
                        S_block_column = i;
                        break;
                    }
                }
                string old_block = Convert.ToString(S_block_column, 2);


                old_block = BitsExpand(old_block, 4);


                if (Program.Key[0] < Program.Key[1])
                {
                    old_block = old_block.Insert(Program.Key[0], block.Substring(0, 1));
                    old_block = old_block.Insert(Program.Key[1], block.Substring(1, 1));
                }
                else
                {
                    old_block = old_block.Insert(Program.Key[1], block.Substring(0, 1));
                    old_block = old_block.Insert(Program.Key[0], block.Substring(1, 1));
                }

                new_bits += old_block;

            }


            byte[] new_bytes = new byte[3];
            new_bytes[0] = BinaryToDecimal(new_bits.Substring(0, 8));
            new_bytes[1] = BinaryToDecimal(new_bits.Substring(8, 8));
            new_bytes[2] = BinaryToDecimal(new_bits.Substring(16, 8));

            return new_bytes; // Вернули 3 байта

        }

        static string BitsExpand(string bits, int size)
        {
            while (bits.Length < size)
            {
                bits = bits.Insert(0, "0");
            }
            return bits;
        }

        static byte BinaryToDecimal(string bits) // Конвертация бинарного числа в десятичное
        {
            byte result = 0;
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                if (bits[i] == '1')
                {
                    result += (byte)Math.Pow(2, bits.Length - i - 1);
                }
            }
            return result;
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