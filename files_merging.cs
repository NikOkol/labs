using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes1 = File.ReadAllBytes("D:\\SBS-002\\ФЦТК.jpg");
            byte[] bytes2 = File.ReadAllBytes("D:\\SBS-002\\TEXT1.txt");
            byte[] bytes3 = new byte[bytes1.Length + bytes2.Length];
            for (int i = 0; i < bytes1.Length; i++)
            {
                bytes3[i] = bytes1[i];
            }

            for (int i = bytes1.Length; i < bytes1.Length + bytes2.Length; i++)
            {
                bytes3[i] = bytes2[i - bytes1.Length];
            }
            File.WriteAllBytes("D:\\SBS-002\\united", bytes3);
        }
    }
}
