using System;
using System.Collections.Generic;
using System.IO;

namespace CryptMethodsLab1
{
    class Program
    {
        const byte x_first = 0b_0110_0001; // 97
        static int gen_length = 0, odd_count = 0, even_count = 0, zeros_count = 0, ones_count = 0;
        const byte key = 0b_1010_0101; // 1 + x + x^3 + x^6 + x^8
        static void Main(string[] args)
        {
            Generator(x_first);
        }


        static void Generator(byte x)
        {
            byte next_x = (byte)(x >> 1);
            if (LastBitCalculator(x))
            {
                next_x += 128;
            }
            Console.Write(x % 2);
            gen_length++;
            if (x % 2 == 0)
            {
                zeros_count++;
            }
            else
            {
                ones_count++;
            }
            if (gen_length % 8 == 0)
            {
                Console.Write(' ');
                if (x % 2 == 0)
                {
                    even_count++;
                }
                else
                {
                    odd_count++;
                }
            }
            if (next_x != x_first)
            {
                Generator(next_x);
            }
            else
            {
                if (gen_length % 8 != 0)
                {
                    if (x % 2 == 0)
                    {
                        even_count++;
                    }
                    else
                    {
                        odd_count++;
                    }
                }
                Console.Write('\n');
                Console.WriteLine("Длина периода в битах: {0}", gen_length);
                Console.WriteLine("Четных: {0} | Нечетных: {1}", even_count, odd_count);
                Console.WriteLine("Число нулей: {0} | Число единиц: {1}", zeros_count, ones_count);
            }


        }

        static bool LastBitCalculator(byte x)
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