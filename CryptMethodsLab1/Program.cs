using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptMethodsLab1
{
    class Program
    {
        static void Main(string[] args)
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
                    case 1:
                        Encryption();
                        Console.WriteLine("File was encrypted");
                        break;
                    case 2:
                        Decryption();
                        break;
                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            }
        }

        static void Encryption()
        {
            List<string> ListOfLines = new List<string>();

            StreamReader sr = new StreamReader("D:\\Important_Information.txt");
            

            string line = sr.ReadLine();
            char[] CharArray = line.ToCharArray();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] bytes = Encoding.Latin1.GetBytes(line);

            while (line != null)
            {
                CharArray = line.ToCharArray();
                bytes = Encoding.Latin1.GetBytes(line);
                for (int i = 0; i < line.Length; i++)
                {
                    CharArray[i] = Convert.ToChar((bytes[i] * 5 + 23) % 256);
                }
                line = new string(CharArray);
                ListOfLines.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            StreamWriter sw = new StreamWriter("D:\\Cipher.txt");
            foreach (string Line in ListOfLines)
            {
                sw.WriteLine(Line);
            }

            sw.Close();
        }

        static void Decryption()
        {
            List<string> ListOfLines = new List<string>();

            StreamReader sr = new StreamReader("D:\\Cipher.txt");

            string line = sr.ReadLine();
            char[] CharArray = line.ToCharArray();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] bytes = Encoding.Latin1.GetBytes(line);

            while (line != null)
            {
                CharArray = line.ToCharArray();
                bytes = Encoding.Latin1.GetBytes(line);
                for (int i = 0; i < line.Length; i++)
                {
                    CharArray[i] = Convert.ToChar(((bytes[i] - 23 + 256) * 205) % 256);
                }
                line = new string(CharArray);
                ListOfLines.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            StreamWriter sw = new StreamWriter("D:\\Decrypted.txt");
            foreach(string Line in ListOfLines)
            {
                sw.WriteLine(Line);
            }
            sw.Close();
        }

        public static int Int_input()
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