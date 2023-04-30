using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptMethodsLab1
{
    class Program
    {
        const int a = 109, c = 55, m = 209, x_zero = 21;  // ���. 21
        static int gen_length = 1, odd_count = 0, even_count = 0, zeros_count = 0, ones_count = 0;
        static void Main(string[] args)
        {
            Console.Write(x_zero + " ");
            Analyse(x_zero);
            Generator(x_zero);
        }

        static void Generator(int x_n) // ����������� ��������� ������������������
        {
            int x_next = (a * x_n + c) % m;
            if (x_next != x_zero)
            {
                gen_length++;
                Console.Write(x_next + " ");
                Analyse(x_next);
                Generator(x_next);
            }
            else // ���� ��������� ���� ����� x(0), ������ �������
            {
                Console.Write("\n");
                Console.Write(ByteForm(x_zero) + " ");
                BinaryOutput(x_zero);
                Console.WriteLine("������ = {0}", gen_length);
                Console.WriteLine("����� ������� � �����: {0}", gen_length * 8);
                Console.WriteLine("������: {0} | ��������: {1}", even_count, odd_count);
                Console.WriteLine("����� �����: {0} | ����� ������: {1}", zeros_count, ones_count);
            }
        }

        static void BinaryOutput(int x_n)
        {
            int x_next = (a * x_n + c) % m;
            if (x_next != x_zero)
            {
                Console.Write(ByteForm(x_next) + " ");
                BinaryOutput(x_next);
            }
            else { Console.Write("\n"); }
        }

        static void Analyse(int x) // �������� �� ��������, ������� ����� � ������
        {
            string bits = ByteForm(x);
            foreach (char c in bits)
            {
                if (c == '0')
                {
                    zeros_count++;
                }
                else if (c == '1')
                {
                    ones_count++;
                }
            }

            if (bits[7] == '0')
            {
                even_count++;
            }
            else if (bits[7] == '1')
            {
                odd_count++;
            }
        }

        static string ByteForm(int x) // ������� �� ���������� � ��������, ���������� ������ �� �����
        {
            string bits = "";
            for (int i = 0; i < 8 - Convert.ToString(x, 2).Length; i++) // �������� ���� ����� �� 8�� ������
            {
                bits += "0";
            }

            bits += Convert.ToString(x, 2);
            return bits;
        }
    }
}