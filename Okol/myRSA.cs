using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Okol
{
    internal class myRSA
    {
        public static List<int> PrimesList()
        {
            int n = 1000;
            bool[] is_prime = new bool[n + 1];
            for (int i = 2; i <= n; ++i)
                is_prime[i] = true;

            List<int> primes = new List<int>();

            for (int i = 2; i <= n; ++i)
                if (is_prime[i])
                {
                    primes.Add(i);
                    if (i * i <= n)
                        for (int j = i * i; j <= n; j += i)
                            is_prime[j] = false;
                }

            return primes;
        }

        public static int RandPrime()
        {
            List<int> primes = PrimesList();
            Random rand = new Random();
            return primes[rand.Next(primes.Count - 1)];
        }


        public static int RandE(int p, int q)
        {
            int E_func = (p - 1) * (q - 1);
            List<int> possible_e_list = new List<int>();
            for (int i = 5; i < E_func; i++)
            {
                if (NOD(E_func, i) == 1) possible_e_list.Add(i);
            }
            Random rand = new Random();
            return possible_e_list[rand.Next(possible_e_list.Count - 1)];
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

        static (int, int, int) gcdex(int a, int b)
        {
            if (a == 0)
                return (b, 0, 1);
            (int gcd, int x, int y) = gcdex(b % a, a);
            return (gcd, y - (b / a) * x, x);
        }

        public static int invmod(int a, int m)
        {
            (int g, int x, int y) = gcdex(a, m);
            return g > 1 ? 0 : (x % m + m) % m;
        }

        public static BigInteger ModPow(BigInteger x, BigInteger n, BigInteger m) // x^n (mod m), n >= 0
        {
            if (n == 0)
            {
                return 1;
            }
            BigInteger res = x % m;
            for (BigInteger i = 1; i < n; i++)
            {
                res = (res * x) % m;
            }
            return res;
        }

        public static (int, int, int) GenerateRandomKey() // returns n, e, d
        {
            int p = RandPrime();
            int q = RandPrime();
            while (p == q)
            {
                q = RandPrime();
            }
            int n = p * q;
            int E_func = (p - 1) * (q - 1);
            int e = RandE(p, q);
            int d = invmod(e, E_func);
            return (n, e, d);
        }

        public static string Encrypt(string str, int n, int e)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            int[] blocks = new int[buffer.Length];
            int i = 0;
            foreach (byte b in buffer)
            {
                blocks[i] = b;
                i++;
            }
            i = 0;
            int[] C = new int[blocks.Length];
            foreach (int block in blocks)
            {
                C[i] = (int)ModPow(block, e, n);
                i++;
            }
            string res = "";
            foreach (int block in C)
            {
                res += block.ToString() + " ";
            }
            return res.Trim();
        }

        public static string Decrypt(string str, int n, int d)
        {
            int[] C = str.Split(' ').Select(int.Parse).ToArray();
            int[] M = new int[C.Length];
            int i = 0;
            foreach (int block in C)
            {
                M[i] = (int)ModPow(block, d, n);
                i++;
            }
            i = 0;
            byte[] bytes = new byte[M.Length];
            foreach (int block in M)
            {
                bytes[i] = (byte)block;
                i++;
            }

            return Encoding.UTF8.GetString(bytes);
        }


    }
}