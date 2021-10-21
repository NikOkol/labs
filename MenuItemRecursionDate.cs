using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.MenuItems
{
    public class MenuItemRecursionDate : MenuItemCore
    {
        public override string Title { get { return "Recursion date"; } }

        public override void Execute()
        {
            Console.WriteLine("Enter the first section");
            (DateTime first_section_from, DateTime first_section_to) = Get_section();
            Console.WriteLine("Enter the second section");
            (DateTime second_section_from, DateTime second_section_to) = Get_section();
            Console.WriteLine("First section: " + first_section_from.ToString("dd.MM.yyyy") + " - " + first_section_to.ToString("dd.MM.yyyy"));
            Console.WriteLine("Second section: " + second_section_from.ToString("dd.MM.yyyy") + " - " + second_section_to.ToString("dd.MM.yyyy"));
            int n = 0;
            if (first_section_from <= second_section_from)
            {
                if ((first_section_to >= second_section_to) && (first_section_to >= second_section_from))
                {
                    n = second_section_to.Subtract(second_section_from).Days + 1;
                }
                if ((first_section_to <= second_section_to) && (first_section_to >= second_section_from))
                {
                    n = first_section_to.Subtract(second_section_from).Days + 1;
                }
                if (first_section_to < second_section_from)
                {
                    n = 0;
                }
            }
            else
            {
                if ((first_section_to >= second_section_to) && (second_section_to >= first_section_from))
                {
                    n = second_section_to.Subtract(first_section_from).Days + 1;
                }
                if ((first_section_to <= second_section_to) && (second_section_to >= first_section_from))
                {
                    n = first_section_to.Subtract(first_section_from).Days + 1;
                }
                if (second_section_to < first_section_from)
                {
                    n = 0;
                }
            }
            Console.WriteLine("N = {0} / {1}", n, int.MaxValue);
            if (n < int.MaxValue)
            {
                Recursive_calculator(n);
            }
            else
            {
                Console.WriteLine("N more than limit!");
            }
        }

        private static (DateTime get_from, DateTime get_to) Get_section()
        {
            DateTime from;
            DateTime to;
            do
            {
                from = IOUtils.Date_input("From: ");
                to = IOUtils.Date_input("To: ");
                if (from > to)
                {
                    Console.WriteLine("Error: second date is less than first! Try again.");
                }
            } while (from > to);
            return (from, to);
        }

        private static void Recursive_calculator(int n)
        {
            int[] Fibbonaci = new int[] { 0, 1, 1 };
            Console.WriteLine(Fibbonaci[0]);
            while (Fibbonaci[1] < n)
            {
                Console.WriteLine(Fibbonaci[1]);
                Fibbonaci[2] = Fibbonaci[0] + Fibbonaci[1];
                Fibbonaci[0] = Fibbonaci[1];
                Fibbonaci[1] = Fibbonaci[2];
            }
        }
    }
}
