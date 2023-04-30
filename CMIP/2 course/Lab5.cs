using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptMethodsLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            const byte G = 0b_0001_0110; // 1 + x^2 + x^3 + x^5
            byte G_degree = Degree(G);
            List<string> hash_values = new List<string>();
            List<string> collisions = new List<string>();
            for (int i = 0; i <= 255; i++)
            {
                string hash = CRC((byte)i, G, G_degree);
                if (hash_values.Contains(hash))
                {
                    if (!collisions.Contains(hash))
                    {
                        collisions.Add(hash);
                    }
                }
                else
                {
                    hash_values.Add(hash);
                }
            }
            Console.WriteLine("Collisions found:");
            foreach (string str in collisions)
            {
                Console.WriteLine(str);
            }
        }

        static string CRC(byte message, byte g, byte g_degree)
        {
            string P = Convert.ToString(message, 2);
            for (int i = 0; i < g_degree; i++)
            {
                P += "0";
            }
            string remainder;

            while (P.Length >= g_degree)
            {
                remainder = Convert.ToString(BinaryStringToByte(P.Substring(0, g_degree)) ^ g, 2);
                P = P.Substring(g_degree);

                if (remainder != "0")
                {
                    P = remainder + P;
                }
                if (P.Contains('1'))
                {
                    while (P[0] == '0')
                    {
                        P = P.Substring(1);
                    }
                }
                else
                {
                    return remainder;
                }

            }
            return P;

        }

        static byte BinaryStringToByte(string str)
        {
            byte b = 0;
            for (int i = str.Length <= 7 ? str.Length - 1 : 7; i >= 0; i--)
            {
                if (str[i] == '1')
                {
                    b += (byte)Math.Pow(2, str.Length <= 7 ? str.Length - 1 - i : 7 - i);
                }
            }
            return b;
        }

        static byte Degree(byte g)
        {
            byte degree = 0;
            while (g != 0)
            {
                degree++;
                g = (byte)(g >> 1);
            }
            return degree;
        }

    }
}