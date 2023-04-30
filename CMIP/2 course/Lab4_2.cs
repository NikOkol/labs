using System;
using System.Collections.Generic;
using System.IO;

namespace CryptMethodsLab1
{
    class Program
    {
        static int p = 193, q = 347;
        static void Main(string[] args)
        {
            int d = 2;
            int F = (q - 1) * (p - 1);
            for (int i = 0; i < 3; i++)
            {
                while (!IsCoprime(d, F))
                {
                    d++;
                }
                int e = 1;
                while ((e * d) % F != 1)
                {
                    e++;
                }
                Console.WriteLine("Public key = ({0}, {1}) Private key = ({2}, {1})", e, p * q, d);
                d++;
            }
        }

        static bool IsCoprime(int a, int b)
        {
            if (NOD(a, b) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        static int NOD(int a, int b)
        {
            while (b != 0)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
            return a;
        }

    }
}