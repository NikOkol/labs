using System;
using System.Collections.Generic;
using System.IO;

namespace CryptMethodsLab1
{
    class Program
    {
        const byte x_first = 0b_0000_0101;
        const byte key = x_first;

        static void Main(string[] args)
        {
            byte[] gamma = Generator(x_first);
            Console.WriteLine("Gamma bytes:");
            foreach (byte b in gamma)
            {
                Console.Write(Convert.ToString(b, 2).PadLeft(8, '0') + " ");
            }
            MenuOutput(gamma);
        }

        static void MenuOutput(byte[] gamma)
        {
            while (true)
            {
                Console.WriteLine("\n0 - Exit");
                Console.WriteLine("1 - Encryption/Decryption");
                int choice = Int_input();
                switch (choice)
                {
                    case 0:
                        return;

                    case 1:
                        Console.WriteLine("Enter file path to encrypt: ");
                        string FilePath = PathInput();
                        FileEncryption(FilePath, gamma);
                        Console.WriteLine("File was encrypted");
                        break;

                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            }
        }
        static void FileEncryption(string FilePath, byte[] gamma) // Расшифрование и шифрование
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            int i = 0;
            foreach (byte b in bytes)
            {
                bytes[i] = (byte)(b ^ gamma[i % gamma.Length]);
                i++;
            }

            File.WriteAllBytes(FilePath, bytes);
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

        static byte[] Generator(byte first_x) // ГПСП на регистрах сдвига с линейной обр. связью
        {
            int position = 0;
            byte[] gamma = new byte[1];
            Console.WriteLine("Gamma generation:\n" +
                "#1: {0} -> {1}", Convert.ToString(first_x, 2).PadLeft(8, '0'), (first_x % 2 != 0) ? 1 : 0);
            gamma[0] += (first_x % 2 != 0) ? (byte)128 : (byte)0;
            byte next_x = Conversion(first_x);
            while (next_x != first_x)
            {
                position++;
                if (position % 8 == 0)
                {
                    gamma = Append(gamma, 0);
                }
                gamma[position / 8] += (next_x % 2 != 0) ? (byte)MathF.Pow(2, 7 - (position % 8)) : (byte)0;
                Console.WriteLine("#{0}: {1} -> {2}", position + 1, Convert.ToString(next_x, 2).PadLeft(8, '0'), (next_x % 2 != 0) ? 1 : 0);
                next_x = Conversion(next_x);
                if (position > 255)
                {
                    Console.WriteLine("Error: inf cycle");
                    break;
                }
            }
            return gamma;
        }

        static byte[] Append(byte[] array, byte item) // Расширение массива
        {
            byte[] result = new byte[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }

        static byte Conversion(byte source) // Преобразования над регистром
        {
            byte new_byte = (byte)(source >> 1);
            if (LastBitCalculator(source))
            {
                new_byte += 128;
            }
            return new_byte;
        }

        static bool LastBitCalculator(byte x) // Вычисление старшего бита
        {
            x = (byte)(x & key);
            int last_bit = (x % 2) ^ ((x >> 1) % 2);
            x = (byte)(x >> 1);
            for (int i = 0; i < 6; i++)
            {
                x = (byte)(x >> 1);
                last_bit = last_bit ^ (x % 2);

            }
            return last_bit != 0;
        }

    }
}